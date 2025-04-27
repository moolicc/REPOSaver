using NLog;
using System;
using System.Diagnostics;
using System.Runtime.Intrinsics.Arm;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;

namespace REPOSaver
{
    public partial class MainForm : Form
    {

        private readonly record struct FileChangedDebounceKey;


        private static Logger WatcherLogger => LogManager.GetLogger("Watcher");
        private static Logger UILogger => LogManager.GetLogger("UI");

        private RepoDirectory? _repoDirectory;
        private List<RepoSave> _saves;
        private FileSystemWatcher _savesWatcher;

        public MainForm()
        {
            _saves = new List<RepoSave>();
            _savesWatcher = new FileSystemWatcher();
            _savesWatcher.EnableRaisingEvents = false;


            InitializeComponent();
        }

        #region FormEvents
        private void Form1_Load(object sender, EventArgs e)
        {
            _savesWatcher.IncludeSubdirectories = true;
            _savesWatcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.CreationTime | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            _savesWatcher.Filter = "*.es3";

            _savesWatcher.Changed += Watcher_Changed;

            AutoDetectDirectory();
            ResizeListView();
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            ResizeListView();
        }


        private void Btn_Directory_Auto_Click(object sender, EventArgs e)
        {
            AutoDetectDirectory();
        }

        private void Txt_Repo_TextChanged(object sender, EventArgs e)
        {
            _savesWatcher.EnableRaisingEvents = false;

            Lst_Saves.Items.Clear();
            _saves.Clear(); // TODO: Cleanup save monitoring.
            if (Directory.Exists(Txt_Repo.Text))
            {
                // The text box contains the path to the saves directory within the base Repo directory, so we have to go to the parent to get that base Repo folder.
                DirectoryInfo? parent = Directory.GetParent(Txt_Repo.Text);
                if (parent != null)
                {
                    _repoDirectory = new RepoDirectory(parent.FullName);

                    _savesWatcher.Path = Txt_Repo.Text;
                    _savesWatcher.IncludeSubdirectories = true;
                    ScanSaves();
                    ScanOrphanBackups();
                    _savesWatcher.EnableRaisingEvents = true;
                }
            }
        }
        #endregion

        #region ListEvents
        private void Lst_Saves_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            Lst_Saves.Items[e.Item].RepoSaveTag(save =>
            {
                var data = EasySave.Read(save.MainFilePath);
                data["teamName"] = e.Label ?? "R.E.P.O.";
                EasySave.Save(save.MainFilePath, data);
            });
        }

        private void Lst_Saves_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var item = Lst_Saves.FocusedItem!;

