namespace COREUtlity
{
    partial class frmHome
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHome));
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.aDDDELETEDBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aDDDeleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.triggersExpiryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getDiscrepencyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutUSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aBOUTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.adminPortalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.adminPortalToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.metrobtnConnectCore = new MetroFramework.Controls.MetroButton();
            this.metrobtn_delete = new MetroFramework.Controls.MetroButton();
            this.metrobtnExpiry = new MetroFramework.Controls.MetroButton();
            this.metrobtnDeleteDatabaseSQLServer = new MetroFramework.Controls.MetroButton();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.cmbSQLServer = new MetroFramework.Controls.MetroComboBox();
            this.PBarSQLServer = new MetroFramework.Controls.MetroProgressBar();
            this.metroStyleManager1 = new MetroFramework.Components.MetroStyleManager(this.components);
            this.txtUserID = new MetroFramework.Controls.MetroTextBox();
            this.txtPasswordsss = new MetroFramework.Controls.MetroTextBox();
            this.btnLoadServers = new MetroFramework.Controls.MetroButton();
            this.btnConnectDB = new MetroFramework.Controls.MetroButton();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel5 = new MetroFramework.Controls.MetroLabel();
            this.cmbDatabases = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel6 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel7 = new MetroFramework.Controls.MetroLabel();
            this.cmbFullAccessUser = new MetroFramework.Controls.MetroComboBox();
            this.txtCurrentCoreLoginUsersss = new MetroFramework.Controls.MetroTextBox();
            this.btlLoadFullAccessUsers = new MetroFramework.Controls.MetroButton();
            this.btnMakeCoreLoginUsers = new MetroFramework.Controls.MetroButton();
            this.btnExitApp = new MetroFramework.Controls.MetroButton();
            this.btnGenerateDiscrepency = new MetroFramework.Controls.MetroButton();
            this.metroLabel8 = new MetroFramework.Controls.MetroLabel();
            this.PBarDiscrepency = new MetroFramework.Controls.MetroProgressBar();
            this.txtCoreAccountss = new MetroFramework.Controls.MetroTextBox();
            this.richTxtDiscrepency = new System.Windows.Forms.RichTextBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.metroPanel2 = new MetroFramework.Controls.MetroPanel();
            this.metroPanel3 = new MetroFramework.Controls.MetroPanel();
            this.pictureBoxicon = new System.Windows.Forms.PictureBox();
            this.btnCopyDiscrepency = new MetroFramework.Controls.MetroButton();
            this.btnClearDiscrepency = new MetroFramework.Controls.MetroButton();
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.metroPanel2.SuspendLayout();
            this.metroPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxicon)).BeginInit();
            this.metroPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.label1.Cursor = System.Windows.Forms.Cursors.AppStarting;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(97, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "SQL_SERVER";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aDDDELETEDBToolStripMenuItem,
            this.triggersExpiryToolStripMenuItem,
            this.aboutUSToolStripMenuItem,
            this.adminPortalToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(20, 60);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1174, 24);
            this.menuStrip1.TabIndex = 14;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // aDDDELETEDBToolStripMenuItem
            // 
            this.aDDDELETEDBToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aDDDeleteToolStripMenuItem});
            this.aDDDELETEDBToolStripMenuItem.Name = "aDDDELETEDBToolStripMenuItem";
            this.aDDDELETEDBToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.aDDDELETEDBToolStripMenuItem.Text = "Core";
            this.aDDDELETEDBToolStripMenuItem.Click += new System.EventHandler(this.aDDDELETEDBToolStripMenuItem_Click);
            // 
            // aDDDeleteToolStripMenuItem
            // 
            this.aDDDeleteToolStripMenuItem.Name = "aDDDeleteToolStripMenuItem";
            this.aDDDeleteToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.aDDDeleteToolStripMenuItem.Text = "ADD/Delete";
            this.aDDDeleteToolStripMenuItem.Click += new System.EventHandler(this.aDDDeleteToolStripMenuItem_Click);
            // 
            // triggersExpiryToolStripMenuItem
            // 
            this.triggersExpiryToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.getDiscrepencyToolStripMenuItem});
            this.triggersExpiryToolStripMenuItem.Name = "triggersExpiryToolStripMenuItem";
            this.triggersExpiryToolStripMenuItem.Size = new System.Drawing.Size(88, 20);
            this.triggersExpiryToolStripMenuItem.Text = "BillQuick/AO";
            this.triggersExpiryToolStripMenuItem.Click += new System.EventHandler(this.triggersExpiryToolStripMenuItem_Click);
            // 
            // getDiscrepencyToolStripMenuItem
            // 
            this.getDiscrepencyToolStripMenuItem.Name = "getDiscrepencyToolStripMenuItem";
            this.getDiscrepencyToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.getDiscrepencyToolStripMenuItem.Text = "Goto BQ/AO";
            this.getDiscrepencyToolStripMenuItem.Click += new System.EventHandler(this.getDiscrepencyToolStripMenuItem_Click);
            // 
            // aboutUSToolStripMenuItem
            // 
            this.aboutUSToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aBOUTToolStripMenuItem});
            this.aboutUSToolStripMenuItem.Name = "aboutUSToolStripMenuItem";
            this.aboutUSToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.aboutUSToolStripMenuItem.Text = "About US";
            // 
            // aBOUTToolStripMenuItem
            // 
            this.aBOUTToolStripMenuItem.Name = "aBOUTToolStripMenuItem";
            this.aBOUTToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.aBOUTToolStripMenuItem.Text = "About US";
            this.aBOUTToolStripMenuItem.Click += new System.EventHandler(this.aBOUTToolStripMenuItem_Click);
            // 
            // adminPortalToolStripMenuItem
            // 
            this.adminPortalToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.adminPortalToolStripMenuItem1});
            this.adminPortalToolStripMenuItem.Name = "adminPortalToolStripMenuItem";
            this.adminPortalToolStripMenuItem.Size = new System.Drawing.Size(89, 20);
            this.adminPortalToolStripMenuItem.Text = "Admin Portal";
            // 
            // adminPortalToolStripMenuItem1
            // 
            this.adminPortalToolStripMenuItem1.Name = "adminPortalToolStripMenuItem1";
            this.adminPortalToolStripMenuItem1.Size = new System.Drawing.Size(144, 22);
            this.adminPortalToolStripMenuItem1.Text = "Admin Portal";
            this.adminPortalToolStripMenuItem1.Click += new System.EventHandler(this.adminPortalToolStripMenuItem1_Click);
            // 
            // metrobtnConnectCore
            // 
            this.metrobtnConnectCore.Location = new System.Drawing.Point(19, 271);
            this.metrobtnConnectCore.Name = "metrobtnConnectCore";
            this.metrobtnConnectCore.Size = new System.Drawing.Size(125, 35);
            this.metrobtnConnectCore.Style = MetroFramework.MetroColorStyle.Green;
            this.metrobtnConnectCore.TabIndex = 41;
            this.metrobtnConnectCore.Text = "Connect Core";
            this.metrobtnConnectCore.Click += new System.EventHandler(this.metrobtnConnectCore_Click);
            // 
            // metrobtn_delete
            // 
            this.metrobtn_delete.Location = new System.Drawing.Point(150, 271);
            this.metrobtn_delete.Name = "metrobtn_delete";
            this.metrobtn_delete.Size = new System.Drawing.Size(158, 35);
            this.metrobtn_delete.TabIndex = 42;
            this.metrobtn_delete.Text = "Delete Database from Core";
            this.metrobtn_delete.Click += new System.EventHandler(this.metrobtn_delete_Click);
            // 
            // metrobtnExpiry
            // 
            this.metrobtnExpiry.Location = new System.Drawing.Point(314, 271);
            this.metrobtnExpiry.Name = "metrobtnExpiry";
            this.metrobtnExpiry.Size = new System.Drawing.Size(131, 35);
            this.metrobtnExpiry.TabIndex = 43;
            this.metrobtnExpiry.Text = "Expend Expiry";
            this.metrobtnExpiry.Click += new System.EventHandler(this.metrobtnExpiry_Click);
            // 
            // metrobtnDeleteDatabaseSQLServer
            // 
            this.metrobtnDeleteDatabaseSQLServer.Location = new System.Drawing.Point(451, 271);
            this.metrobtnDeleteDatabaseSQLServer.Name = "metrobtnDeleteDatabaseSQLServer";
            this.metrobtnDeleteDatabaseSQLServer.Size = new System.Drawing.Size(147, 35);
            this.metrobtnDeleteDatabaseSQLServer.TabIndex = 44;
            this.metrobtnDeleteDatabaseSQLServer.Text = "Delete Database from SQL";
            this.metrobtnDeleteDatabaseSQLServer.Click += new System.EventHandler(this.metrobtnDeleteDatabaseSQLServer_Click);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(19, 86);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(47, 19);
            this.metroLabel1.TabIndex = 45;
            this.metroLabel1.Text = "UserID";
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(19, 121);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(63, 19);
            this.metroLabel2.TabIndex = 46;
            this.metroLabel2.Text = "Password";
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(19, 20);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(80, 19);
            this.metroLabel3.TabIndex = 47;
            this.metroLabel3.Text = "SQL Servers";
            // 
            // cmbSQLServer
            // 
            this.cmbSQLServer.FormattingEnabled = true;
            this.cmbSQLServer.ItemHeight = 23;
            this.cmbSQLServer.Location = new System.Drawing.Point(115, 14);
            this.cmbSQLServer.Name = "cmbSQLServer";
            this.cmbSQLServer.Size = new System.Drawing.Size(357, 29);
            this.cmbSQLServer.TabIndex = 48;
            this.cmbSQLServer.DropDown += new System.EventHandler(this.cmbSQLServer_DropDown);
            this.cmbSQLServer.Click += new System.EventHandler(this.cmbSQLServer_Click);
            // 
            // PBarSQLServer
            // 
            this.PBarSQLServer.Location = new System.Drawing.Point(115, 57);
            this.PBarSQLServer.Name = "PBarSQLServer";
            this.PBarSQLServer.Size = new System.Drawing.Size(360, 23);
            this.PBarSQLServer.TabIndex = 49;
            // 
            // metroStyleManager1
            // 
            this.metroStyleManager1.Owner = null;
            // 
            // txtUserID
            // 
            this.txtUserID.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtUserID.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtUserID.Location = new System.Drawing.Point(115, 86);
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new System.Drawing.Size(360, 23);
            this.txtUserID.TabIndex = 50;
            // 
            // txtPasswordsss
            // 
            this.txtPasswordsss.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtPasswordsss.Location = new System.Drawing.Point(115, 117);
            this.txtPasswordsss.Name = "txtPasswordsss";
            this.txtPasswordsss.Size = new System.Drawing.Size(360, 23);
            this.txtPasswordsss.TabIndex = 51;
            this.txtPasswordsss.TabStop = false;
            // 
            // btnLoadServers
            // 
            this.btnLoadServers.Location = new System.Drawing.Point(478, 14);
            this.btnLoadServers.Name = "btnLoadServers";
            this.btnLoadServers.Size = new System.Drawing.Size(96, 35);
            this.btnLoadServers.TabIndex = 52;
            this.btnLoadServers.Text = "Load Servers";
            this.btnLoadServers.Click += new System.EventHandler(this.btnLoadServers_Click);
            // 
            // btnConnectDB
            // 
            this.btnConnectDB.Location = new System.Drawing.Point(177, 150);
            this.btnConnectDB.Name = "btnConnectDB";
            this.btnConnectDB.Size = new System.Drawing.Size(147, 23);
            this.btnConnectDB.TabIndex = 53;
            this.btnConnectDB.Text = "Connect";
            this.btnConnectDB.Click += new System.EventHandler(this.btnConnectDB_Click);
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.Location = new System.Drawing.Point(19, 219);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(89, 19);
            this.metroLabel4.TabIndex = 54;
            this.metroLabel4.Text = "Core Account";
            // 
            // metroLabel5
            // 
            this.metroLabel5.AutoSize = true;
            this.metroLabel5.Location = new System.Drawing.Point(19, 189);
            this.metroLabel5.Name = "metroLabel5";
            this.metroLabel5.Size = new System.Drawing.Size(68, 19);
            this.metroLabel5.TabIndex = 55;
            this.metroLabel5.Text = "Databases";
            // 
            // cmbDatabases
            // 
            this.cmbDatabases.FormattingEnabled = true;
            this.cmbDatabases.ItemHeight = 23;
            this.cmbDatabases.Location = new System.Drawing.Point(115, 179);
            this.cmbDatabases.Name = "cmbDatabases";
            this.cmbDatabases.Size = new System.Drawing.Size(360, 29);
            this.cmbDatabases.TabIndex = 56;
            this.cmbDatabases.SelectedIndexChanged += new System.EventHandler(this.cmbDatabases_SelectedIndexChanged);
            // 
            // metroLabel6
            // 
            this.metroLabel6.AutoSize = true;
            this.metroLabel6.Location = new System.Drawing.Point(17, 64);
            this.metroLabel6.Name = "metroLabel6";
            this.metroLabel6.Size = new System.Drawing.Size(106, 19);
            this.metroLabel6.TabIndex = 58;
            this.metroLabel6.Text = "Full Access Users";
            // 
            // metroLabel7
            // 
            this.metroLabel7.AutoSize = true;
            this.metroLabel7.Location = new System.Drawing.Point(17, 32);
            this.metroLabel7.Name = "metroLabel7";
            this.metroLabel7.Size = new System.Drawing.Size(152, 19);
            this.metroLabel7.TabIndex = 59;
            this.metroLabel7.Text = "Current Core Login User";
            // 
            // cmbFullAccessUser
            // 
            this.cmbFullAccessUser.FormattingEnabled = true;
            this.cmbFullAccessUser.ItemHeight = 23;
            this.cmbFullAccessUser.Location = new System.Drawing.Point(175, 64);
            this.cmbFullAccessUser.Name = "cmbFullAccessUser";
            this.cmbFullAccessUser.Size = new System.Drawing.Size(343, 29);
            this.cmbFullAccessUser.TabIndex = 61;
            // 
            // txtCurrentCoreLoginUsersss
            // 
            this.txtCurrentCoreLoginUsersss.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtCurrentCoreLoginUsersss.Location = new System.Drawing.Point(175, 32);
            this.txtCurrentCoreLoginUsersss.Name = "txtCurrentCoreLoginUsersss";
            this.txtCurrentCoreLoginUsersss.Size = new System.Drawing.Size(343, 23);
            this.txtCurrentCoreLoginUsersss.TabIndex = 62;
            // 
            // btlLoadFullAccessUsers
            // 
            this.btlLoadFullAccessUsers.Location = new System.Drawing.Point(175, 114);
            this.btlLoadFullAccessUsers.Name = "btlLoadFullAccessUsers";
            this.btlLoadFullAccessUsers.Size = new System.Drawing.Size(145, 31);
            this.btlLoadFullAccessUsers.TabIndex = 63;
            this.btlLoadFullAccessUsers.Text = "Load Full Access Users";
            this.btlLoadFullAccessUsers.Click += new System.EventHandler(this.btlLoadFullAccessUsers_Click);
            // 
            // btnMakeCoreLoginUsers
            // 
            this.btnMakeCoreLoginUsers.Location = new System.Drawing.Point(326, 114);
            this.btnMakeCoreLoginUsers.Name = "btnMakeCoreLoginUsers";
            this.btnMakeCoreLoginUsers.Size = new System.Drawing.Size(139, 31);
            this.btnMakeCoreLoginUsers.TabIndex = 64;
            this.btnMakeCoreLoginUsers.Text = "Make Core Login User";
            this.btnMakeCoreLoginUsers.Click += new System.EventHandler(this.btnMakeCoreLoginUsers_Click);
            // 
            // btnExitApp
            // 
            this.btnExitApp.Location = new System.Drawing.Point(218, 11);
            this.btnExitApp.Name = "btnExitApp";
            this.btnExitApp.Size = new System.Drawing.Size(134, 31);
            this.btnExitApp.TabIndex = 66;
            this.btnExitApp.Text = "Exit Application";
            this.btnExitApp.Click += new System.EventHandler(this.btnExitApp_Click);
            // 
            // btnGenerateDiscrepency
            // 
            this.btnGenerateDiscrepency.Location = new System.Drawing.Point(45, 11);
            this.btnGenerateDiscrepency.Name = "btnGenerateDiscrepency";
            this.btnGenerateDiscrepency.Size = new System.Drawing.Size(170, 31);
            this.btnGenerateDiscrepency.TabIndex = 68;
            this.btnGenerateDiscrepency.Text = "Generate Discrepency";
            this.btnGenerateDiscrepency.Click += new System.EventHandler(this.btnGenerateDiscrepency_Click);
            // 
            // metroLabel8
            // 
            this.metroLabel8.AutoSize = true;
            this.metroLabel8.Location = new System.Drawing.Point(377, 29);
            this.metroLabel8.Name = "metroLabel8";
            this.metroLabel8.Size = new System.Drawing.Size(95, 19);
            this.metroLabel8.TabIndex = 69;
            this.metroLabel8.Text = "Version 1.1.0.50";
            // 
            // PBarDiscrepency
            // 
            this.PBarDiscrepency.Location = new System.Drawing.Point(36, 60);
            this.PBarDiscrepency.Name = "PBarDiscrepency";
            this.PBarDiscrepency.Size = new System.Drawing.Size(339, 23);
            this.PBarDiscrepency.TabIndex = 72;
            // 
            // txtCoreAccountss
            // 
            this.txtCoreAccountss.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtCoreAccountss.Location = new System.Drawing.Point(115, 219);
            this.txtCoreAccountss.Name = "txtCoreAccountss";
            this.txtCoreAccountss.Size = new System.Drawing.Size(360, 23);
            this.txtCoreAccountss.TabIndex = 73;
            // 
            // richTxtDiscrepency
            // 
            this.richTxtDiscrepency.Font = new System.Drawing.Font("Arial Rounded MT Bold", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTxtDiscrepency.ForeColor = System.Drawing.SystemColors.ControlText;
            this.richTxtDiscrepency.Location = new System.Drawing.Point(33, 97);
            this.richTxtDiscrepency.Name = "richTxtDiscrepency";
            this.richTxtDiscrepency.Size = new System.Drawing.Size(342, 182);
            this.richTxtDiscrepency.TabIndex = 74;
            this.richTxtDiscrepency.Text = "";
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(20, 101);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(141, 362);
            this.pictureBox1.TabIndex = 75;
            this.pictureBox1.TabStop = false;
            // 
            // metroPanel2
            // 
            this.metroPanel2.Controls.Add(this.txtCoreAccountss);
            this.metroPanel2.Controls.Add(this.cmbDatabases);
            this.metroPanel2.Controls.Add(this.metroLabel5);
            this.metroPanel2.Controls.Add(this.metroLabel4);
            this.metroPanel2.Controls.Add(this.btnConnectDB);
            this.metroPanel2.Controls.Add(this.btnLoadServers);
            this.metroPanel2.Controls.Add(this.txtPasswordsss);
            this.metroPanel2.Controls.Add(this.txtUserID);
            this.metroPanel2.Controls.Add(this.PBarSQLServer);
            this.metroPanel2.Controls.Add(this.metroLabel3);
            this.metroPanel2.Controls.Add(this.metroLabel2);
            this.metroPanel2.Controls.Add(this.metroLabel1);
            this.metroPanel2.Controls.Add(this.metrobtnDeleteDatabaseSQLServer);
            this.metroPanel2.Controls.Add(this.metrobtnExpiry);
            this.metroPanel2.Controls.Add(this.metrobtn_delete);
            this.metroPanel2.Controls.Add(this.metrobtnConnectCore);
            this.metroPanel2.Controls.Add(this.cmbSQLServer);
            this.metroPanel2.HorizontalScrollbarBarColor = true;
            this.metroPanel2.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel2.HorizontalScrollbarSize = 10;
            this.metroPanel2.Location = new System.Drawing.Point(164, 85);
            this.metroPanel2.Name = "metroPanel2";
            this.metroPanel2.Size = new System.Drawing.Size(616, 338);
            this.metroPanel2.TabIndex = 76;
            this.metroPanel2.VerticalScrollbarBarColor = true;
            this.metroPanel2.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel2.VerticalScrollbarSize = 10;
            // 
            // metroPanel3
            // 
            this.metroPanel3.Controls.Add(this.btnMakeCoreLoginUsers);
            this.metroPanel3.Controls.Add(this.btlLoadFullAccessUsers);
            this.metroPanel3.Controls.Add(this.txtCurrentCoreLoginUsersss);
            this.metroPanel3.Controls.Add(this.cmbFullAccessUser);
            this.metroPanel3.Controls.Add(this.metroLabel7);
            this.metroPanel3.Controls.Add(this.metroLabel6);
            this.metroPanel3.HorizontalScrollbarBarColor = true;
            this.metroPanel3.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel3.HorizontalScrollbarSize = 10;
            this.metroPanel3.Location = new System.Drawing.Point(166, 431);
            this.metroPanel3.Name = "metroPanel3";
            this.metroPanel3.Size = new System.Drawing.Size(549, 160);
            this.metroPanel3.TabIndex = 77;
            this.metroPanel3.VerticalScrollbarBarColor = true;
            this.metroPanel3.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel3.VerticalScrollbarSize = 10;
            // 
            // pictureBoxicon
            // 
            this.pictureBoxicon.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxicon.Image")));
            this.pictureBoxicon.Location = new System.Drawing.Point(118, 7);
            this.pictureBoxicon.Name = "pictureBoxicon";
            this.pictureBoxicon.Size = new System.Drawing.Size(71, 50);
            this.pictureBoxicon.TabIndex = 78;
            this.pictureBoxicon.TabStop = false;
            // 
            // btnCopyDiscrepency
            // 
            this.btnCopyDiscrepency.Location = new System.Drawing.Point(107, 300);
            this.btnCopyDiscrepency.Name = "btnCopyDiscrepency";
            this.btnCopyDiscrepency.Size = new System.Drawing.Size(105, 32);
            this.btnCopyDiscrepency.TabIndex = 65;
            this.btnCopyDiscrepency.Text = "Copy Discrepency";
            this.btnCopyDiscrepency.Click += new System.EventHandler(this.btnCopyDiscrepency_Click);
            // 
            // btnClearDiscrepency
            // 
            this.btnClearDiscrepency.Location = new System.Drawing.Point(218, 300);
            this.btnClearDiscrepency.Name = "btnClearDiscrepency";
            this.btnClearDiscrepency.Size = new System.Drawing.Size(126, 32);
            this.btnClearDiscrepency.TabIndex = 67;
            this.btnClearDiscrepency.Text = "Clear Discrepency";
            this.btnClearDiscrepency.Click += new System.EventHandler(this.btnClearDiscrepency_Click);
            // 
            // metroPanel1
            // 
            this.metroPanel1.Controls.Add(this.btnClearDiscrepency);
            this.metroPanel1.Controls.Add(this.btnCopyDiscrepency);
            this.metroPanel1.Controls.Add(this.richTxtDiscrepency);
            this.metroPanel1.Controls.Add(this.PBarDiscrepency);
            this.metroPanel1.Controls.Add(this.btnGenerateDiscrepency);
            this.metroPanel1.Controls.Add(this.btnExitApp);
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(789, 82);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(404, 399);
            this.metroPanel1.TabIndex = 79;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // frmHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1214, 616);
            this.Controls.Add(this.metroPanel1);
            this.Controls.Add(this.pictureBoxicon);
            this.Controls.Add(this.metroPanel3);
            this.Controls.Add(this.metroPanel2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.metroLabel8);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmHome";
            this.ShowInTaskbar = false;
            this.Text = "                        BQE Core Utility  ";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.metroPanel2.ResumeLayout(false);
            this.metroPanel2.PerformLayout();
            this.metroPanel3.ResumeLayout(false);
            this.metroPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxicon)).EndInit();
            this.metroPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem aDDDELETEDBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aDDDeleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem triggersExpiryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutUSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aBOUTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getDiscrepencyToolStripMenuItem;
        private MetroFramework.Controls.MetroButton metrobtnConnectCore;
        private MetroFramework.Controls.MetroButton metrobtn_delete;
        private MetroFramework.Controls.MetroButton metrobtnExpiry;
        private MetroFramework.Controls.MetroButton metrobtnDeleteDatabaseSQLServer;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroComboBox cmbSQLServer;
        private MetroFramework.Controls.MetroProgressBar PBarSQLServer;
        private MetroFramework.Components.MetroStyleManager metroStyleManager1;
        private MetroFramework.Controls.MetroTextBox txtUserID;
        private MetroFramework.Controls.MetroTextBox txtPasswordsss;
        private MetroFramework.Controls.MetroButton btnLoadServers;
        private MetroFramework.Controls.MetroButton btnConnectDB;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private MetroFramework.Controls.MetroLabel metroLabel5;
        private MetroFramework.Controls.MetroComboBox cmbDatabases;
        private MetroFramework.Controls.MetroLabel metroLabel6;
        private MetroFramework.Controls.MetroLabel metroLabel7;
        private MetroFramework.Controls.MetroComboBox cmbFullAccessUser;
        private MetroFramework.Controls.MetroTextBox txtCurrentCoreLoginUsersss;
        private MetroFramework.Controls.MetroButton btlLoadFullAccessUsers;
        private MetroFramework.Controls.MetroButton btnMakeCoreLoginUsers;
        private MetroFramework.Controls.MetroButton btnExitApp;
        private MetroFramework.Controls.MetroButton btnGenerateDiscrepency;
        private MetroFramework.Controls.MetroLabel metroLabel8;
        private MetroFramework.Controls.MetroProgressBar PBarDiscrepency;
        private MetroFramework.Controls.MetroTextBox txtCoreAccountss;
        private System.Windows.Forms.RichTextBox richTxtDiscrepency;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private MetroFramework.Controls.MetroPanel metroPanel2;
        private MetroFramework.Controls.MetroPanel metroPanel3;
        private System.Windows.Forms.PictureBox pictureBoxicon;
        private MetroFramework.Controls.MetroButton btnCopyDiscrepency;
        private MetroFramework.Controls.MetroButton btnClearDiscrepency;
        private MetroFramework.Controls.MetroPanel metroPanel1;
        private System.Windows.Forms.ToolStripMenuItem adminPortalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem adminPortalToolStripMenuItem1;
    }
}

