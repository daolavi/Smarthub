namespace SmartHub.DbMigration.UI
{
    partial class Main
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ddlMigration = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.txtDatabase = new System.Windows.Forms.TextBox();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnCheckCurrent = new System.Windows.Forms.Button();
            this.grdMigrationMap = new System.Windows.Forms.DataGridView();
            this.btnMigrate = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtConsole = new System.Windows.Forms.RichTextBox();
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.mniProfiles = new System.Windows.Forms.ToolStripMenuItem();
            this.mniProfiles_Manage = new System.Windows.Forms.ToolStripMenuItem();
            this.mniAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.label8 = new System.Windows.Forms.Label();
            this.ddlProfiles = new System.Windows.Forms.ComboBox();
            this.grpConnection = new System.Windows.Forms.GroupBox();
            this.grpMigration = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grdMigrationMap)).BeginInit();
            this.mnuMain.SuspendLayout();
            this.grpConnection.SuspendLayout();
            this.grpMigration.SuspendLayout();
            this.SuspendLayout();
            // 
            // ddlMigration
            // 
            this.ddlMigration.DisplayMember = "MigrationName";
            this.ddlMigration.FormattingEnabled = true;
            this.ddlMigration.Items.AddRange(new object[] {
            "- No migration(s) found -"});
            this.ddlMigration.Location = new System.Drawing.Point(9, 19);
            this.ddlMigration.Name = "ddlMigration";
            this.ddlMigration.Size = new System.Drawing.Size(380, 21);
            this.ddlMigration.TabIndex = 5;
            this.ddlMigration.ValueMember = "AssemblyName";
            this.ddlMigration.SelectedIndexChanged += new System.EventHandler(this.DdlMigration_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Server";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(126, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Database";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(231, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "User";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(312, 64);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Password";
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(9, 80);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(114, 20);
            this.txtServer.TabIndex = 1;
            this.txtServer.Text = ".\\SQLEXPRESS";
            this.txtServer.TextChanged += new System.EventHandler(this.TxtServer_TextChanged);
            // 
            // txtDatabase
            // 
            this.txtDatabase.Location = new System.Drawing.Point(129, 80);
            this.txtDatabase.Name = "txtDatabase";
            this.txtDatabase.Size = new System.Drawing.Size(99, 20);
            this.txtDatabase.TabIndex = 2;
            this.txtDatabase.Text = "SmartHub";
            this.txtDatabase.TextChanged += new System.EventHandler(this.TxtDatabase_TextChanged);
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(234, 80);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(75, 20);
            this.txtUser.TabIndex = 3;
            this.txtUser.Text = "smarthub";
            this.txtUser.TextChanged += new System.EventHandler(this.TxtUser_TextChanged);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(315, 80);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(75, 20);
            this.txtPassword.TabIndex = 4;
            this.txtPassword.Text = "smarthub";
            this.txtPassword.TextChanged += new System.EventHandler(this.TxtPassword_TextChanged);
            // 
            // btnCheckCurrent
            // 
            this.btnCheckCurrent.Location = new System.Drawing.Point(159, 255);
            this.btnCheckCurrent.Name = "btnCheckCurrent";
            this.btnCheckCurrent.Size = new System.Drawing.Size(69, 23);
            this.btnCheckCurrent.TabIndex = 7;
            this.btnCheckCurrent.Text = "Check";
            this.btnCheckCurrent.UseVisualStyleBackColor = true;
            this.btnCheckCurrent.Click += new System.EventHandler(this.BtnCheckCurrent_Click);
            // 
            // grdMigrationMap
            // 
            this.grdMigrationMap.AllowUserToAddRows = false;
            this.grdMigrationMap.AllowUserToDeleteRows = false;
            this.grdMigrationMap.AllowUserToOrderColumns = true;
            this.grdMigrationMap.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.grdMigrationMap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdMigrationMap.DefaultCellStyle = dataGridViewCellStyle1;
            this.grdMigrationMap.Location = new System.Drawing.Point(9, 59);
            this.grdMigrationMap.MultiSelect = false;
            this.grdMigrationMap.Name = "grdMigrationMap";
            this.grdMigrationMap.ReadOnly = true;
            this.grdMigrationMap.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdMigrationMap.Size = new System.Drawing.Size(380, 190);
            this.grdMigrationMap.TabIndex = 6;
            this.grdMigrationMap.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.GrdMigrationMap_CellFormatting);
            // 
            // btnMigrate
            // 
            this.btnMigrate.Location = new System.Drawing.Point(232, 255);
            this.btnMigrate.Name = "btnMigrate";
            this.btnMigrate.Size = new System.Drawing.Size(158, 23);
            this.btnMigrate.TabIndex = 8;
            this.btnMigrate.Text = "Migrate to selected vesion";
            this.btnMigrate.UseVisualStyleBackColor = true;
            this.btnMigrate.Click += new System.EventHandler(this.BtnMigrate_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 255);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(147, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "* highlighted = current version";
            // 
            // txtConsole
            // 
            this.txtConsole.BackColor = System.Drawing.Color.Black;
            this.txtConsole.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConsole.ForeColor = System.Drawing.Color.Lime;
            this.txtConsole.HideSelection = false;
            this.txtConsole.Location = new System.Drawing.Point(1, 436);
            this.txtConsole.Name = "txtConsole";
            this.txtConsole.ReadOnly = true;
            this.txtConsole.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtConsole.Size = new System.Drawing.Size(421, 144);
            this.txtConsole.TabIndex = 9;
            this.txtConsole.Text = "";
            // 
            // mnuMain
            // 
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mniProfiles,
            this.mniAbout});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Size = new System.Drawing.Size(422, 24);
            this.mnuMain.TabIndex = 19;
            this.mnuMain.Text = "Main Menu";
            // 
            // mniProfiles
            // 
            this.mniProfiles.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mniProfiles_Manage});
            this.mniProfiles.ForeColor = System.Drawing.Color.Black;
            this.mniProfiles.Name = "mniProfiles";
            this.mniProfiles.Size = new System.Drawing.Size(58, 20);
            this.mniProfiles.Text = "&Profiles";
            // 
            // mniProfiles_Manage
            // 
            this.mniProfiles_Manage.BackColor = System.Drawing.SystemColors.Control;
            this.mniProfiles_Manage.ForeColor = System.Drawing.Color.Black;
            this.mniProfiles_Manage.Name = "mniProfiles_Manage";
            this.mniProfiles_Manage.Size = new System.Drawing.Size(117, 22);
            this.mniProfiles_Manage.Text = "&Manage";
            this.mniProfiles_Manage.Click += new System.EventHandler(this.MniProfiles_Manage_Click);
            // 
            // mniAbout
            // 
            this.mniAbout.ForeColor = System.Drawing.Color.Black;
            this.mniAbout.Name = "mniAbout";
            this.mniAbout.Size = new System.Drawing.Size(52, 20);
            this.mniAbout.Text = "&About";
            this.mniAbout.Click += new System.EventHandler(this.MniAbout_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 23);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(36, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "Profile";
            // 
            // ddlProfiles
            // 
            this.ddlProfiles.DisplayMember = "Name";
            this.ddlProfiles.FormattingEnabled = true;
            this.ddlProfiles.Items.AddRange(new object[] {
            "- No Profiles found -"});
            this.ddlProfiles.Location = new System.Drawing.Point(9, 40);
            this.ddlProfiles.Name = "ddlProfiles";
            this.ddlProfiles.Size = new System.Drawing.Size(383, 21);
            this.ddlProfiles.TabIndex = 0;
            this.ddlProfiles.ValueMember = "Id";
            this.ddlProfiles.SelectedIndexChanged += new System.EventHandler(this.DdlProfiles_SelectedIndexChanged);
            // 
            // grpConnection
            // 
            this.grpConnection.Controls.Add(this.ddlProfiles);
            this.grpConnection.Controls.Add(this.label8);
            this.grpConnection.Controls.Add(this.label2);
            this.grpConnection.Controls.Add(this.txtServer);
            this.grpConnection.Controls.Add(this.label3);
            this.grpConnection.Controls.Add(this.txtDatabase);
            this.grpConnection.Controls.Add(this.txtUser);
            this.grpConnection.Controls.Add(this.label5);
            this.grpConnection.Controls.Add(this.txtPassword);
            this.grpConnection.Controls.Add(this.label4);
            this.grpConnection.Location = new System.Drawing.Point(12, 33);
            this.grpConnection.Name = "grpConnection";
            this.grpConnection.Size = new System.Drawing.Size(398, 107);
            this.grpConnection.TabIndex = 22;
            this.grpConnection.TabStop = false;
            this.grpConnection.Text = "Connection";
            // 
            // grpMigration
            // 
            this.grpMigration.Controls.Add(this.label1);
            this.grpMigration.Controls.Add(this.btnMigrate);
            this.grpMigration.Controls.Add(this.label7);
            this.grpMigration.Controls.Add(this.ddlMigration);
            this.grpMigration.Controls.Add(this.btnCheckCurrent);
            this.grpMigration.Controls.Add(this.grdMigrationMap);
            this.grpMigration.Location = new System.Drawing.Point(12, 146);
            this.grpMigration.Name = "grpMigration";
            this.grpMigration.Size = new System.Drawing.Size(398, 284);
            this.grpMigration.TabIndex = 23;
            this.grpMigration.TabStop = false;
            this.grpMigration.Text = "Migration";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Database Version Info";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(422, 580);
            this.Controls.Add(this.grpMigration);
            this.Controls.Add(this.txtConsole);
            this.Controls.Add(this.mnuMain);
            this.Controls.Add(this.grpConnection);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MainMenuStrip = this.mnuMain;
            this.Name = "Main";
            this.Text = "DbMigrator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.grdMigrationMap)).EndInit();
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.grpConnection.ResumeLayout(false);
            this.grpConnection.PerformLayout();
            this.grpMigration.ResumeLayout(false);
            this.grpMigration.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox ddlMigration;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.TextBox txtDatabase;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnCheckCurrent;
        private System.Windows.Forms.DataGridView grdMigrationMap;
        private System.Windows.Forms.Button btnMigrate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RichTextBox txtConsole;
        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStripMenuItem mniProfiles;
        private System.Windows.Forms.ToolStripMenuItem mniProfiles_Manage;
        private System.Windows.Forms.ToolStripMenuItem mniAbout;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox ddlProfiles;
        private System.Windows.Forms.GroupBox grpConnection;
        private System.Windows.Forms.GroupBox grpMigration;
        private System.Windows.Forms.Label label1;
    }
}

