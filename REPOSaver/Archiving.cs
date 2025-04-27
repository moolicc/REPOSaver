using NLog;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOSaver
{
    
    static class Archiving
    {
        private readonly record struct BackupDebounceKey;
        private readonly record struct RestoreDebounceKey;


        public static Logger Log => LogManager.GetLogger(nameof(Archiving));

        private static SemaphoreSlim _semaphore;

        private static CancellationTokenSource _restoreDebounceCancel;
        private static int _restoreCounter;
        private static object _restoreDebounceLock;

        static Archiving()
        {
            _semaphore = new SemaphoreSlim(1, 1);
            _restoreDebounceLock = new object();
        }

        public static async Task<bool> Backup(string directory, string zipFile)
        {
            int opid = Debouncer<BackupDebounceKey>.GetCount();
            Log.Info("[{0}] Awaiting my turn to backup from {1} to {2}...", opid, directory, zipFile);

            // The semaphore ensures only a single archive/backup may run at once.
            await _semaphore.WaitAsync();

            bool success = await Debouncer<BackupDebounceKey>.DebounceAsync();
            try
            {
                if (success)
                {
                    Log.Info("[{0}] Performing backup from {1} to {2}...", opid, directory, zipFile);
                    Backup_Backend(directory, zipFile);
                }
                else
                {
                    Log.Info("[{0}] Debouncing backup from {1} to {2}...", opid, directory, zipFile);
                }
            }
            finally
            {
                _semaphore.Release();
            }
            Log.Info("[{0}] Ending my turn to backup from {1} to {2}...", opid, directory, zipFile);
            return success;
        }

        public static async Task<bool> Restore(string zipFile, string directory)
        {
            int opid = Debouncer<RestoreDebounceKey>.GetCount();
            Log.Info("[{0}] Awaiting my turn to restore from {1} to {2}...", opid, zipFile, directory);


            // The semaphore ensures only a single archive/backup may run at once.
            await _semaphore.WaitAsync();

            bool success = await Debouncer<RestoreDebounceKey>.DebounceAsync();
            try
            {
                if (success)
                {
                    Log.Info("[{0}] Performing restore from {1} to {2}...", opid, zipFile, directory);
                    Restore_Backend(zipFile, directory);
                }
                else
                {
                    Log.Info("[{0}] Debouncing restore from {1} to {2}...", opid, zipFile, directory);
                }
            }
            finally
            {
                _semaphore.Release();
            }
            Log.Info("[{0}] Ending my turn to restore from {1} to {2}...", opid, zipFile, directory);
            return success;
        }


        private static void Backup_Backend(string directory, string zipFile)
        {
            using (var destination = new FileStream(zipFile, FileMode.Create))
            {
                using (var archive = new ZipArchive(destination, ZipArchiveMode.Create))
                {
                    foreach (var file in Directory.GetFiles(directory))
                    {
                        archive.CreateEntryFromFile(file, Path.GetFileName(file));
                    }
                }
            }
        }

        private static void Restore_Backend(string zipFile, string directory)
        {
            using (var source = new FileStream(zipFile, FileMode.Open))
            {
                using (var archive = new ZipArchive(source, ZipArchiveMode.Read))
                {
                    archive.ExtractToDirectory(directory, true);
                }
            }
        }
    }
}
