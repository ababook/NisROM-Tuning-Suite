namespace NisROM_Tuning_Suite
{
    partial class MainForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadROMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveROMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eCUDumpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loggerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eCUConnectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cANOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cANDumpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkDTCsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearDTCsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.btnBigDecrement = new System.Windows.Forms.Button();
            this.btnBigIncrement = new System.Windows.Forms.Button();
            this.btnDecrement = new System.Windows.Forms.Button();
            this.btnIncrement = new System.Windows.Forms.Button();
            this.rOMOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fixChecksumsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.cANOptionsToolStripMenuItem,
            this.rOMOptionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1713, 33);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadROMToolStripMenuItem,
            this.saveROMToolStripMenuItem,
            this.eCUDumpToolStripMenuItem,
            this.loggerToolStripMenuItem,
            this.eCUConnectToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(50, 29);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadROMToolStripMenuItem
            // 
            this.loadROMToolStripMenuItem.Name = "loadROMToolStripMenuItem";
            this.loadROMToolStripMenuItem.Size = new System.Drawing.Size(247, 30);
            this.loadROMToolStripMenuItem.Text = "Load ROM";
            this.loadROMToolStripMenuItem.Click += new System.EventHandler(this.loadROMToolStripMenuItem_Click);
            // 
            // saveROMToolStripMenuItem
            // 
            this.saveROMToolStripMenuItem.Name = "saveROMToolStripMenuItem";
            this.saveROMToolStripMenuItem.Size = new System.Drawing.Size(247, 30);
            this.saveROMToolStripMenuItem.Text = "Save ROM";
            this.saveROMToolStripMenuItem.Click += new System.EventHandler(this.saveROMToolStripMenuItem_Click);
            // 
            // eCUDumpToolStripMenuItem
            // 
            this.eCUDumpToolStripMenuItem.Name = "eCUDumpToolStripMenuItem";
            this.eCUDumpToolStripMenuItem.Size = new System.Drawing.Size(247, 30);
            this.eCUDumpToolStripMenuItem.Text = "Dump/Flash K-Line";
            this.eCUDumpToolStripMenuItem.Click += new System.EventHandler(this.eCUDumpToolStripMenuItem_Click);
            // 
            // loggerToolStripMenuItem
            // 
            this.loggerToolStripMenuItem.Name = "loggerToolStripMenuItem";
            this.loggerToolStripMenuItem.Size = new System.Drawing.Size(247, 30);
            this.loggerToolStripMenuItem.Text = "Logger";
            this.loggerToolStripMenuItem.Click += new System.EventHandler(this.loggerToolStripMenuItem_Click);
            // 
            // eCUConnectToolStripMenuItem
            // 
            this.eCUConnectToolStripMenuItem.Name = "eCUConnectToolStripMenuItem";
            this.eCUConnectToolStripMenuItem.Size = new System.Drawing.Size(247, 30);
            this.eCUConnectToolStripMenuItem.Text = "ECU Connect";
            this.eCUConnectToolStripMenuItem.Click += new System.EventHandler(this.eCUConnectToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configureToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(88, 29);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // configureToolStripMenuItem
            // 
            this.configureToolStripMenuItem.Name = "configureToolStripMenuItem";
            this.configureToolStripMenuItem.Size = new System.Drawing.Size(175, 30);
            this.configureToolStripMenuItem.Text = "Configure";
            this.configureToolStripMenuItem.Click += new System.EventHandler(this.configureToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(61, 29);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // cANOptionsToolStripMenuItem
            // 
            this.cANOptionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cANDumpToolStripMenuItem,
            this.checkDTCsToolStripMenuItem,
            this.clearDTCsToolStripMenuItem});
            this.cANOptionsToolStripMenuItem.Name = "cANOptionsToolStripMenuItem";
            this.cANOptionsToolStripMenuItem.Size = new System.Drawing.Size(129, 29);
            this.cANOptionsToolStripMenuItem.Text = "CAN Options";
            // 
            // cANDumpToolStripMenuItem
            // 
            this.cANDumpToolStripMenuItem.Name = "cANDumpToolStripMenuItem";
            this.cANDumpToolStripMenuItem.Size = new System.Drawing.Size(190, 30);
            this.cANDumpToolStripMenuItem.Text = "CAN Dump";
            this.cANDumpToolStripMenuItem.Click += new System.EventHandler(this.cANDumpToolStripMenuItem_Click);
            // 
            // checkDTCsToolStripMenuItem
            // 
            this.checkDTCsToolStripMenuItem.Name = "checkDTCsToolStripMenuItem";
            this.checkDTCsToolStripMenuItem.Size = new System.Drawing.Size(190, 30);
            this.checkDTCsToolStripMenuItem.Text = "Check DTCs";
            this.checkDTCsToolStripMenuItem.Click += new System.EventHandler(this.checkDTCsToolStripMenuItem_Click);
            // 
            // clearDTCsToolStripMenuItem
            // 
            this.clearDTCsToolStripMenuItem.Name = "clearDTCsToolStripMenuItem";
            this.clearDTCsToolStripMenuItem.Size = new System.Drawing.Size(190, 30);
            this.clearDTCsToolStripMenuItem.Text = "Clear DTCs";
            this.clearDTCsToolStripMenuItem.Click += new System.EventHandler(this.clearDTCsToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 33);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1713, 899);
            this.splitContainer1.SplitterDistance = 490;
            this.splitContainer1.TabIndex = 1;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(490, 899);
            this.treeView1.TabIndex = 0;
            this.treeView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseDoubleClick);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.btnBigDecrement);
            this.splitContainer2.Panel1.Controls.Add(this.btnBigIncrement);
            this.splitContainer2.Panel1.Controls.Add(this.btnDecrement);
            this.splitContainer2.Panel1.Controls.Add(this.btnIncrement);
            this.splitContainer2.Size = new System.Drawing.Size(1219, 899);
            this.splitContainer2.SplitterDistance = 31;
            this.splitContainer2.TabIndex = 0;
            // 
            // btnBigDecrement
            // 
            this.btnBigDecrement.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(1)))), ((int)(((byte)(1)))));
            this.btnBigDecrement.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnBigDecrement.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBigDecrement.Location = new System.Drawing.Point(174, 0);
            this.btnBigDecrement.Name = "btnBigDecrement";
            this.btnBigDecrement.Size = new System.Drawing.Size(58, 31);
            this.btnBigDecrement.TabIndex = 3;
            this.btnBigDecrement.Text = "--";
            this.btnBigDecrement.UseVisualStyleBackColor = false;
            this.btnBigDecrement.Click += new System.EventHandler(this.btnBigDecrement_Click);
            // 
            // btnBigIncrement
            // 
            this.btnBigIncrement.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(1)))), ((int)(((byte)(1)))));
            this.btnBigIncrement.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnBigIncrement.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBigIncrement.Location = new System.Drawing.Point(116, 0);
            this.btnBigIncrement.Name = "btnBigIncrement";
            this.btnBigIncrement.Size = new System.Drawing.Size(58, 31);
            this.btnBigIncrement.TabIndex = 2;
            this.btnBigIncrement.Text = "++";
            this.btnBigIncrement.UseVisualStyleBackColor = false;
            this.btnBigIncrement.Click += new System.EventHandler(this.btnBigIncrement_Click);
            // 
            // btnDecrement
            // 
            this.btnDecrement.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(1)))), ((int)(((byte)(1)))));
            this.btnDecrement.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnDecrement.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDecrement.Location = new System.Drawing.Point(58, 0);
            this.btnDecrement.Name = "btnDecrement";
            this.btnDecrement.Size = new System.Drawing.Size(58, 31);
            this.btnDecrement.TabIndex = 1;
            this.btnDecrement.Text = "-";
            this.btnDecrement.UseVisualStyleBackColor = false;
            this.btnDecrement.Click += new System.EventHandler(this.btnDecrement_Click);
            // 
            // btnIncrement
            // 
            this.btnIncrement.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(1)))), ((int)(((byte)(1)))));
            this.btnIncrement.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnIncrement.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIncrement.Location = new System.Drawing.Point(0, 0);
            this.btnIncrement.Name = "btnIncrement";
            this.btnIncrement.Size = new System.Drawing.Size(58, 31);
            this.btnIncrement.TabIndex = 0;
            this.btnIncrement.Text = "+";
            this.btnIncrement.UseVisualStyleBackColor = false;
            this.btnIncrement.Click += new System.EventHandler(this.btnIncrement_Click);
            // 
            // rOMOptionsToolStripMenuItem
            // 
            this.rOMOptionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fixChecksumsToolStripMenuItem});
            this.rOMOptionsToolStripMenuItem.Name = "rOMOptionsToolStripMenuItem";
            this.rOMOptionsToolStripMenuItem.Size = new System.Drawing.Size(134, 29);
            this.rOMOptionsToolStripMenuItem.Text = "ROM Options";
            // 
            // fixChecksumsToolStripMenuItem
            // 
            this.fixChecksumsToolStripMenuItem.Enabled = false;
            this.fixChecksumsToolStripMenuItem.Name = "fixChecksumsToolStripMenuItem";
            this.fixChecksumsToolStripMenuItem.Size = new System.Drawing.Size(212, 30);
            this.fixChecksumsToolStripMenuItem.Text = "Fix Checksums";
            this.fixChecksumsToolStripMenuItem.Click += new System.EventHandler(this.fixChecksumsToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1713, 932);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadROMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveROMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ToolStripMenuItem eCUDumpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configureToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button btnDecrement;
        private System.Windows.Forms.Button btnIncrement;
        private System.Windows.Forms.Button btnBigDecrement;
        private System.Windows.Forms.Button btnBigIncrement;
        private System.Windows.Forms.ToolStripMenuItem loggerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cANOptionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cANDumpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkDTCsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearDTCsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eCUConnectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rOMOptionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fixChecksumsToolStripMenuItem;
    }
}

