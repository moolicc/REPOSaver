namespace REPOSaver
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            Txt_Repo = new TextBox();
            label1 = new Label();
            Btn_Directory_Auto = new Button();
            Btn_Directory_Browse = new Button();
            groupBox1 = new GroupBox();
            Lst_Saves = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader5 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            columnHeader4 = new ColumnHeader();
            Tls_ListItem = new ToolStrip();
            restoreNowToolStripButton = new ToolStripButton();
            backupNowToolStripButton = new ToolStripButton();
            autoRestoreToolStripButton = new ToolStripButton();
            toolStripSeparator5 = new ToolStripSeparator();
            renameToolStripButton = new ToolStripButton();
            editToolStripButton = new ToolStripButton();
            toolStripSeparator6 = new ToolStripSeparator();
            toolStripDropDownButton1 = new ToolStripDropDownButton();
            importFromFileToolStripMenuItem2 = new ToolStripMenuItem();
            importFromClipboardToolStripMenuItem2 = new ToolStripMenuItem();
            toolStripDropDownButton2 = new ToolStripDropDownButton();
            exportToFileToolStripMenuItem2 = new ToolStripMenuItem();
            exportToClipboardToolStripMenuItem2 = new ToolStripMenuItem();
            toolStripSeparator7 = new ToolStripSeparator();
            openInExplorerToolStripButton = new ToolStripButton();
            openBackupInExplorerToolStripButton = new ToolStripButton();
            toolStripSeparator8 = new ToolStripSeparator();
            cloneToolStripButton = new ToolStripButton();
            refreshToolStripButton = new ToolStripButton();
            tableLayoutPanel1 = new TableLayoutPanel();
            Mnu_ListItem = new ContextMenuStrip(components);
            restoreNowToolStripMenuItem = new ToolStripMenuItem();
            backupNowToolStripMenuItem = new ToolStripMenuItem();
            autoRestoreToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            renameSaveToolStripMenuItem = new ToolStripMenuItem();
            editSaveToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            importJSONToolStripMenuItem = new ToolStripMenuItem();
            importFromFileToolStripMenuItem = new ToolStripMenuItem();
            importFromClipboardToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator9 = new ToolStripSeparator();
            newFromFileToolStripMenuItem = new ToolStripMenuItem();
            newFromClipboardToolStripMenuItem = new ToolStripMenuItem();
            exportJSONToolStripMenuItem = new ToolStripMenuItem();
            exportToFileToolStripMenuItem = new ToolStripMenuItem();
            exportToClipboardToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator3 = new ToolStripSeparator();
            openInExplorerToolStripMenuItem = new ToolStripMenuItem();
            openBackupInExplorerToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator4 = new ToolStripSeparator();
            cloneToolStripMenuItem = new ToolStripMenuItem();
            refreshToolStripMenuItem = new ToolStripMenuItem();
            Sdlg_ExportJson = new SaveFileDialog();
            Odlg_ImportJson = new OpenFileDialog();
            Mnu_OrphanArchive = new ContextMenuStrip(components);
            moa_restoreNowToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator10 = new ToolStripSeparator();
            moa_openBackupInExplorerToolStripMenuItem = new ToolStripMenuItem();
            groupBox1.SuspendLayout();
            Tls_ListItem.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            Mnu_ListItem.SuspendLayout();
            Mnu_OrphanArchive.SuspendLayout();
            SuspendLayout();
            // 
            // Txt_Repo
            // 
            Txt_Repo.Dock = DockStyle.Fill;
            Txt_Repo.Location = new Point(3, 18);
            Txt_Repo.Name = "Txt_Repo";
            Txt_Repo.Size = new Size(574, 23);
            Txt_Repo.TabIndex = 0;
            Txt_Repo.TextChanged += Txt_Repo_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(81, 15);
            label1.TabIndex = 1;
            label1.Text = "Save directory";
            // 
            // Btn_Directory_Auto
            // 
            Btn_Directory_Auto.Location = new Point(583, 18);
            Btn_Directory_Auto.Name = "Btn_Directory_Auto";
            Btn_Directory_Auto.Size = new Size(80, 23);
            Btn_Directory_Auto.TabIndex = 2;
            Btn_Directory_Auto.Text = "Auto-detect";
            Btn_Directory_Auto.UseVisualStyleBackColor = true;
            Btn_Directory_Auto.Click += Btn_Directory_Auto_Click;
            // 
            // Btn_Directory_Browse
            // 
            Btn_Directory_Browse.Location = new Point(669, 18);
            Btn_Directory_Browse.Name = "Btn_Directory_Browse";
            Btn_Directory_Browse.Size = new Size(75, 23);
            Btn_Directory_Browse.TabIndex = 3;
            Btn_Directory_Browse.Text = "Browse";
            Btn_Directory_Browse.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(Lst_Saves);
            groupBox1.Controls.Add(Tls_ListItem);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Location = new Point(0, 49);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(747, 458);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            groupBox1.Text = "Saves";
            // 
            // Lst_Saves
            // 
            Lst_Saves.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader5, columnHeader3, columnHeader4 });
            Lst_Saves.Dock = DockStyle.Fill;
            Lst_Saves.FullRowSelect = true;
            Lst_Saves.LabelEdit = true;
            Lst_Saves.Location = new Point(3, 44);
            Lst_Saves.Name = "Lst_Saves";
            Lst_Saves.Size = new Size(741, 411);
            Lst_Saves.TabIndex = 0;
            Lst_Saves.UseCompatibleStateImageBehavior = false;
            Lst_Saves.View = View.Details;
            Lst_Saves.AfterLabelEdit += Lst_Saves_AfterLabelEdit;
            Lst_Saves.SelectedIndexChanged += Lst_Saves_SelectedIndexChanged;
            Lst_Saves.MouseClick += Lst_Saves_MouseClick;
            Lst_Saves.MouseDoubleClick += Lst_Saves_MouseDoubleClick;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Name";
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Last Modified";
            // 
            // columnHeader5
            // 
            columnHeader5.Text = "Last Backup";
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "Directory";
            // 
            // columnHeader4
            // 
            columnHeader4.Text = "Status";
            // 
            // Tls_ListItem
            // 
            Tls_ListItem.Items.AddRange(new ToolStripItem[] { restoreNowToolStripButton, backupNowToolStripButton, autoRestoreToolStripButton, toolStripSeparator5, renameToolStripButton, editToolStripButton, toolStripSeparator6, toolStripDropDownButton1, toolStripDropDownButton2, toolStripSeparator7, openInExplorerToolStripButton, openBackupInExplorerToolStripButton, toolStripSeparator8, cloneToolStripButton, refreshToolStripButton });
            Tls_ListItem.Location = new Point(3, 19);
            Tls_ListItem.Name = "Tls_ListItem";
            Tls_ListItem.Size = new Size(741, 25);
            Tls_ListItem.TabIndex = 1;
            Tls_ListItem.Text = "toolStrip1";
            // 
            // restoreNowToolStripButton
            // 
            restoreNowToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            restoreNowToolStripButton.Enabled = false;
            restoreNowToolStripButton.Image = Properties.Resources.Input;
            restoreNowToolStripButton.ImageTransparentColor = Color.Magenta;
            restoreNowToolStripButton.Name = "restoreNowToolStripButton";
            restoreNowToolStripButton.Size = new Size(23, 22);
            restoreNowToolStripButton.Text = "Restore Now";
            restoreNowToolStripButton.Click += restoreNowToolStripButton_Click;
            // 
            // backupNowToolStripButton
            // 
            backupNowToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            backupNowToolStripButton.Enabled = false;
            backupNowToolStripButton.Image = Properties.Resources.Output;
            backupNowToolStripButton.ImageTransparentColor = Color.Magenta;
            backupNowToolStripButton.Name = "backupNowToolStripButton";
            backupNowToolStripButton.Size = new Size(23, 22);
            backupNowToolStripButton.Text = "Backup Now";
            backupNowToolStripButton.Click += backupNowToolStripButton_Click;
            // 
            // autoRestoreToolStripButton
            // 
            autoRestoreToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            autoRestoreToolStripButton.Enabled = false;
            autoRestoreToolStripButton.Image = Properties.Resources.Refresh;
            autoRestoreToolStripButton.ImageTransparentColor = Color.Magenta;
            autoRestoreToolStripButton.Name = "autoRestoreToolStripButton";
            autoRestoreToolStripButton.Size = new Size(23, 22);
            autoRestoreToolStripButton.Text = "Auto Restore";
            autoRestoreToolStripButton.Click += autoRestoreToolStripButton_Click;
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new Size(6, 25);
            // 
            // renameToolStripButton
            // 
            renameToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            renameToolStripButton.Enabled = false;
            renameToolStripButton.Image = Properties.Resources.Rename;
            renameToolStripButton.ImageTransparentColor = Color.Magenta;
            renameToolStripButton.Name = "renameToolStripButton";
            renameToolStripButton.Size = new Size(23, 22);
            renameToolStripButton.Text = "Rename Save";
            renameToolStripButton.Click += renameToolStripButton_Click;
            // 
            // editToolStripButton
            // 
            editToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            editToolStripButton.Enabled = false;
            editToolStripButton.Image = Properties.Resources.Edit;
            editToolStripButton.ImageTransparentColor = Color.Magenta;
            editToolStripButton.Name = "editToolStripButton";
            editToolStripButton.Size = new Size(23, 22);
            editToolStripButton.Text = "Edit Save";
            editToolStripButton.Click += editToolStripButton_Click;
            // 
            // toolStripSeparator6
            // 
            toolStripSeparator6.Name = "toolStripSeparator6";
            toolStripSeparator6.Size = new Size(6, 25);
            // 
            // toolStripDropDownButton1
            // 
            toolStripDropDownButton1.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripDropDownButton1.DropDownItems.AddRange(new ToolStripItem[] { importFromFileToolStripMenuItem2, importFromClipboardToolStripMenuItem2 });
            toolStripDropDownButton1.Image = Properties.Resources.Import;
            toolStripDropDownButton1.ImageTransparentColor = Color.Magenta;
            toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            toolStripDropDownButton1.Size = new Size(29, 22);
            toolStripDropDownButton1.Text = "Import JSON";
            // 
            // importFromFileToolStripMenuItem2
            // 
            importFromFileToolStripMenuItem2.Enabled = false;
            importFromFileToolStripMenuItem2.Image = Properties.Resources.OpenFile;
            importFromFileToolStripMenuItem2.Name = "importFromFileToolStripMenuItem2";
            importFromFileToolStripMenuItem2.Size = new Size(196, 22);
            importFromFileToolStripMenuItem2.Text = "Import From File";
            importFromFileToolStripMenuItem2.Click += importFromFileToolStripMenuItem2_Click;
            // 
            // importFromClipboardToolStripMenuItem2
            // 
            importFromClipboardToolStripMenuItem2.Enabled = false;
            importFromClipboardToolStripMenuItem2.Image = Properties.Resources.Paste;
            importFromClipboardToolStripMenuItem2.Name = "importFromClipboardToolStripMenuItem2";
            importFromClipboardToolStripMenuItem2.Size = new Size(196, 22);
            importFromClipboardToolStripMenuItem2.Text = "Import From Clipboard";
            importFromClipboardToolStripMenuItem2.Click += importFromClipboardToolStripMenuItem2_Click;
            // 
            // toolStripDropDownButton2
            // 
            toolStripDropDownButton2.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripDropDownButton2.DropDownItems.AddRange(new ToolStripItem[] { exportToFileToolStripMenuItem2, exportToClipboardToolStripMenuItem2 });
            toolStripDropDownButton2.Image = Properties.Resources.Export;
            toolStripDropDownButton2.ImageTransparentColor = Color.Magenta;
            toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            toolStripDropDownButton2.Size = new Size(29, 22);
            toolStripDropDownButton2.Text = "Export JSON";
            // 
            // exportToFileToolStripMenuItem2
            // 
            exportToFileToolStripMenuItem2.Enabled = false;
            exportToFileToolStripMenuItem2.Image = Properties.Resources.Save;
            exportToFileToolStripMenuItem2.Name = "exportToFileToolStripMenuItem2";
            exportToFileToolStripMenuItem2.Size = new Size(178, 22);
            exportToFileToolStripMenuItem2.Text = "Export To File";
            exportToFileToolStripMenuItem2.Click += exportToFileToolStripMenuItem2_Click;
            // 
            // exportToClipboardToolStripMenuItem2
            // 
            exportToClipboardToolStripMenuItem2.Enabled = false;
            exportToClipboardToolStripMenuItem2.Image = Properties.Resources.Copy;
            exportToClipboardToolStripMenuItem2.Name = "exportToClipboardToolStripMenuItem2";
            exportToClipboardToolStripMenuItem2.Size = new Size(178, 22);
            exportToClipboardToolStripMenuItem2.Text = "Export To Clipboard";
            exportToClipboardToolStripMenuItem2.Click += exportToClipboardToolStripMenuItem2_Click;
            // 
            // toolStripSeparator7
            // 
            toolStripSeparator7.Name = "toolStripSeparator7";
            toolStripSeparator7.Size = new Size(6, 25);
            // 
            // openInExplorerToolStripButton
            // 
            openInExplorerToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            openInExplorerToolStripButton.Enabled = false;
            openInExplorerToolStripButton.Image = Properties.Resources.OpenFolder;
            openInExplorerToolStripButton.ImageTransparentColor = Color.Magenta;
            openInExplorerToolStripButton.Name = "openInExplorerToolStripButton";
            openInExplorerToolStripButton.Size = new Size(23, 22);
            openInExplorerToolStripButton.Text = "Open in Explorer";
            openInExplorerToolStripButton.Click += openInExplorerToolStripButton_Click;
            // 
            // openBackupInExplorerToolStripButton
            // 
            openBackupInExplorerToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            openBackupInExplorerToolStripButton.Enabled = false;
            openBackupInExplorerToolStripButton.Image = Properties.Resources.OpenProjectFolder;
            openBackupInExplorerToolStripButton.ImageTransparentColor = Color.Magenta;
            openBackupInExplorerToolStripButton.Name = "openBackupInExplorerToolStripButton";
            openBackupInExplorerToolStripButton.Size = new Size(23, 22);
            openBackupInExplorerToolStripButton.Text = "Open Backup in Explorer";
            openBackupInExplorerToolStripButton.Click += openBackupInExplorerToolStripButton_Click;
            // 
            // toolStripSeparator8
            // 
            toolStripSeparator8.Name = "toolStripSeparator8";
            toolStripSeparator8.Size = new Size(6, 25);
            // 
            // cloneToolStripButton
            // 
            cloneToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            cloneToolStripButton.Enabled = false;
            cloneToolStripButton.Image = Properties.Resources.CopyItem;
            cloneToolStripButton.ImageTransparentColor = Color.Magenta;
            cloneToolStripButton.Name = "cloneToolStripButton";
            cloneToolStripButton.Size = new Size(23, 22);
            cloneToolStripButton.Text = "Clone";
            cloneToolStripButton.Click += cloneToolStripButton_Click;
            // 
            // refreshToolStripButton
            // 
            refreshToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            refreshToolStripButton.Image = Properties.Resources.Sync;
            refreshToolStripButton.ImageTransparentColor = Color.Magenta;
            refreshToolStripButton.Name = "refreshToolStripButton";
            refreshToolStripButton.Size = new Size(23, 22);
            refreshToolStripButton.Text = "Refresh";
            refreshToolStripButton.Click += refreshToolStripButton_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(Txt_Repo, 0, 1);
            tableLayoutPanel1.Controls.Add(Btn_Directory_Browse, 2, 1);
            tableLayoutPanel1.Controls.Add(Btn_Directory_Auto, 1, 1);
            tableLayoutPanel1.Dock = DockStyle.Top;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(747, 49);
            tableLayoutPanel1.TabIndex = 5;
            // 
            // Mnu_ListItem
            // 
            Mnu_ListItem.Items.AddRange(new ToolStripItem[] { restoreNowToolStripMenuItem, backupNowToolStripMenuItem, autoRestoreToolStripMenuItem, toolStripSeparator1, renameSaveToolStripMenuItem, editSaveToolStripMenuItem, toolStripSeparator2, importJSONToolStripMenuItem, exportJSONToolStripMenuItem, toolStripSeparator3, openInExplorerToolStripMenuItem, openBackupInExplorerToolStripMenuItem, toolStripSeparator4, cloneToolStripMenuItem, refreshToolStripMenuItem });
            Mnu_ListItem.Name = "LstItm_ContextMenuStrip";
            Mnu_ListItem.Size = new Size(205, 270);
            // 
            // restoreNowToolStripMenuItem
            // 
            restoreNowToolStripMenuItem.Image = Properties.Resources.Input;
            restoreNowToolStripMenuItem.Name = "restoreNowToolStripMenuItem";
            restoreNowToolStripMenuItem.Size = new Size(204, 22);
            restoreNowToolStripMenuItem.Text = "Restore Now";
            restoreNowToolStripMenuItem.Click += restoreNowToolStripMenuItem_Click;
            // 
            // backupNowToolStripMenuItem
            // 
            backupNowToolStripMenuItem.Image = Properties.Resources.Output;
            backupNowToolStripMenuItem.Name = "backupNowToolStripMenuItem";
            backupNowToolStripMenuItem.Size = new Size(204, 22);
            backupNowToolStripMenuItem.Text = "Backup Now";
            backupNowToolStripMenuItem.Click += backupNowToolStripMenuItem_Click;
            // 
            // autoRestoreToolStripMenuItem
            // 
            autoRestoreToolStripMenuItem.Checked = true;
            autoRestoreToolStripMenuItem.CheckOnClick = true;
            autoRestoreToolStripMenuItem.CheckState = CheckState.Checked;
            autoRestoreToolStripMenuItem.Image = Properties.Resources.Refresh;
            autoRestoreToolStripMenuItem.Name = "autoRestoreToolStripMenuItem";
            autoRestoreToolStripMenuItem.Size = new Size(204, 22);
            autoRestoreToolStripMenuItem.Text = "Auto Restore";
            autoRestoreToolStripMenuItem.CheckedChanged += autoRestoreToolStripMenuItem_CheckedChanged;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(201, 6);
            // 
            // renameSaveToolStripMenuItem
            // 
            renameSaveToolStripMenuItem.Image = Properties.Resources.Rename;
            renameSaveToolStripMenuItem.Name = "renameSaveToolStripMenuItem";
            renameSaveToolStripMenuItem.Size = new Size(204, 22);
            renameSaveToolStripMenuItem.Text = "Rename Save";
            renameSaveToolStripMenuItem.Click += renameSaveToolStripMenuItem_Click;
            // 
            // editSaveToolStripMenuItem
            // 
            editSaveToolStripMenuItem.Image = Properties.Resources.Edit;
            editSaveToolStripMenuItem.Name = "editSaveToolStripMenuItem";
            editSaveToolStripMenuItem.Size = new Size(204, 22);
            editSaveToolStripMenuItem.Text = "Edit Save";
            editSaveToolStripMenuItem.Click += editSaveToolStripMenuItem_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(201, 6);
            // 
            // importJSONToolStripMenuItem
            // 
            importJSONToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { importFromFileToolStripMenuItem, importFromClipboardToolStripMenuItem, toolStripSeparator9, newFromFileToolStripMenuItem, newFromClipboardToolStripMenuItem });
            importJSONToolStripMenuItem.Image = Properties.Resources.Import;
            importJSONToolStripMenuItem.Name = "importJSONToolStripMenuItem";
            importJSONToolStripMenuItem.Size = new Size(204, 22);
            importJSONToolStripMenuItem.Text = "Import JSON";
            // 
            // importFromFileToolStripMenuItem
            // 
            importFromFileToolStripMenuItem.Image = Properties.Resources.OpenFile;
            importFromFileToolStripMenuItem.Name = "importFromFileToolStripMenuItem";
            importFromFileToolStripMenuItem.Size = new Size(184, 22);
            importFromFileToolStripMenuItem.Text = "From File";
            importFromFileToolStripMenuItem.Click += importFromFileToolStripMenuItem_Click;
            // 
            // importFromClipboardToolStripMenuItem
            // 
            importFromClipboardToolStripMenuItem.Image = Properties.Resources.Paste;
            importFromClipboardToolStripMenuItem.Name = "importFromClipboardToolStripMenuItem";
            importFromClipboardToolStripMenuItem.Size = new Size(184, 22);
            importFromClipboardToolStripMenuItem.Text = "From Clipboard";
            importFromClipboardToolStripMenuItem.Click += importFromClipboardToolStripMenuItem_Click;
            // 
            // toolStripSeparator9
            // 
            toolStripSeparator9.Name = "toolStripSeparator9";
            toolStripSeparator9.Size = new Size(181, 6);
            toolStripSeparator9.Visible = false;
            // 
            // newFromFileToolStripMenuItem
            // 
            newFromFileToolStripMenuItem.Name = "newFromFileToolStripMenuItem";
            newFromFileToolStripMenuItem.Size = new Size(184, 22);
            newFromFileToolStripMenuItem.Text = "New From File";
            newFromFileToolStripMenuItem.Visible = false;
            newFromFileToolStripMenuItem.Click += newFromFileToolStripMenuItem_Click;
            // 
            // newFromClipboardToolStripMenuItem
            // 
            newFromClipboardToolStripMenuItem.Name = "newFromClipboardToolStripMenuItem";
            newFromClipboardToolStripMenuItem.Size = new Size(184, 22);
            newFromClipboardToolStripMenuItem.Text = "New From Clipboard";
            newFromClipboardToolStripMenuItem.Visible = false;
            newFromClipboardToolStripMenuItem.Click += newFromClipboardToolStripMenuItem_Click;
            // 
            // exportJSONToolStripMenuItem
            // 
            exportJSONToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { exportToFileToolStripMenuItem, exportToClipboardToolStripMenuItem });
            exportJSONToolStripMenuItem.Image = Properties.Resources.Export;
            exportJSONToolStripMenuItem.Name = "exportJSONToolStripMenuItem";
            exportJSONToolStripMenuItem.Size = new Size(204, 22);
            exportJSONToolStripMenuItem.Text = "Export JSON";
            // 
            // exportToFileToolStripMenuItem
            // 
            exportToFileToolStripMenuItem.Image = Properties.Resources.Save;
            exportToFileToolStripMenuItem.Name = "exportToFileToolStripMenuItem";
            exportToFileToolStripMenuItem.Size = new Size(141, 22);
            exportToFileToolStripMenuItem.Text = "To File";
            exportToFileToolStripMenuItem.Click += exportToFileToolStripMenuItem_Click;
            // 
            // exportToClipboardToolStripMenuItem
            // 
            exportToClipboardToolStripMenuItem.Image = Properties.Resources.Copy;
            exportToClipboardToolStripMenuItem.Name = "exportToClipboardToolStripMenuItem";
            exportToClipboardToolStripMenuItem.Size = new Size(141, 22);
            exportToClipboardToolStripMenuItem.Text = "To Clipboard";
            exportToClipboardToolStripMenuItem.Click += exportToClipboardToolStripMenuItem_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(201, 6);
            // 
            // openInExplorerToolStripMenuItem
            // 
            openInExplorerToolStripMenuItem.Image = Properties.Resources.OpenFolder;
            openInExplorerToolStripMenuItem.Name = "openInExplorerToolStripMenuItem";
            openInExplorerToolStripMenuItem.Size = new Size(204, 22);
            openInExplorerToolStripMenuItem.Text = "Open in Explorer";
            openInExplorerToolStripMenuItem.Click += openInExplorerToolStripMenuItem_Click;
            // 
            // openBackupInExplorerToolStripMenuItem
            // 
            openBackupInExplorerToolStripMenuItem.Image = Properties.Resources.OpenProjectFolder;
            openBackupInExplorerToolStripMenuItem.Name = "openBackupInExplorerToolStripMenuItem";
            openBackupInExplorerToolStripMenuItem.Size = new Size(204, 22);
            openBackupInExplorerToolStripMenuItem.Text = "Open Backup in Explorer";
            openBackupInExplorerToolStripMenuItem.Click += openBackupInExplorerToolStripMenuItem_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(201, 6);
            // 
            // cloneToolStripMenuItem
            // 
            cloneToolStripMenuItem.Image = Properties.Resources.CopyItem;
            cloneToolStripMenuItem.Name = "cloneToolStripMenuItem";
            cloneToolStripMenuItem.Size = new Size(204, 22);
            cloneToolStripMenuItem.Text = "Clone";
            cloneToolStripMenuItem.Click += cloneToolStripMenuItem_Click;
            // 
            // refreshToolStripMenuItem
            // 
            refreshToolStripMenuItem.Image = Properties.Resources.Sync;
            refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            refreshToolStripMenuItem.Size = new Size(204, 22);
            refreshToolStripMenuItem.Text = "Refresh";
            refreshToolStripMenuItem.Click += refreshToolStripMenuItem_Click;
            // 
            // Sdlg_ExportJson
            // 
            Sdlg_ExportJson.Filter = "Json files|*.json|All files|*.*";
            Sdlg_ExportJson.Title = "Export Json";
            // 
            // Odlg_ImportJson
            // 
            Odlg_ImportJson.Filter = "Json files|*.json|All files|*.*";
            Odlg_ImportJson.Title = "Import Json";
            // 
            // Mnu_OrphanArchive
            // 
            Mnu_OrphanArchive.Items.AddRange(new ToolStripItem[] { moa_restoreNowToolStripMenuItem, toolStripSeparator10, moa_openBackupInExplorerToolStripMenuItem });
            Mnu_OrphanArchive.Name = "Mnu_OrphanArchive";
            Mnu_OrphanArchive.Size = new Size(205, 54);
            // 
            // moa_restoreNowToolStripMenuItem
            // 
            moa_restoreNowToolStripMenuItem.Image = Properties.Resources.Input;
            moa_restoreNowToolStripMenuItem.Name = "moa_restoreNowToolStripMenuItem";
            moa_restoreNowToolStripMenuItem.Size = new Size(204, 22);
            moa_restoreNowToolStripMenuItem.Text = "Restore Now";
            moa_restoreNowToolStripMenuItem.Click += moa_restoreNowToolStripMenuItem_Click;
            // 
            // toolStripSeparator10
            // 
            toolStripSeparator10.Name = "toolStripSeparator10";
            toolStripSeparator10.Size = new Size(201, 6);
            // 
            // moa_openBackupInExplorerToolStripMenuItem
            // 
            moa_openBackupInExplorerToolStripMenuItem.Image = Properties.Resources.OpenProjectFolder;
            moa_openBackupInExplorerToolStripMenuItem.Name = "moa_openBackupInExplorerToolStripMenuItem";
            moa_openBackupInExplorerToolStripMenuItem.Size = new Size(204, 22);
            moa_openBackupInExplorerToolStripMenuItem.Text = "Open Backup in Explorer";
            moa_openBackupInExplorerToolStripMenuItem.Click += moa_openBackupInExplorerToolStripMenuItem_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(747, 507);
            Controls.Add(groupBox1);
            Controls.Add(tableLayoutPanel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            Text = "REPOSaver";
            Load += Form1_Load;
            SizeChanged += Form1_SizeChanged;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            Tls_ListItem.ResumeLayout(false);
            Tls_ListItem.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            Mnu_ListItem.ResumeLayout(false);
            Mnu_OrphanArchive.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TextBox Txt_Repo;
        private Label label1;
        private Button Btn_Directory_Auto;
        private Button Btn_Directory_Browse;
        private GroupBox groupBox1;
        private TableLayoutPanel tableLayoutPanel1;
        private ListView Lst_Saves;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private ContextMenuStrip Mnu_ListItem;
        private ToolStripMenuItem restoreNowToolStripMenuItem;
        private ToolStripMenuItem backupNowToolStripMenuItem;
        private ToolStripMenuItem autoRestoreToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem openInExplorerToolStripMenuItem;
        private ToolStripMenuItem openBackupInExplorerToolStripMenuItem;
        private ColumnHeader columnHeader5;
        private ToolStripMenuItem editSaveToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem renameSaveToolStripMenuItem;
        private ToolStripMenuItem exportJSONToolStripMenuItem;
        private ToolStripMenuItem importJSONToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator3;
        private SaveFileDialog Sdlg_ExportJson;
        private ToolStripMenuItem exportToFileToolStripMenuItem;
        private ToolStripMenuItem exportToClipboardToolStripMenuItem;
        private ToolStripMenuItem importFromFileToolStripMenuItem;
        private ToolStripMenuItem importFromClipboardToolStripMenuItem;
        private OpenFileDialog Odlg_ImportJson;
        private ToolStrip Tls_ListItem;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripMenuItem cloneToolStripMenuItem;
        private ToolStripButton restoreNowToolStripButton;
        private ToolStripButton backupNowToolStripButton;
        private ToolStripButton autoRestoreToolStripButton;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripButton renameToolStripButton;
        private ToolStripButton editToolStripButton;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStripSeparator toolStripSeparator7;
        private ToolStripButton openInExplorerToolStripButton;
        private ToolStripButton openBackupInExplorerToolStripButton;
        private ToolStripSeparator toolStripSeparator8;
        private ToolStripButton cloneToolStripButton;
        private ToolStripDropDownButton toolStripDropDownButton1;
        private ToolStripMenuItem importFromFileToolStripMenuItem2;
        private ToolStripDropDownButton toolStripDropDownButton2;
        private ToolStripMenuItem importFromClipboardToolStripMenuItem2;
        private ToolStripMenuItem exportToFileToolStripMenuItem2;
        private ToolStripMenuItem exportToClipboardToolStripMenuItem2;
        private ToolStripSeparator toolStripSeparator9;
        private ToolStripMenuItem newFromFileToolStripMenuItem;
        private ToolStripMenuItem newFromClipboardToolStripMenuItem;
        private ToolStripButton refreshToolStripButton;
        private ToolStripMenuItem refreshToolStripMenuItem;
        private ContextMenuStrip Mnu_OrphanArchive;
        private ToolStripMenuItem moa_restoreNowToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator10;
        private ToolStripMenuItem moa_openBackupInExplorerToolStripMenuItem;
    }
}
