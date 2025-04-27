using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOSaver
{
    class RepoDirectory
    {
        public string BaseDirectory { get; private set; }
        public string SavesDirectory { get; private set; }
        public string BackupsDirectory { get; private set; }
        public bool BaseExists => Directory.Exists(BaseDirectory);
        public bool SavesExists => Directory.Exists(SavesDirectory);

        public RepoDirectory(string baseDirectory)
        {
            BaseDirectory = baseDirectory;
            SavesDirectory = Path.Combine(BaseDirectory, "saves");
            BackupsDirectory = Path.Combine(BaseDirectory, "reposaver_backups");

            if(!Directory.Exists(BackupsDirectory))
            {
                Directory.CreateDirectory(BackupsDirectory);
            }
        }

        public RepoSave[] GetSaves()
        {
            if(!SavesExists)
            {
                return new RepoSave[0];
            }

            List<RepoSave> results = new List<RepoSave>();
            if(Directory.GetFiles(SavesDirectory).Any(f => f.EndsWith(".es3")))
            {
               // string mainFile = ".es3";
               // results.Add(new RepoSave(SavesDirectory, mainFile, BackupsDirectory));
            }

            foreach (string childFolder in Directory.GetDirectories(SavesDirectory))
            {
                if(Directory.GetFiles(childFolder).Any(f => f.EndsWith(".es3")))
                {
                    DirectoryInfo childFolderInfo = new DirectoryInfo(childFolder);
                    string mainFile = childFolderInfo.Name + ".es3";
                    results.Add(new RepoSave(childFolder, mainFile, BackupsDirectory));
                }
            }

            return results.ToArray();
        }

        public RepoSave CopySave(RepoSave save)
        {
            string src = save.FullDirectoryPath;
            DirectoryInfo info = new DirectoryInfo(src);

            string srcTag = info.Name;
            string destTag = $"REPO_SAVE_{DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss")}";

            string dest = Path.Combine(SavesDirectory, destTag);

            CopyDir(src, dest, srcTag, destTag);

            return new RepoSave(dest, destTag, BackupsDirectory);
        }

        public RepoSave CreateSave(string fromJson)
        {
            string destTag = $"REPO_SAVE_{DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss")}";
            string dest = Path.Combine(SavesDirectory, destTag);
            Directory.CreateDirectory(dest);

            string file = Path.Combine(dest, $"{destTag}.es3");
            File.WriteAllText(file, fromJson);

            return new RepoSave(dest, Path.GetFileName(file), BackupsDirectory);
        }

        private void CopyDir(string source, string dest, string sourceTag, string destTag)
        {
            Directory.CreateDirectory(dest);

            foreach (var subDirectory in Directory.GetDirectories(source))
            {
                CopyDir(subDirectory, Path.Combine(dest, Path.GetDirectoryName(subDirectory)!).Replace(sourceTag, destTag), sourceTag, destTag);
            }

            foreach (var file in Directory.GetFiles(source))
            {
                File.Copy(file, Path.Combine(dest, Path.GetFileName(file)!).Replace(sourceTag, destTag));
            }
        }
    }
}
