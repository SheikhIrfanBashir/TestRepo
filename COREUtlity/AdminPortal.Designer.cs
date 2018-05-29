namespace COREUtlity
{
    partial class AdminPortal
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
            this.cmbQueryList = new MetroFramework.Controls.MetroComboBox();
            this.btnCreateQuerylistTable = new MetroFramework.Controls.MetroButton();
            this.btnLoadQueries = new MetroFramework.Controls.MetroButton();
            this.txtComment = new MetroFramework.Controls.MetroTextBox();
            this.txtSQLQuery = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.btnAddQuery = new MetroFramework.Controls.MetroButton();
            this.btnEditQuery = new MetroFramework.Controls.MetroButton();
            this.btnDeleteQuery = new MetroFramework.Controls.MetroButton();
            this.btnBackToCore = new MetroFramework.Controls.MetroButton();
            this.btnExitApp = new MetroFramework.Controls.MetroButton();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnValidateQuery = new MetroFramework.Controls.MetroButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtPassword = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel6 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel5 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.cmbDatabases = new MetroFramework.Controls.MetroComboBox();
            this.btnConnectDB = new MetroFramework.Controls.MetroButton();
            this.txtUserID = new MetroFramework.Controls.MetroTextBox();
            this.btnLoadServers = new MetroFramework.Controls.MetroButton();
            this.metroProgressBar2 = new MetroFramework.Controls.MetroProgressBar();
            this.cmbSQLServer = new MetroFramework.Controls.MetroComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCopyQuery = new MetroFramework.Controls.MetroButton();
            this.btnClearQuery = new MetroFramework.Controls.MetroButton();
            this.btnLoadQueryDetails = new MetroFramework.Controls.MetroButton();
            this.btnDeleteQuerylistTable = new MetroFramework.Controls.MetroButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbQueryList
            // 
            this.cmbQueryList.FormattingEnabled = true;
            this.cmbQueryList.ItemHeight = 23;
            this.cmbQueryList.Location = new System.Drawing.Point(125, 37);
            this.cmbQueryList.Name = "cmbQueryList";
            this.cmbQueryList.Size = new System.Drawing.Size(686, 29);
            this.cmbQueryList.TabIndex = 0;
            // 
            // btnCreateQuerylistTable
            // 
            this.btnCreateQuerylistTable.Location = new System.Drawing.Point(433, 310);
            this.btnCreateQuerylistTable.Name = "btnCreateQuerylistTable";
            this.btnCreateQuerylistTable.Size = new System.Drawing.Size(151, 30);
            this.btnCreateQuerylistTable.TabIndex = 1;
            this.btnCreateQuerylistTable.Text = "Create Querylist Table";
            this.btnCreateQuerylistTable.Click += new System.EventHandler(this.btnCreateQuerylistTable_Click);
            // 
            // btnLoadQueries
            // 
            this.btnLoadQueries.Location = new System.Drawing.Point(288, 84);
            this.btnLoadQueries.Name = "btnLoadQueries";
            this.btnLoadQueries.Size = new System.Drawing.Size(145, 30);
            this.btnLoadQueries.TabIndex = 2;
            this.btnLoadQueries.Text = "Load Queries";
            this.btnLoadQueries.Click += new System.EventHandler(this.btnLoadQueries_Click);
            // 
            // txtComment
            // 
            this.txtComment.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtComment.Location = new System.Drawing.Point(125, 134);
            this.txtComment.Multiline = true;
            this.txtComment.Name = "txtComment";
            this.txtComment.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtComment.Size = new System.Drawing.Size(686, 48);
            this.txtComment.TabIndex = 3;
            // 
            // txtSQLQuery
            // 
            this.txtSQLQuery.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtSQLQuery.Location = new System.Drawing.Point(125, 188);
            this.txtSQLQuery.Multiline = true;
            this.txtSQLQuery.Name = "txtSQLQuery";
            this.txtSQLQuery.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSQLQuery.Size = new System.Drawing.Size(686, 86);
            this.txtSQLQuery.TabIndex = 4;
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(24, 134);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(77, 19);
            this.metroLabel1.TabIndex = 5;
            this.metroLabel1.Text = "COMMENT";
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(26, 213);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(80, 19);
            this.metroLabel2.TabIndex = 6;
            this.metroLabel2.Text = "SQL QUERY";
            // 
            // btnAddQuery
            // 
            this.btnAddQuery.Location = new System.Drawing.Point(125, 322);
            this.btnAddQuery.Name = "btnAddQuery";
            this.btnAddQuery.Size = new System.Drawing.Size(145, 30);
            this.btnAddQuery.TabIndex = 7;
            this.btnAddQuery.Text = "Add Query";
            this.btnAddQuery.Click += new System.EventHandler(this.btnAddQuery_Click);
            // 
            // btnEditQuery
            // 
            this.btnEditQuery.Location = new System.Drawing.Point(276, 322);
            this.btnEditQuery.Name = "btnEditQuery";
            this.btnEditQuery.Size = new System.Drawing.Size(145, 30);
            this.btnEditQuery.TabIndex = 8;
            this.btnEditQuery.Text = "Edit Query";
            this.btnEditQuery.Click += new System.EventHandler(this.btnEditQuery_Click);
            // 
            // btnDeleteQuery
            // 
            this.btnDeleteQuery.Location = new System.Drawing.Point(427, 322);
            this.btnDeleteQuery.Name = "btnDeleteQuery";
            this.btnDeleteQuery.Size = new System.Drawing.Size(145, 30);
            this.btnDeleteQuery.TabIndex = 9;
            this.btnDeleteQuery.Text = "Delete Query";
            this.btnDeleteQuery.Click += new System.EventHandler(this.btnDeleteQuery_Click);
            // 
            // btnBackToCore
            // 
            this.btnBackToCore.Location = new System.Drawing.Point(276, 310);
            this.btnBackToCore.Name = "btnBackToCore";
            this.btnBackToCore.Size = new System.Drawing.Size(151, 30);
            this.btnBackToCore.TabIndex = 10;
            this.btnBackToCore.Text = "Back To Core";
            this.btnBackToCore.Click += new System.EventHandler(this.btnBackToCore_Click);
            // 
            // btnExitApp
            // 
            this.btnExitApp.Location = new System.Drawing.Point(125, 310);
            this.btnExitApp.Name = "btnExitApp";
            this.btnExitApp.Size = new System.Drawing.Size(151, 30);
            this.btnExitApp.TabIndex = 11;
            this.btnExitApp.Text = "Exit App";
            this.btnExitApp.Click += new System.EventHandler(this.btnExitApp_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::COREUtlity.Properties.Resources.data_migration_multiple_db_actual_image;
            this.pictureBox2.Location = new System.Drawing.Point(23, 87);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(142, 363);
            this.pictureBox2.TabIndex = 14;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::COREUtlity.Properties.Resources.icon2;
            this.pictureBox1.Location = new System.Drawing.Point(177, 17);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 50);
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // btnValidateQuery
            // 
            this.btnValidateQuery.Location = new System.Drawing.Point(427, 286);
            this.btnValidateQuery.Name = "btnValidateQuery";
            this.btnValidateQuery.Size = new System.Drawing.Size(151, 30);
            this.btnValidateQuery.TabIndex = 15;
            this.btnValidateQuery.Text = "Validate Query";
            this.btnValidateQuery.Click += new System.EventHandler(this.btnValidateQuery_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnDeleteQuerylistTable);
            this.panel1.Controls.Add(this.txtPassword);
            this.panel1.Controls.Add(this.metroLabel6);
            this.panel1.Controls.Add(this.metroLabel5);
            this.panel1.Controls.Add(this.btnCreateQuerylistTable);
            this.panel1.Controls.Add(this.metroLabel4);
            this.panel1.Controls.Add(this.metroLabel3);
            this.panel1.Controls.Add(this.cmbDatabases);
            this.panel1.Controls.Add(this.btnBackToCore);
            this.panel1.Controls.Add(this.btnExitApp);
            this.panel1.Controls.Add(this.btnConnectDB);
            this.panel1.Controls.Add(this.txtUserID);
            this.panel1.Controls.Add(this.btnLoadServers);
            this.panel1.Controls.Add(this.metroProgressBar2);
            this.panel1.Controls.Add(this.cmbSQLServer);
            this.panel1.Location = new System.Drawing.Point(171, 87);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(743, 363);
            this.panel1.TabIndex = 16;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(125, 179);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(548, 23);
            this.txtPassword.TabIndex = 29;
            // 
            // metroLabel6
            // 
            this.metroLabel6.AutoSize = true;
            this.metroLabel6.Location = new System.Drawing.Point(24, 254);
            this.metroLabel6.Name = "metroLabel6";
            this.metroLabel6.Size = new System.Drawing.Size(68, 19);
            this.metroLabel6.TabIndex = 28;
            this.metroLabel6.Text = "Databases";
            // 
            // metroLabel5
            // 
            this.metroLabel5.AutoSize = true;
            this.metroLabel5.Location = new System.Drawing.Point(24, 179);
            this.metroLabel5.Name = "metroLabel5";
            this.metroLabel5.Size = new System.Drawing.Size(79, 19);
            this.metroLabel5.TabIndex = 27;
            this.metroLabel5.Text = "PASSWORD";
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.Location = new System.Drawing.Point(24, 140);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(56, 19);
            this.metroLabel4.TabIndex = 26;
            this.metroLabel4.Text = "USER ID";
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(24, 16);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(82, 19);
            this.metroLabel3.TabIndex = 25;
            this.metroLabel3.Text = "SQL SERVER";
            // 
            // cmbDatabases
            // 
            this.cmbDatabases.FormattingEnabled = true;
            this.cmbDatabases.ItemHeight = 23;
            this.cmbDatabases.Location = new System.Drawing.Point(125, 254);
            this.cmbDatabases.Name = "cmbDatabases";
            this.cmbDatabases.Size = new System.Drawing.Size(548, 29);
            this.cmbDatabases.TabIndex = 24;
            // 
            // btnConnectDB
            // 
            this.btnConnectDB.Location = new System.Drawing.Point(259, 208);
            this.btnConnectDB.Name = "btnConnectDB";
            this.btnConnectDB.Size = new System.Drawing.Size(127, 30);
            this.btnConnectDB.TabIndex = 23;
            this.btnConnectDB.Text = "Connect";
            this.btnConnectDB.Click += new System.EventHandler(this.btnConnectDB_Click);
            // 
            // txtUserID
            // 
            this.txtUserID.Location = new System.Drawing.Point(125, 140);
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new System.Drawing.Size(548, 23);
            this.txtUserID.TabIndex = 21;
            // 
            // btnLoadServers
            // 
            this.btnLoadServers.Location = new System.Drawing.Point(259, 94);
            this.btnLoadServers.Name = "btnLoadServers";
            this.btnLoadServers.Size = new System.Drawing.Size(115, 30);
            this.btnLoadServers.TabIndex = 20;
            this.btnLoadServers.Text = "Load Servers";
            this.btnLoadServers.Click += new System.EventHandler(this.btnLoadServers_Click);
            // 
            // metroProgressBar2
            // 
            this.metroProgressBar2.Location = new System.Drawing.Point(125, 51);
            this.metroProgressBar2.Name = "metroProgressBar2";
            this.metroProgressBar2.Size = new System.Drawing.Size(548, 23);
            this.metroProgressBar2.TabIndex = 17;
            // 
            // cmbSQLServer
            // 
            this.cmbSQLServer.FormattingEnabled = true;
            this.cmbSQLServer.ItemHeight = 23;
            this.cmbSQLServer.Location = new System.Drawing.Point(125, 16);
            this.cmbSQLServer.Name = "cmbSQLServer";
            this.cmbSQLServer.Size = new System.Drawing.Size(548, 29);
            this.cmbSQLServer.TabIndex = 16;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnCopyQuery);
            this.panel2.Controls.Add(this.btnClearQuery);
            this.panel2.Controls.Add(this.btnLoadQueryDetails);
            this.panel2.Controls.Add(this.btnDeleteQuery);
            this.panel2.Controls.Add(this.btnValidateQuery);
            this.panel2.Controls.Add(this.cmbQueryList);
            this.panel2.Controls.Add(this.btnLoadQueries);
            this.panel2.Controls.Add(this.txtComment);
            this.panel2.Controls.Add(this.metroLabel1);
            this.panel2.Controls.Add(this.metroLabel2);
            this.panel2.Controls.Add(this.btnAddQuery);
            this.panel2.Controls.Add(this.btnEditQuery);
            this.panel2.Controls.Add(this.txtSQLQuery);
            this.panel2.Location = new System.Drawing.Point(171, 473);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1075, 369);
            this.panel2.TabIndex = 17;
            // 
            // btnCopyQuery
            // 
            this.btnCopyQuery.Location = new System.Drawing.Point(578, 286);
            this.btnCopyQuery.Name = "btnCopyQuery";
            this.btnCopyQuery.Size = new System.Drawing.Size(151, 30);
            this.btnCopyQuery.TabIndex = 18;
            this.btnCopyQuery.Text = "Copy Query";
            this.btnCopyQuery.Click += new System.EventHandler(this.btnCopyQuery_Click);
            // 
            // btnClearQuery
            // 
            this.btnClearQuery.Location = new System.Drawing.Point(276, 286);
            this.btnClearQuery.Name = "btnClearQuery";
            this.btnClearQuery.Size = new System.Drawing.Size(145, 30);
            this.btnClearQuery.TabIndex = 17;
            this.btnClearQuery.Text = "Clear Text";
            this.btnClearQuery.Click += new System.EventHandler(this.btnClearQuery_Click);
            // 
            // btnLoadQueryDetails
            // 
            this.btnLoadQueryDetails.Location = new System.Drawing.Point(125, 286);
            this.btnLoadQueryDetails.Name = "btnLoadQueryDetails";
            this.btnLoadQueryDetails.Size = new System.Drawing.Size(145, 30);
            this.btnLoadQueryDetails.TabIndex = 16;
            this.btnLoadQueryDetails.Text = "Load Query Details";
            this.btnLoadQueryDetails.Click += new System.EventHandler(this.btnLoadQueryDetails_Click);
            // 
            // btnDeleteQuerylistTable
            // 
            this.btnDeleteQuerylistTable.Location = new System.Drawing.Point(590, 310);
            this.btnDeleteQuerylistTable.Name = "btnDeleteQuerylistTable";
            this.btnDeleteQuerylistTable.Size = new System.Drawing.Size(133, 30);
            this.btnDeleteQuerylistTable.TabIndex = 19;
            this.btnDeleteQuerylistTable.Text = "Restore QueryList table";
            this.btnDeleteQuerylistTable.Click += new System.EventHandler(this.btnDeleteQuerylistTable_Click);
            // 
            // AdminPortal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 895);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Name = "AdminPortal";
            this.Text = "AdminPortal";
            this.Load += new System.EventHandler(this.AdminPortal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroComboBox cmbQueryList;
        private MetroFramework.Controls.MetroButton btnCreateQuerylistTable;
        private MetroFramework.Controls.MetroButton btnLoadQueries;
        private MetroFramework.Controls.MetroTextBox txtComment;
        private MetroFramework.Controls.MetroTextBox txtSQLQuery;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroButton btnAddQuery;
        private MetroFramework.Controls.MetroButton btnEditQuery;
        private MetroFramework.Controls.MetroButton btnDeleteQuery;
        private MetroFramework.Controls.MetroButton btnBackToCore;
        private MetroFramework.Controls.MetroButton btnExitApp;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private MetroFramework.Controls.MetroButton btnValidateQuery;
        private System.Windows.Forms.Panel panel1;
        private MetroFramework.Controls.MetroProgressBar metroProgressBar2;
        private MetroFramework.Controls.MetroComboBox cmbSQLServer;
        private MetroFramework.Controls.MetroButton btnLoadServers;
        private MetroFramework.Controls.MetroTextBox txtUserID;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroComboBox cmbDatabases;
        private MetroFramework.Controls.MetroButton btnConnectDB;
        private MetroFramework.Controls.MetroLabel metroLabel5;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private MetroFramework.Controls.MetroLabel metroLabel6;
        private System.Windows.Forms.Panel panel2;
        private MetroFramework.Controls.MetroTextBox txtPassword;
        private MetroFramework.Controls.MetroButton btnClearQuery;
        private MetroFramework.Controls.MetroButton btnLoadQueryDetails;
        private MetroFramework.Controls.MetroButton btnCopyQuery;
        private MetroFramework.Controls.MetroButton btnDeleteQuerylistTable;
    }
}