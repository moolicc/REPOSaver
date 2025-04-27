using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace REPOSaver
{
    class EasySave
    {
        public const string PASSWORD = "Why would you want to cheat?... :o It's no fun. :') :'D";
        public static readonly byte[] PW_BYTES = Encoding.UTF8.GetBytes(PASSWORD);


        public static Dictionary<string, object> Read(string path)
        {
            string json = ReadJson(path);
            return FromJson(json);
        }

        public static string ReadJson(string path)
        {
            string json = "";
            byte[] decryptedData;

            byte[] encrypted = Array.Empty<byte>();
            encrypted = File.ReadAllBytes(path);

            var iv = encrypted[..16];

            using (var hmac = new HMACSHA1())
            {
                var key = Rfc2898DeriveBytes.Pbkdf2(PASSWORD, iv, 100, HashAlgorithmName.SHA1, 16);
                using (var aes = Aes.Create())
                {
                    aes.Key = key;
                    aes.IV = iv;
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;

                    using (var decryptor = aes.CreateDecryptor())
                    {
                        decryptedData = decryptor.TransformFinalBlock(encrypted, 16, encrypted.Length - 16);
                    }
                }
            }

            json = Encoding.UTF8.GetString(decryptedData);
            return json;
        }


        public static void Save(string file, Dictionary<string, object> data)
        {
            string json = ToJson(data);
            SaveJson(file, json);
        }

        public static void SaveJson(string file, string json)
        {
            json = json.Replace("/", "\\/");
            json = json.Replace("System.Private.CoreLib, Version=9.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", "mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");


            byte[] buffer = Encoding.UTF8.GetBytes(json);

            byte[] iv = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(iv);
            }

            byte[] key = Rfc2898DeriveBytes.Pbkdf2(PASSWORD, iv, 100, HashAlgorithmName.SHA1, 16);
            byte[] encryptedData;
            using (var aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using (var encryptor = aes.CreateEncryptor())
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                        {
                            cryptoStream.Write(buffer, 0, buffer.Length);
                        }
                        encryptedData = memoryStream.ToArray();
                    }
                }
            }

            using (var fs = new FileStream(file, FileMode.Create))
            {
                fs.Write(iv, 0, iv.Length);
                fs.Write(encryptedData, 0, encryptedData.Length);
            }
        }


        private static Dictionary<string, object> FromJson(string json)
        {
            var options = new JsonSerializerOptions
            {
                Converters = { new TypeAwareJsonConverter() },
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            var deserializedData = JsonSerializer.Deserialize<Dictionary<string, object>>(json, options);
            return deserializedData;
        }

        private static string ToJson(Dictionary<string, object> data)
        {
            var options = new JsonSerializerOptions
            {
                Converters = { new TypeAwareJsonConverter() },
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            return JsonSerializer.Serialize(data, options);
        }
    }
}