                if (item.ItemTag()!.Type == MainListItemTagTypes.RepoSave)
                {
                    Mnu_ListItem.Tag = item;
                    autoRestoreToolStripMenuItem.Checked = ((RepoSave)item.Tag!).AutoWatchEnabled;

                    restoreNowToolStripMenuItem.Enabled = ((RepoSave)item.Tag!).Status == SaveStatus.Idle;
                    backupNowToolStripMenuItem.Enabled = ((RepoSave)item.Tag!).Status == SaveStatus.Idle;

                    if (item != null && item.Bounds.Contains(e.Location))
                    {
                        // TODO: Enable/Disable based on if an item is right clicked or not (we want to be able to show import new options regardless of selection
                        Mnu_ListItem.Show(Cursor.Position);
                    }
                }
                else if (item.ItemTag().Type == MainListItemTagTypes.OrhpanArchive)
                {
                    Mnu_OrphanArchive.Tag = item;
                    if (item != null && item.Bounds.Contains(e.Location))
                    {
                        Mnu_OrphanArchive.Show(Cursor.Position);
                    }
                }
            }
        }

        private void Lst_Saves_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (Lst_Saves.SelectedItems.Count > 0)
                editSaveToolStripMenuItem.PerformClick();
        }

        private void Lst_Saves_SelectedIndexChanged(object sender, EventArgs e)
        {
            void SetEnabled(ToolStripItemCollection items, bool enabled)
            {
                foreach (var item in items)
                {
                    if (item is ToolStripDropDownButton d)
                    {
                        SetEnabled(d.DropDownItems, enabled);
                    }

                    if (item is ToolStripItem itm)
                    {
                        itm.Enabled = enabled;

                    }
                    else if (item is ToolStripMenuItem mitm)
                    {
                        mitm.Enabled = enabled;
                    }
                }
            }

            autoRestoreToolStripButton.Checked = false;
            SetEnabled(Tls_ListItem.Items, false);

            if (Lst_Saves.SelectedItems.Count > 0)
            {
                if (Lst_Saves.SelectedItems[0].ItemTag().Type == MainListItemTagTypes.RepoSave)
                {
                    SetEnabled(Tls_ListItem.Items, true);
                    autoRestoreToolStripButton.Checked = autoRestoreToolStripMenuItem.Checked;
                }
                else
                {
                    restoreNowToolStripButton.Enabled = true;
                }
            }
        }
        #endregion



        #region MenuStripEvents
        private void restoreNowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewItem selectedItem = Lst_Saves.SelectedItems[0];
            RepoSave save = (RepoSave)selectedItem.Tag!;
            Restore(save, selectedItem.Index);
        }

        private void backupNowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewItem selectedItem = Lst_Saves.SelectedItems[0];
            RepoSave save = (RepoSave)selectedItem.Tag!;
            Backup(save, selectedItem.Index);
        }

        private void autoRestoreToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            ListViewItem? selectedItem = (ListViewItem?)Mnu_ListItem.Tag;
            if (selectedItem?.Tag == null)
            {
                return;
            }

            RepoSave save = (RepoSave)selectedItem.Tag!;
            save.AutoWatchEnabled = autoRestoreToolStripMenuItem.Checked;


            autoRestoreToolStripButton.Checked = save.AutoWatchEnabled;
        }

        private void openInExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewItem selectedItem = Lst_Saves.SelectedItems[0];
            RepoSave save = (RepoSave)selectedItem.Tag!;

            Process.Start("explorer.exe", $"/select,\"{save.MainFilePath}\"");
        }

        private void openBackupInExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewItem selectedItem = Lst_Saves.SelectedItems[0];
            RepoSave save = (RepoSave)selectedItem.Tag!;

            if (File.Exists(save.BackupFilePath))
            {
                Process.Start("explorer.exe", $"\"{save.BackupFilePath}\"");
            }
            else
            {
                Process.Start("explorer.exe", $"\"{_repoDirectory?.BackupsDirectory}\"");
            }
        }

        private void editSaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewItem selectedItem = Lst_Saves.SelectedItems[0];

            selectedItem.RepoSaveTag(save =>
            {
                MessageBox.Show("Editing save files is risky business!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                var data = EasySave.Read(save.MainFilePath);

                SaveEditor editor = new SaveEditor(data);
                if (editor.ShowDialog() == DialogResult.OK)
                {
                    EasySave.Save(save.MainFilePath, editor.Fields);
                }
            });
        }

        private void renameSaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewItem selectedItem = Lst_Saves.SelectedItems[0];
            selectedItem.BeginEdit();
        }

        private void exportToFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Sdlg_ExportJson.ShowDialog() == DialogResult.OK)
            {
                ListViewItem selectedItem = Lst_Saves.SelectedItems[0];
                RepoSave save = (RepoSave)selectedItem.Tag!;
                var json = EasySave.ReadJson(save.MainFilePath);

                File.WriteAllText(Sdlg_ExportJson.FileName, json);
            }
        }


        private void exportToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewItem selectedItem = Lst_Saves.SelectedItems[0];
            RepoSave save = (RepoSave)selectedItem.Tag!;
            var json = EasySave.ReadJson(save.MainFilePath);
            Clipboard.SetText(json);
        }

        private void importFromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Odlg_ImportJson.ShowDialog() == DialogResult.OK)
            {
                ListViewItem selectedItem = Lst_Saves.SelectedItems[0];
                RepoSave save = (RepoSave)selectedItem.Tag!;
                string json = File.ReadAllText(Odlg_ImportJson.FileName);

                // TODO: Update name field after importing.
                EasySave.SaveJson(save.MainFilePath, json);
            }
        }

        private void importFromClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewItem selectedItem = Lst_Saves.SelectedItems[0];
            RepoSave save = (RepoSave)selectedItem.Tag!;
            string json = Clipboard.GetText();

            try
            {
                dynamic? obj = System.Text.Json.JsonSerializer.Deserialize<dynamic>(json);
            }
            catch
            {
                if (MessageBox.Show("The clipboard doesn't appear to contain valid json. Proceed anyway?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    return;
                }
            }

            // TODO: Update name field after importing.
            EasySave.SaveJson(save.MainFilePath, json);
        }


        private void newFromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Odlg_ImportJson.ShowDialog() == DialogResult.OK)
            {
                string json = File.ReadAllText(Odlg_ImportJson.FileName);
                RepoSave newSave = _repoDirectory!.CreateSave(json);

                // TODO: Update lists as necessary.
            }
        }

        private void newFromClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string json = Clipboard.GetText();

            try
            {
                dynamic? obj = System.Text.Json.JsonSerializer.Deserialize<dynamic>(json);
            }
            catch
            {
                if (MessageBox.Show("The clipboard doesn't appear to contain valid json. Proceed anyway?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    return;
                }
            }

            _repoDirectory!.CreateSave(json);
            // TODO: Update lists as necessary.
        }


        private void cloneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewItem selectedItem = Lst_Saves.SelectedItems[0];
            RepoSave save = (RepoSave)selectedItem.Tag!;

            _repoDirectory!.CopySave(save);
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _saves.Clear();
            Lst_Saves.Items.Clear();
            ScanSaves();
        }
        #endregion

        #region OrphanArchive MenuStripEvents

        private void moa_restoreNowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewItem selectedItem = Lst_Saves.SelectedItems[0];

            selectedItem.OrphanArchiveTag(p =>
            {
                Restore(p, selectedItem.Index);
            });
        }

        private void moa_openBackupInExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewItem selectedItem = Lst_Saves.SelectedItems[0];
            selectedItem.OrphanArchiveTag(path =>
            {
                if (File.Exists(path))
                {
                    Process.Start("explorer.exe", $"\"{path}\"");
                }
                else
                {
                    Process.Start("explorer.exe", $"\"{_repoDirectory?.BackupsDirectory}\"");
                }
            });
        }
        #endregion

        #region ToolstripEvents
        private void restoreNowToolStripButton_Click(object sender, EventArgs e)
        {
            Lst_Saves.SelectedItems[0].RepoSaveTag(_ =>
            {
                restoreNowToolStripMenuItem.PerformClick();
            });
            Lst_Saves.SelectedItems[0].OrphanArchiveTag(_ =>
            {
                moa_restoreNowToolStripMenuItem.PerformClick();
            });
        }

        private void backupNowToolStripButton_Click(object sender, EventArgs e)
        {
            backupNowToolStripMenuItem.PerformClick();
        }

        private void autoRestoreToolStripButton_Click(object sender, EventArgs e)
        {
            autoRestoreToolStripMenuItem.Checked = !autoRestoreToolStripMenuItem.Checked;
            //autoRestoreToolStripMenuItem.PerformClick();
        }

        private void renameToolStripButton_Click(object sender, EventArgs e)
        {
            renameSaveToolStripMenuItem.PerformClick();
        }

        private void editToolStripButton_Click(object sender, EventArgs e)
        {
            editSaveToolStripMenuItem.PerformClick();
        }

        private void importFromFileToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            importFromFileToolStripMenuItem.PerformClick();
        }

        private void importFromClipboardToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            importFromClipboardToolStripMenuItem.PerformClick();
        }

        private void exportToFileToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            exportToFileToolStripMenuItem.PerformClick();
        }

        private void exportToClipboardToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            exportToClipboardToolStripMenuItem.PerformClick();
        }

        private void openInExplorerToolStripButton_Click(object sender, EventArgs e)
        {
            openInExplorerToolStripMenuItem.PerformClick();
        }

        private void openBackupInExplorerToolStripButton_Click(object sender, EventArgs e)
        {
            openBackupInExplorerToolStripMenuItem.PerformClick();
        }

        private void cloneToolStripButton_Click(object sender, EventArgs e)
        {
            cloneToolStripMenuItem.PerformClick();
        }

        private void refreshToolStripButton_Click(object sender, EventArgs e)
        {
            refreshToolStripMenuItem.PerformClick();
        }
        #endregion


        #region WatcherEvents
        private async void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            if (e.FullPath.Contains("_BACKUP"))
            {
                return;
            }

            int count = Debouncer<FileChangedDebounceKey>.GetCount();

            WatcherLogger.Info("[{0}] Filtering file change for {1}", count, e.FullPath);
            Task<bool> task = Debouncer<FileChangedDebounceKey>.ThreadedDebounce();
            await task.ContinueWith(t =>
            {
                bool success = t.Result;
                if (!success)
                {
                    WatcherLogger.Info("[{0}] Debouncing file change for {1}", count, e.FullPath);
                    return;
                }

                WatcherLogger.Info("[{0}] Passed debounce check for {1}", count, e.FullPath);

                // If it was a main save file that has changed, update the modified on field in the listview.
                if (e.FullPath.EndsWith(".es3") && File.Exists(e.FullPath))
                {
                    OnFileChangedOrCreated(e);
                }
                else if (!File.Exists(e.FullPath))
                {
                    OnFileDeleted(e);
                }
            });
        }

        private void OnFileChangedOrCreated(FileSystemEventArgs e)
        {
            const int FILE_WAIT_TIME = 1000;
            const int FILE_WAIT_INTERVAL = 10;

            // True if the save already existed prior to this edit.
            bool found = false;

            // First, assume this is a modification of an existing save.
            for (int i = 0; i < _saves.Count; i++)
            {
                RepoSave curSave = _saves[i];
                if (curSave.MainFilePath == e.FullPath)
                {
                    found = true;
                    WatcherLogger.Info("Existing save found for file change {0}", e.FullPath);
                    bool backup = curSave.AutoWatchEnabled;
                    Invoke(() =>
                    {
                        UpdateSaveModifiedTime(i);
                        if (backup)
                        {
                            Backup(curSave, i);
                        }
                    });
                    break;
                }
            }

            // The save was not found, so it is new.
            if (!found)
            {
                FileInfo saveFileInfo = new FileInfo(e.FullPath);
                RepoSave newSave = new RepoSave(saveFileInfo.Directory!.FullName, e.FullPath, _repoDirectory!.BackupsDirectory);

                // Wait for REPO to create the save file.
                int totalWait = 0;
                int waitCount = 0;
                while (!File.Exists(newSave.MainFilePath) && totalWait < FILE_WAIT_TIME)
                {
                    waitCount++;
                    totalWait += FILE_WAIT_INTERVAL;
                    Thread.Sleep(FILE_WAIT_INTERVAL);
                }

                Thread.Sleep(1000);

                _saves.Add(newSave);
                Invoke(() => CreateListItem(newSave));
            }
        }

        private void OnFileDeleted(FileSystemEventArgs e)
        {
            WatcherLogger.Info("File {0} not found. Game over probable.", e.FullPath);

            for (int i = 0; i < _saves.Count; i++)
            {
                RepoSave curSave = _saves[i];

                if (curSave.MainFilePath == e.FullPath)
                {
                    if (!curSave.AutoWatchEnabled)
                    {
                        Invoke(() => RemoveListViewItem(i));
                    }
                    Invoke(() => Restore(curSave, i));
                }
            }
        }

        #endregion

        private void ResizeListView()
        {
            int columnCount = Lst_Saves.Columns.Count;
            foreach (ColumnHeader column in Lst_Saves.Columns)
            {
                column.Width = Lst_Saves.Width / columnCount;
            }
        }

        private void AutoDetectDirectory()
        {
            string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string localLow = Path.Combine(home, "AppData", "LocalLow");
            string repoDirectory = Path.Combine(localLow, "semiwork", "Repo", "saves");

            if (Directory.Exists(repoDirectory))
            {
                Txt_Repo.Text = repoDirectory;
            }
            else
            {
                Txt_Repo.Text = "<Directory not found>";
            }
        }

        private void ScanSaves()
        {
            if (_repoDirectory == null)
            {
                return;
            }


            _saves.AddRange(_repoDirectory.GetSaves());

            bool? outOfDatePromptResult = null;
            foreach (var save in _saves)
            {
                CreateListItem(save);

                if (!File.Exists(save.BackupFilePath))
                {
                    Backup(save, Lst_Saves.Items.Count - 1);
                }
                else if (File.GetLastAccessTime(save.BackupFilePath) < save.LastModifiedTime)
                {
                    if (outOfDatePromptResult == null)
                    {
                        outOfDatePromptResult = MessageBox.Show("One or more saves have backups that are out of date. Would you like to create backups for those saves now?", "Out Of Date Backups Detected", MessageBoxButtons.YesNo) == DialogResult.Yes;
                    }
                    if (outOfDatePromptResult.Value)
                    {
                        Backup(save, Lst_Saves.Items.Count - 1);
                    }
                }
            }
        }

        private void ScanOrphanBackups()
        {
            foreach (var archive in Directory.GetFiles(_repoDirectory!.BackupsDirectory))
            {
                string name = Path.GetFileNameWithoutExtension(archive);
                string expectedSaveDir = Path.Combine(_repoDirectory.SavesDirectory, name);

                if (!Directory.Exists(expectedSaveDir))
                {
                    CreateListItem(archive);
                }
            }
        }

        private void Backup(RepoSave save, int itemIndex)
        {
            save.Status = SaveStatus.Archiving;
            UpdateSaveStatus(save, itemIndex);

            _ = Task.Factory.StartNew(async s =>
            {
                var (forSave, index) = ((RepoSave, int))s!;

                bool success = await Archiving.Backup(forSave.FullDirectoryPath, forSave.BackupFilePath);
                if (success)
                {
                    forSave.Status = SaveStatus.Idle;
                    Invoke(() =>
                    {
                        UpdateSaveStatus(forSave, index);
                        UpdateBackupModifiedTime(forSave, index);
                        UpdateSaveName(forSave, index);
                    });
                }
            }, (save, itemIndex));
        }

        private void Restore(RepoSave save, int itemIndex)
        {
            save.Status = SaveStatus.Restoring;
            UpdateSaveStatus(save, itemIndex);

            Task.Factory.StartNew(async s =>
            {
                var (forSave, index) = ((RepoSave, int))s!;

                bool success = await Archiving.Restore(forSave.BackupFilePath, forSave.FullDirectoryPath);

                if (success)
                {
                    save.Status = SaveStatus.Idle;
                    Invoke(() =>
                    {
                        UpdateSaveName(forSave, index);
                        UpdateSaveStatus(forSave, index);
                        UpdateSaveName(forSave, index);
                    });
                }
            }, (save, itemIndex));
        }

        private void Restore(string archive, int itemIndex)
        {
            string nameStub = Path.GetFileNameWithoutExtension(archive);
            string targetPath = Path.Combine(_repoDirectory!.SavesDirectory, nameStub);

            RepoSave save = new RepoSave(targetPath, $"{nameStub}.es3", _repoDirectory!.BackupsDirectory);
            int saveIndex = _saves.Count;
            _saves.Add(save);

            Lst_Saves.Items[itemIndex].BackColor = Lst_Saves.BackColor;
            Lst_Saves.Items[itemIndex].ToolTipText = "";

            Archiving.Restore(archive, targetPath).ContinueWith(async t =>
            {
                bool success = await t;

            });

        }

        private void CreateListItem(RepoSave forSave)
        {
            DirectoryInfo saveDir = new DirectoryInfo(forSave.FullDirectoryPath);
            ListViewItem item = new ListViewItem(["", "", "", saveDir.Name, "Idle"]);
            item.Tag = new MainListItemTag(forSave);

            Lst_Saves.Items.Add(item);

            try
            {
                UpdateSaveName(forSave, Lst_Saves.Items.Count - 1);
                UpdateSaveModifiedTime(item.Index);
                UpdateBackupModifiedTime(forSave, item.Index);
            }
            catch (Exception ex)
            {
                UILogger.Error(ex, "Failed to create list item for save {0}!", forSave.MainFilePath);
            }
        }

        private void CreateListItem(string forOrphanArchive)
        {
            string name = Path.GetFileName(forOrphanArchive);
            DateTime lastBackedUp = File.GetLastWriteTime(forOrphanArchive);

            ListViewItem item = new ListViewItem([name, "", GetTimeString(lastBackedUp), "", "Idle"]);
            item.Tag = new MainListItemTag(forOrphanArchive);
            item.ToolTipText = "This is a backup with no corresponding active save file.";
            item.BackColor = Color.Orange;

            Lst_Saves.Items.Add(item);
        }

        private void RemoveListViewItem(int forSaveIndex)
        {
            Lst_Saves.Items.RemoveAt(forSaveIndex);
        }

        private void UpdateSaveName(RepoSave forSave, int forSaveIndex)
        {
            if (!File.Exists(forSave.MainFilePath))
            {
                return;
            }
            var data = EasySave.Read(forSave.MainFilePath);
            Lst_Saves.Items[forSaveIndex].Text = data["teamName"].ToString();
        }

        private void UpdateSaveModifiedTime(int forSaveIndex)
        {
            ListViewItem item = Lst_Saves.Items[forSaveIndex];
            item.SubItems[1].Text = GetTimeString(_saves[forSaveIndex].LastModifiedTime);
        }

        private void UpdateSaveStatus(RepoSave forSave, int forIndex)
        {
            ListViewItem item = Lst_Saves.Items[forIndex];
            item.SubItems[4].Text = forSave.Status.ToString();
        }

        private void UpdateBackupModifiedTime(RepoSave forSave, int forIndex)
        {
            ListViewItem item = Lst_Saves.Items[forIndex];

            if (File.Exists(forSave.BackupFilePath))
            {
                item.SubItems[2].Text = GetTimeString(File.GetLastWriteTime(forSave.BackupFilePath));
            }
            else
            {
                item.SubItems[2].Text = "";
            }
        }

        private string GetTimeString(DateTime time)
        {
            DateTime now = DateTime.Now;
            if (now.Day == time.Day && now.Month == time.Month && now.Year == time.Year)
            {
                return time.ToShortTimeString();
            }
            return $"{time.ToShortDateString()} {time.ToShortTimeString()}";
        }
    }
}
