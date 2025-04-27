namespace REPOSaver
{
    partial class SaveEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            TreeNode treeNode1 = new TreeNode("Items");
            TreeNode treeNode2 = new TreeNode("I don't know what these do");
            TreeNode treeNode3 = new TreeNode("Global", new TreeNode[] { treeNode1, treeNode2 });
            TreeNode treeNode4 = new TreeNode("Players");
            TreeNode treeNode5 = new TreeNode("Unhandled");
            propertyGrid1 = new PropertyGrid();
            Mnu_Reset = new ContextMenuStrip(components);
            resetToolStripMenuItem = new ToolStripMenuItem();
            tableLayoutPanel1 = new TableLayoutPanel();
            Btn_Ok = new Button();
            Btn_Cancel = new Button();
            splitContainer1 = new SplitContainer();
            treeView1 = new TreeView();
            Mnu_Players = new ContextMenuStrip(components);
            addPlayerToolStripMenuItem = new ToolStripMenuItem();
            removePlayerToolStripMenuItem = new ToolStripMenuItem();
            Mnu_Reset.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            Mnu_Players.SuspendLayout();
            SuspendLayout();
            // 
            // propertyGrid1
            // 
            propertyGrid1.BackColor = SystemColors.Control;
            propertyGrid1.ContextMenuStrip = Mnu_Reset;
            propertyGrid1.Dock = DockStyle.Fill;
            propertyGrid1.Location = new Point(0, 0);
            propertyGrid1.Name = "propertyGrid1";
            propertyGrid1.Size = new Size(506, 407);
            propertyGrid1.TabIndex = 0;
            // 
            // Mnu_Reset
            // 
            Mnu_Reset.Items.AddRange(new ToolStripItem[] { resetToolStripMenuItem });
            Mnu_Reset.Name = "Mnu_Reset";
            Mnu_Reset.Size = new Size(103, 26);
            Mnu_Reset.Opening += Mnu_Reset_Opening;
            // 
            // resetToolStripMenuItem
            // 
            resetToolStripMenuItem.Name = "resetToolStripMenuItem";
            resetToolStripMenuItem.Size = new Size(102, 22);
            resetToolStripMenuItem.Text = "Reset";
            resetToolStripMenuItem.Click += resetValueToolStripMenuItem_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.Controls.Add(Btn_Ok, 0, 0);
            tableLayoutPanel1.Controls.Add(Btn_Cancel, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Bottom;
            tableLayoutPanel1.Location = new Point(0, 407);
            tableLayoutPanel1.Margin = new Padding(1);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RightToLeft = RightToLeft.Yes;
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(637, 27);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // Btn_Ok
            // 
            Btn_Ok.Location = new Point(547, 1);
            Btn_Ok.Margin = new Padding(1);
            Btn_Ok.Name = "Btn_Ok";
            Btn_Ok.Size = new Size(89, 21);
            Btn_Ok.TabIndex = 0;
            Btn_Ok.Text = "Ok";
            Btn_Ok.UseVisualStyleBackColor = true;
            Btn_Ok.Click += Btn_Ok_Click;
            // 
            // Btn_Cancel
            // 
            Btn_Cancel.Location = new Point(456, 1);
            Btn_Cancel.Margin = new Padding(1);
            Btn_Cancel.Name = "Btn_Cancel";
            Btn_Cancel.Size = new Size(89, 21);
            Btn_Cancel.TabIndex = 1;
            Btn_Cancel.Text = "Cancel";
            Btn_Cancel.UseVisualStyleBackColor = true;
            Btn_Cancel.Click += Btn_Cancel_Click;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Margin = new Padding(1);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(treeView1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(propertyGrid1);
            splitContainer1.Size = new Size(637, 407);
            splitContainer1.SplitterDistance = 129;
            splitContainer1.SplitterWidth = 2;
            splitContainer1.TabIndex = 2;
            // 
            // treeView1
            // 
            treeView1.Dock = DockStyle.Fill;
            treeView1.Location = new Point(0, 0);
            treeView1.Margin = new Padding(1);
            treeView1.Name = "treeView1";
            treeNode1.Name = "Node_Global_Items";
            treeNode1.Text = "Items";
            treeNode1.ToolTipText = "Item Settings";
            treeNode2.Name = "Node_Unknown";
            treeNode2.Text = "I don't know what these do";
            treeNode3.Name = "Node_Global";
            treeNode3.Text = "Global";
            treeNode3.ToolTipText = "Global Settings";
            treeNode4.Name = "Node_Players";
            treeNode4.Text = "Players";
            treeNode4.ToolTipText = "Player Settings";
            treeNode5.Name = "Node_Unhandled";
            treeNode5.Text = "Unhandled";
            treeView1.Nodes.AddRange(new TreeNode[] { treeNode3, treeNode4, treeNode5 });
            treeView1.Size = new Size(129, 407);
            treeView1.TabIndex = 0;
            treeView1.AfterSelect += treeView1_AfterSelect;
            // 
            // Mnu_Players
            // 
            Mnu_Players.ImageScalingSize = new Size(36, 36);
            Mnu_Players.Items.AddRange(new ToolStripItem[] { addPlayerToolStripMenuItem, removePlayerToolStripMenuItem });
            Mnu_Players.Name = "Mnu_Players";
            Mnu_Players.Size = new Size(153, 48);
            // 
            // addPlayerToolStripMenuItem
            // 
            addPlayerToolStripMenuItem.Name = "addPlayerToolStripMenuItem";
            addPlayerToolStripMenuItem.Size = new Size(152, 22);
            addPlayerToolStripMenuItem.Text = "Add Player";
            // 
            // removePlayerToolStripMenuItem
            // 
            removePlayerToolStripMenuItem.Name = "removePlayerToolStripMenuItem";
            removePlayerToolStripMenuItem.Size = new Size(152, 22);
            removePlayerToolStripMenuItem.Text = "Remove Player";
            // 
            // SaveEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(637, 434);
            Controls.Add(splitContainer1);
            Controls.Add(tableLayoutPanel1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SaveEditor";
            ShowIcon = false;
            Text = "Save Editor";
            Load += SaveEditor_Load;
            Mnu_Reset.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            Mnu_Players.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private PropertyGrid propertyGrid1;
        private TableLayoutPanel tableLayoutPanel1;
        private SplitContainer splitContainer1;
        private TreeView treeView1;
        private Button Btn_Ok;
        private Button Btn_Cancel;
        private ContextMenuStrip Mnu_Players;
        private ToolStripMenuItem addPlayerToolStripMenuItem;
        private ToolStripMenuItem removePlayerToolStripMenuItem;
        private ContextMenuStrip Mnu_Reset;
        private ToolStripMenuItem resetToolStripMenuItem;
    }
}