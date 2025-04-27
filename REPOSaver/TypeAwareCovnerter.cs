using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace REPOSaver
{

    public class TypeAwareJsonConverter : JsonConverter<object>
    {
        public override object Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException("Expected StartObject token");
            }

            string typeName = null;
            object value = null;

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    break;
                }

                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    string propertyName = reader.GetString();
                    reader.Read();

                    if (propertyName == "__type")
                    {
                        typeName = reader.GetString();
                    }
                    else if (propertyName == "value")
                    {
                        Type type = ResolveType(typeName);
                        value = DeserializeValue(ref reader, type, options);
                    }
                }
            }

            if (typeName == null || value == null)
            {
                throw new JsonException("Invalid object structure");
            }

            return value;
        }

        public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            // Write the type information
            Type valueType = value.GetType();
            string typeName = valueType.AssemblyQualifiedName!;

            if (valueType == typeof(float))
                typeName = "float";
            else if (valueType == typeof(int))
                typeName = "int";
            else if (valueType == typeof(string))
                typeName = "string";
            else if (valueType == typeof(bool))
                typeName = "bool";
            else if (valueType == typeof(double))
                typeName = "double";

            writer.WriteString("__type", typeName);

            // Write the actual value
            writer.WritePropertyName("value");
            SerializeValue(writer, value, options);

            writer.WriteEndObject();
        }

        private Type ResolveType(string typeName)
        {
            if (string.IsNullOrEmpty(typeName))
            {
                throw new JsonException("Type information is missing");
            }

            Type type = typeName switch
            {
                "float" => typeof(float),
                "int" => typeof(int),
                "bool" => typeof(bool),
                "string" => typeof(string),
                "double" => typeof(double),
                // Add more mappings as needed
                _ => Type.GetType(typeName)!,
            };

            if (type == null)
            {
                throw new JsonException($"Unable to resolve type: {typeName}");
            }

            return type;
        }

        private object DeserializeValue(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options)
        {
            if (type == typeof(int)) return reader.GetInt32();
            if (type == typeof(double)) return reader.GetDouble();
            if (type == typeof(bool)) return reader.GetBoolean();
            if (type == typeof(string)) return reader.GetString();
            if (typeof(Dictionary<string, object>).IsAssignableFrom(type))
            {
                return JsonSerializer.Deserialize<Dictionary<string, object>>(ref reader, options);
            }
            if (typeof(List<object>).IsAssignableFrom(type))
            {
                return JsonSerializer.Deserialize<List<object>>(ref reader, options);
            }

            // Fallback for other types
            return JsonSerializer.Deserialize(ref reader, type, options);
        }

        private void SerializeValue(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
        {
            if (value is int i)
            {
                writer.WriteNumberValue(i);
            }
            else if (value is double d)
            {
                writer.WriteNumberValue(d);
            }
            else if (value is bool b)
            {
                writer.WriteBooleanValue(b);
            }
            else if (value is string s)
            {
                writer.WriteStringValue(s);
            }
            else if (value is Dictionary<string, object> dict)
            {
                JsonSerializer.Serialize(writer, dict, options);
            }
            else if (value is List<object> list)
            {
                JsonSerializer.Serialize(writer, list, options);
            }
            else
            {
                // Fallback for other types
                JsonSerializer.Serialize(writer, value, value.GetType(), options);
            }
        }
    }
}
