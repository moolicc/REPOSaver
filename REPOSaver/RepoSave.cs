using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOSaver
{
    public enum SaveStatus
    {
        Idle,
        Archiving,
        Restoring,

    }

    public class RepoSave
    {
        public SaveStatus Status { get; set; }
        public bool AutoWatchEnabled { get; set; }


        public string FullDirectoryPath { get; private set; }
        public string MainFile { get; private set; }
        public string MainFilePath { get; private set; }
        public string BackupFilePath { get; private set; }

        public DateTime LastModifiedTime => File.GetLastWriteTime(MainFilePath);

        public RepoSave(string directory, string mainFile, string backupDirectory)
        {
            Status = SaveStatus.Idle;
            AutoWatchEnabled = true;
            FullDirectoryPath = directory;
            MainFile = mainFile;
            MainFilePath = Path.Combine(directory, mainFile);

            DirectoryInfo di = new DirectoryInfo(directory);
            BackupFilePath = Path.Combine(backupDirectory, $"{di.Name}.zip");
        }
    }
}
