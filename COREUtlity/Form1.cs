using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;



namespace COREUtlity
{
    public partial class frmHome :MetroFramework.Forms.MetroForm
    {
        SqlDataSourceEnumerator servers;
        DataTable tableServers;
        String server;



        public frmHome()
        {
            InitializeComponent();
        }

        private void cmbSQLServer_DropDown(object sender, EventArgs e)
        {
            

        }

        private void cmbSQLServer_Click(object sender, EventArgs e)
        {


        }

        private void Form1_Load(object sender, EventArgs e)
        {

            //MaximizeBox = false;
        }
              


       
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        

        private void aDDDELETEDBToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void aDDDeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frmADDDELETE = frmHome.ActiveForm;
            frmADDDELETE.Show();
        }

        private void aBOUTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Developed by Sheikh Irfan Ahmad");
        }

        private void getDiscrepencyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Visible = false;

            frmDiscrepencies form2 = new frmDiscrepencies();
            form2.Show();
        }

        private void btnGetDiscrepency_Click(object sender, EventArgs e)
        {
            try
            {
                
                richTxtDiscrepency.Text = "";
                ClassCoreUtility.CaluculateAll(PBarDiscrepency);
                //ClassCoreUtility.CaluculateAll(pgrDiscrepency);
                if (string.IsNullOrEmpty(cmbDatabases.Text.ToString()) || string.IsNullOrEmpty(txtCoreAccountss.Text.ToString()))
                {
                    MessageBox.Show("Please select  Coredatabase and Account");
                    return;
                }

                string SQL;

                int count = 0;

                //Check whether Database exists on the SQL Server                

                if (!(ClassCoreUtility.checkIfDatabaseExistsOnSQL(cmbDatabases.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString())))
                {
                    MessageBox.Show("Database does not exist on SQL Server");
                    return;
                }


                //Check whether database is Core Database  

                if (!(ClassCoreUtility.checkIfCoreDatabase(cmbDatabases.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString())))
                {
                    MessageBox.Show("Selected Database not a Core Database!!");
                    return;
                }


                //Check whether Database exist in CORE

                if (!(ClassCoreUtility.checkIfDatabaseAlreadyAddedToCore(cmbDatabases.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString())))
                {
                    MessageBox.Show("Database does not exist  Core");
                    return;
                }

                //Get discrepency in Core database
                Dictionary<string, string> dctDiscrepency = ClassCoreUtility.GetDiscrepency(cmbDatabases.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString());


                foreach (var item in dctDiscrepency)
                {
                    string comment = item.Key.ToString();
                    int lencomment = comment.Length;

                    richTxtDiscrepency.Text += comment;

                    //richTxtDiscrepency.Select(richTxtDiscrepency.Text.IndexOf("--"), lencomment);
                    //richTxtDiscrepency.ForeColor = System.Drawing.Color.Green;                    

                    //richTxtDiscrepency.SelectionColor = Color.Green;
                    //richTxtDiscrepency.SelectedText = Environment.NewLine + item.Value;


                    richTxtDiscrepency.AppendText(Environment.NewLine);


                    int lenSQL = item.Value.Length;
                    richTxtDiscrepency.Text += item.Value.ToString();


                    richTxtDiscrepency.AppendText(Environment.NewLine);
                    richTxtDiscrepency.AppendText(Environment.NewLine);
                }


                if (richTxtDiscrepency.Text == "")
                {
                    MessageBox.Show("No Discrepency found!!");
                }
                else
                {
                    MessageBox.Show("Copy the Discrepencies!!");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

       

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (richTxtDiscrepency.Text == "")
            {
                MessageBox.Show("Nothing to be Copied");
            }
            else
            {
                //richTxtDiscrepency.SelectionStart = 1;
                //richTxtDiscrepency.SelectionLength = richTxtDiscrepency.TextLength;
                //richTxtDiscrepency.SelectionBackColor = Color.LightBlue;

                string text = richTxtDiscrepency.Text;
                Clipboard.SetText(text);
                MessageBox.Show("Text Copied!!");
            }
        }

        private void cmbDatabases_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCurrentCoreLoginUsersss.Text = "";
            cmbFullAccessUser.Text = string.Empty;
        }

       

        private void btnLoadFullAccessUser_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(ClassCoreUtility.checkIfDatabaseAlreadyAddedToCore(cmbDatabases.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString())))
                {
                    MessageBox.Show("Database does not exist in CORE!!");
                    return;
                }


                if (String.IsNullOrEmpty(txtCoreAccountss.Text.ToString()) || string.IsNullOrEmpty(cmbDatabases.Text.ToString()))
                {
                    MessageBox.Show(" Please select Account and Coredatabase");
                    return;
                }

                string SQL, connectString;
                List<string> lstFullAccessUsers = new List<string>();
                

                //Check whether Database exists on the SQL Server                

                if (!(ClassCoreUtility.checkIfDatabaseExistsOnSQL(cmbDatabases.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString())))
                {
                    MessageBox.Show("Database does not exist on SQL Server");
                    return;
                }


                //Check whether database is Core Database  

                if (!(ClassCoreUtility.checkIfCoreDatabase(cmbDatabases.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString())))
                {
                    MessageBox.Show("Selected Database not a Core Database!!");
                    return;
                }               


                //Check whether account present in CORE

                if (!(ClassCoreUtility.checkIfAccountPresentInCore(txtCoreAccountss.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString(), cmbDatabases.Text.ToString())))
                {
                    MessageBox.Show("Account does not exist in Core! Please give correct Account");
                    return;
                }


                //Check whether database has full access security employee

                if (!(ClassCoreUtility.checkIfCoreDBHasFullAccessUser(cmbDatabases.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString())))
                {
                    MessageBox.Show("No Employee with full access security/Set the security manually");
                    return;
                }


                // Set connection string with selected server and integrated security      
                connectString = "Data Source=" + this.server + ";Integrated Security=false;Initial Catalog=master;User ID=" +
                    txtUserID.Text + ";Password=" + txtPasswordsss.Text + ";";

                using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(connectString))
                {
                    // Open connection
                    con.Open();

                    //Get list of full access users
                    SQL = "SELECT EmployeeID FROM [" + cmbDatabases.Text + "].dbo.Employee WHERE Employee_ID IN (SELECT Employee_ID FROM [" + cmbDatabases.Text +  "].dbo.Security WHERE SecurityTemplate_ID IN(SELECT SecurityTemplate_ID FROM [" +cmbDatabases.Text + "]. dbo.SecurityTemplate WHERE SecurityTemplateName = 'Full Access'));";

                    SqlCommand com =
                                 new SqlCommand(SQL,con);
                    SqlDataReader dr = com.ExecuteReader();

                    while (dr.Read())
                    {
                        lstFullAccessUsers.Add(dr[0].ToString());
                    }

                    // Set databases list as combobox’s datasource
                    this.cmbFullAccessUser.DataSource = lstFullAccessUsers;
                    

                    //Get current login Core User
                    string st = ClassCoreUtility.getCurrentCoreLoginUser(cmbDatabases.Text, txtCoreAccountss.Text, this.server, txtUserID.Text, txtPasswordsss.Text);
                    txtCurrentCoreLoginUsersss.Text = st;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (richTxtDiscrepency.Text == "")
            {
                MessageBox.Show(" Discrepencies Already cleared");
            }
            else
            {
                richTxtDiscrepency.Text = "";
            }
        }

       
        private void txtCurrentCoreLoginUserss_TextChanged(object sender, EventArgs e)
        {

        }

       

       
        private void triggersExpiryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.Hide();
            //frmDiscrepencies fd = new frmDiscrepencies();
            //fd.Show();
        }

        private void metrobtnConnectCore_Click(object sender, EventArgs e)
        {
            try
            {
                txtCurrentCoreLoginUsersss.Text = "";
                cmbFullAccessUser.Text = string.Empty;
                richTxtDiscrepency.Text = "";

                if (String.IsNullOrEmpty(txtCoreAccountss.Text.ToString()) || string.IsNullOrEmpty(cmbDatabases.Text.ToString()))
                {
                    MessageBox.Show(" Please select Account and Coredatabase");
                    return;
                }

                string SQL;

                int count = 0;

                //Check whether Database exists on the SQL Server                

                if (!(ClassCoreUtility.checkIfDatabaseExistsOnSQL(cmbDatabases.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString())))
                {
                    MessageBox.Show("Database does not exist on SQL Server");
                    return;
                }


                //Check whether database is Core Database  

                if (!(ClassCoreUtility.checkIfCoreDatabase(cmbDatabases.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString())))
                {
                    MessageBox.Show("Selected Database not a Core Database!!");
                    return;
                }


                //Check whether Database exist in CORE

                if ((ClassCoreUtility.checkIfDatabaseAlreadyAddedToCore(cmbDatabases.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString())))
                {
                    MessageBox.Show("Database already added to Core");
                    return;
                }



                //Check whether account present in CORE

                if (!(ClassCoreUtility.checkIfAccountPresentInCore(txtCoreAccountss.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString(), cmbDatabases.Text.ToString())))
                {
                    MessageBox.Show("Account does not exist in Core! Please give correct Account");
                    return;
                }


                //Check whether database has full access security employee

                if (!(ClassCoreUtility.checkIfCoreDBHasFullAccessUser(cmbDatabases.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString())))
                {
                    MessageBox.Show("No Employee with full access security/Set the security manually");

                }

                //Add Database to the CORE

                SQL = "";
                SQL = "USE BQECoreHost;";
                SQL += " DECLARE @industry AS UNIQUEIDENTIFIER, @country AS UNIQUEIDENTIFIER, @version AS NVARCHAR(MAX),";
                SQL += " @DatabaseID AS NVARCHAR(MAX), @account AS UNIQUEIDENTIFIER, @company AS UNIQUEIDENTIFIER, @route AS UNIQUEIDENTIFIER,";
                SQL += "  @DatabaseName AS NVARCHAR(MAX), @password AS NVARCHAR(MAX), @EmployeeID AS NVARCHAR(MAX),  @DataFileLocation AS NVARCHAR(MAX),  @LogFileLocation AS NVARCHAR(MAX); ";
                SQL += "SET @industry =(SELECT TOP 1 ID FROM BQECoreMaster.dbo.Industry  WHERE SICDescription =" + "'" + "Computer Programmer" + "');";
                SQL += "SET @country =(    SELECT TOP 1 ID FROM BQECoreHost.dbo.Country WHERE Name ='" + "United States" + "');";
                SQL += "SET @version =(    SELECT TOP 1    CoreVersion    FROM BQECoreMaster.dbo.UpdateQuery    ORDER BY CreateDate DESC);";
                SQL += "SET @DatabaseID = '" + cmbDatabases.Text + "';";
                SQL += "SET @account =(    SELECT ID    FROM BQECoreHost.dbo.Account    WHERE Email = '" + txtCoreAccountss.Text + "');";
                SQL += "SET @password =(    SELECT Password    FROM BQECoreHost.dbo.Account    WHERE Email = '" + txtCoreAccountss.Text + "');";
                SQL += "SET @route =(    SELECT  ID FROM BQECoreHost.dbo.Route WHERE ID='88888888-4444-4444-4444-123456789012');";
                SQL += "SET @DataFileLocation  = '' + 'C:\\Program Files\\Microsoft SQL Server\\MSSQL13.MSSQLSERVER2016D\\MSSQL\\DATA\\" + cmbDatabases.Text + ".mdf';";

                SQL += "SET @LogFileLocation  = '' + 'C:\\Program Files\\Microsoft SQL Server\\MSSQL13.MSSQLSERVER2016D\\MSSQL\\DATA\\" + cmbDatabases.Text + "_log.ldf';";


                SQL += " INSERT INTO Company ( [ID], [Name], [Industry_ID], [Country_ID], [CreatedOn], [TrailExpDate], [CompanyStatus],		[StatusMessage],[CoreDBVersion],[Mode],[UpdatedBy],[UpdatedOn],[CreatedBy],	[HasArchive], [DatabaseID],       [CoreDeliveredVersion],[DataFileLocation],  [LogFileLocation] )";

                SQL += "VALUES (NEWID(),'" + cmbDatabases.Text + "'," + " @industry, @country, GETDATE(), DATEADD(yy, 20, GETDATE()), 2, NULL,@version,2,NULL,GETDATE(), @account,0,'" + cmbDatabases.Text + "', @version,@DataFileLocation, @LogFileLocation); ";

                SQL += "SET @company =(SELECT TOP 1 ID FROM BQECoreHost.dbo.Company WHERE DatabaseID = @DatabaseID); ";

                SQL += " INSERT INTO AccountCompany( [ID], [Account_ID], [UpdatedOn],[UserType], [CreatedOn], [Company_ID],[UserStatus],		[UpdatedBy],[CreatedBy],[IsDefault], [Route_ID])";
                SQL += "VALUES (NEWID(),@account, GETDATE(),0, GETDATE(), @company,0,@account,@account,NULL, @route);";

                SQL += "SET @EmployeeID=(SELECT TOP 1 Employee_ID FROM [" + cmbDatabases.Text + "].dbo.Employee WHERE HostAccount_ID IS NOT NULL AND Employee_ID IN(SELECT Employee_ID FROM [" + cmbDatabases.Text + "].dbo.Security WHERE SecurityTemplate_ID = '5DE836B0-F3F2-45DD-8A44-0C689E8B2ACD')); ";
                SQL += "UPDATE [" + cmbDatabases.Text + "].dbo.Employee  SET HostAccount_ID = @account,Password = @password    WHERE Employee_ID = @EmployeeID; ";

                count = ClassCoreUtility.executeSQLInsertUpdate(SQL, this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString());
                if (count > 0)
                    MessageBox.Show("Database added sucessfully to Core");
                else
                    MessageBox.Show("Database not added to Core");


            }
            catch (Exception ex)
            { MessageBox.Show(ex.ToString()); }
        }

        private void btnConnectDB_Click(object sender, EventArgs e)
        {
            try
            {
                richTxtDiscrepency.Text = "";

                string SQL;
                string connectString;
                List<string> listDataBases = new List<string>();

                // Check if user was selected a server to connect                  
                
                if (this.cmbSQLServer.Text == "")
                {
                    MessageBox.Show("You Must select a server/Server does not exists!!");
                    return;

                }

                this.server = this.cmbSQLServer.Text;

                if (String.IsNullOrEmpty(txtPasswordsss.Text.ToString()) || string.IsNullOrEmpty(txtUserID.Text.ToString()))
                    MessageBox.Show(" Please enter correct User credentials");


                // Set connection string with selected server and integrated security      
                connectString = "Data Source=" + this.server + ";Integrated Security=false;Initial Catalog=master;User ID=" +
                    txtUserID.Text + ";Password=" + txtPasswordsss.Text + ";";

                using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(connectString))
                {
                    // Open connection
                    con.Open();

                    //Get databases names in server in a datareader 
                    SQL = "select name from sys.databases ORDER BY name;";

                    System.Data.SqlClient.SqlCommand com =
                                 new System.Data.SqlClient.SqlCommand(SQL, con);
                    System.Data.SqlClient.SqlDataReader dr = com.ExecuteReader();

                    while (dr.Read())
                    {
                        listDataBases.Add(dr[0].ToString());
                    }

                    // Set databases list as combobox’s datasource
                    this.cmbDatabases.DataSource = listDataBases;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid Crdential!!");
            }

        }

        private void metrobtn_delete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show("Do you really want to delete  the Database from Core?", "Delete from Core", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {

                    txtCurrentCoreLoginUsersss.Text = "";
                    cmbFullAccessUser.Text = string.Empty;
                    richTxtDiscrepency.Text = "";
                    if (string.IsNullOrEmpty(cmbDatabases.Text.ToString()) || string.IsNullOrEmpty(txtCoreAccountss.Text.ToString()))
                    {
                        MessageBox.Show("Please select  Coredatabase and Account");
                        return;
                    }

                    string SQL;

                    int count = 0;

                    //Check whether Database exists on the SQL Server                

                    if (!(ClassCoreUtility.checkIfDatabaseExistsOnSQL(cmbDatabases.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString())))
                    {
                        MessageBox.Show("Database does not exist on SQL Server");
                        return;
                    }


                    //Check whether database is Core Database  

                    if (!(ClassCoreUtility.checkIfCoreDatabase(cmbDatabases.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString())))
                    {
                        MessageBox.Show("Selected Database not a Core Database!!");
                        return;
                    }


                    //Check whether Database exist in CORE

                    if (!(ClassCoreUtility.checkIfDatabaseAlreadyAddedToCore(cmbDatabases.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString())))
                    {
                        MessageBox.Show("Database does not exist  Core");
                        return;
                    }



                    //Check whether account present in CORE

                    if (!(ClassCoreUtility.checkIfAccountPresentInCore(txtCoreAccountss.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString(), cmbDatabases.Text.ToString())))
                    {
                        MessageBox.Show("Account does not exist in Core! Please give correct Account");
                        return;
                    }


                    //Delete database from Core
                    SQL = "";
                    SQL = "USE BQECoreHost;";
                    SQL += "delete FROM dbo.AccountCompany WHERE Company_ID IN(SELECT ID FROM dbo.Company WHERE DatabaseID='" + cmbDatabases.Text + "');";
                    SQL += "DELETE FROM company WHERE DatabaseID = '" + cmbDatabases.Text + "'; ";

                    count = ClassCoreUtility.executeSQLInsertUpdate(SQL, this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString());

                    if (count > 0)

                        MessageBox.Show("Database deleted sucessully from Core");
                    else
                        MessageBox.Show("Database not deleted");

                    //btnConnectDB.PerformClick();
                }
                

            }
            catch (Exception ex)
            { MessageBox.Show(ex.ToString()); }

        }

        private void btnGenerateDiscrepency_Click(object sender, EventArgs e)
        {
            try
            {

                richTxtDiscrepency.Text = "";
                ClassCoreUtility.CaluculateAll(PBarDiscrepency);
                //ClassCoreUtility.CaluculateAll(pgrDiscrepency);
                if (string.IsNullOrEmpty(cmbDatabases.Text.ToString()) || string.IsNullOrEmpty(txtCoreAccountss.Text.ToString()))
                {
                    MessageBox.Show("Please select  Coredatabase and Account");
                    return;
                }

                string SQL;

                int count = 0;

                //Check whether Database exists on the SQL Server                

                if (!(ClassCoreUtility.checkIfDatabaseExistsOnSQL(cmbDatabases.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString())))
                {
                    MessageBox.Show("Database does not exist on SQL Server");
                    return;
                }


                //Check whether database is Core Database  

                if (!(ClassCoreUtility.checkIfCoreDatabase(cmbDatabases.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString())))
                {
                    MessageBox.Show("Selected Database not a Core Database!!");
                    return;
                }


                //Check whether Database exist in CORE

                if (!(ClassCoreUtility.checkIfDatabaseAlreadyAddedToCore(cmbDatabases.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString())))
                {
                    MessageBox.Show("Database does not exist  Core");
                    return;
                }

                //Check whether Database Coreutility exists on the SQL Server                

                if (!(ClassCoreUtility.checkIfDatabaseExistsOnSQL("CoreUtility", cmbSQLServer.Text.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString())))
                {
                    MessageBox.Show("Create Querylist table first in Admin Portal!!");
                    return;
                }


                //Get discrepency in Core database
                Dictionary<string, string> dctDiscrepency = ClassCoreUtility.GetDiscrepency(cmbDatabases.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString());


                foreach (var item in dctDiscrepency)
                {
                    string comment = item.Key.ToString();
                    int lencomment = comment.Length;

                    richTxtDiscrepency.Text += comment;

                    //richTxtDiscrepency.Select(richTxtDiscrepency.Text.IndexOf("--"), lencomment);
                    //richTxtDiscrepency.ForeColor = System.Drawing.Color.Green;                    

                    //richTxtDiscrepency.SelectionColor = Color.Green;
                    //richTxtDiscrepency.SelectedText = Environment.NewLine + item.Value;


                    richTxtDiscrepency.AppendText(Environment.NewLine);
                    richTxtDiscrepency.AppendText(Environment.NewLine);

                    int lenSQL = item.Value.Length;
                    richTxtDiscrepency.Text += item.Value.ToString();

                    
                    richTxtDiscrepency.AppendText(Environment.NewLine);
                    richTxtDiscrepency.AppendText(Environment.NewLine);
                }


                if (richTxtDiscrepency.Text == "")
                {
                    MessageBox.Show("No Discrepency found!!");
                }
                else
                {
                    MessageBox.Show("Copy the Discrepencies!!");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void metrobtnExpiry_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtCoreAccountss.Text.ToString()) || string.IsNullOrEmpty(cmbDatabases.Text.ToString()))
            {
                MessageBox.Show(" Please select Account and Coredatabase");
                return;
            }

            string SQL;

            int count = 0;

            //Check whether Database exists on the SQL Server                

            if (!(ClassCoreUtility.checkIfDatabaseExistsOnSQL(cmbDatabases.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString())))
            {
                MessageBox.Show("Database does not exist on SQL Server");
                return;
            }


            //Check whether database is Core Database  

            if (!(ClassCoreUtility.checkIfCoreDatabase(cmbDatabases.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString())))
            {
                MessageBox.Show("Selected Database not a Core Database!!");
                return;
            }



            //Check whether account present in CORE

            if (!(ClassCoreUtility.checkIfAccountPresentInCore(txtCoreAccountss.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString(), cmbDatabases.Text.ToString())))
            {
                MessageBox.Show("Account does not exist in Core! Please give correct Account");
                return;
            }


            // Check whether Database exist in CORE
            if (!(ClassCoreUtility.checkIfDatabaseAlreadyAddedToCore(cmbDatabases.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString())))
            {
                MessageBox.Show("Database does not exist  Core");
                return;
            }

            SQL = "UPDATE BQECoreHost.dbo.Company SET CompanyStatus=2,TrailExpDate='2038-01-02 15:23:19.973' WHERE DatabaseID='" + cmbDatabases.Text + "';";
            count = ClassCoreUtility.executeSQLInsertUpdate(SQL, this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString());

            if (count > 0)

                MessageBox.Show("Expiry extended sucessully in Core");
            else
                MessageBox.Show("Opeartion not sucessfull");
        }

        private void btnCopyDiscrepency_Click(object sender, EventArgs e)
        {
            if (richTxtDiscrepency.Text == "")
            {
                MessageBox.Show("Nothing to be Copied");
            }
            else
            {
                richTxtDiscrepency.SelectionStart = 1;
                richTxtDiscrepency.SelectionLength = richTxtDiscrepency.TextLength;
                richTxtDiscrepency.SelectionBackColor = Color.LightBlue;

                string text = richTxtDiscrepency.Text;
                Clipboard.SetText(text);
                MessageBox.Show("Text Copied!!");
            }
        }
       

        private void btnLoadServers_Click(object sender, EventArgs e)
        {
            try
            {
                ClassCoreUtility.CaluculateAll(PBarSQLServer);
                // Create a instance of the SqlDataSourceEnumerator class
                servers = SqlDataSourceEnumerator.Instance;

                // Fetch all visible SQL server 2000 or SQL Server 2005 instances
                tableServers = new DataTable();

                // Check if datatable is empty
                if (tableServers.Rows.Count == 0)
                {
                    // Get a datatable with info about SQL Server 2000 and 2005 instances
                    tableServers = servers.GetDataSources();


                    // List that will be combobox’s datasource
                    List<string> listservers = new List<string>();


                    // For each element in the datatable add a new element in the list
                    foreach (DataRow rowServer in tableServers.Rows)
                    {

                        // Server instance could have instace name or only server name,
                        // check this for show the name
                        if (String.IsNullOrEmpty(rowServer["InstanceName"].ToString()))
                            listservers.Add(rowServer["ServerName"].ToString());
                        else
                            listservers.Add(rowServer["ServerName"] + "\\" + rowServer["InstanceName"]);
                    }
                                        
                    // Set servers list to combobox’s datasource
                    this.cmbSQLServer.DataSource = listservers;

                }



            }
            catch (Exception ex)
            { ex.ToString(); }
        }

        private void btlLoadFullAccessUsers_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(ClassCoreUtility.checkIfDatabaseAlreadyAddedToCore(cmbDatabases.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString())))
                {
                    MessageBox.Show("Database does not exist in CORE!!");
                    return;
                }


                if (String.IsNullOrEmpty(txtCoreAccountss.Text.ToString()) || string.IsNullOrEmpty(cmbDatabases.Text.ToString()))
                {
                    MessageBox.Show(" Please select Account and Coredatabase");
                    return;
                }

                string SQL, connectString;
                List<string> lstFullAccessUsers = new List<string>();


                //Check whether Database exists on the SQL Server                

                if (!(ClassCoreUtility.checkIfDatabaseExistsOnSQL(cmbDatabases.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString())))
                {
                    MessageBox.Show("Database does not exist on SQL Server");
                    return;
                }


                //Check whether database is Core Database  

                if (!(ClassCoreUtility.checkIfCoreDatabase(cmbDatabases.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString())))
                {
                    MessageBox.Show("Selected Database not a Core Database!!");
                    return;
                }


                //Check whether account present in CORE

                if (!(ClassCoreUtility.checkIfAccountPresentInCore(txtCoreAccountss.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString(), cmbDatabases.Text.ToString())))
                {
                    MessageBox.Show("Account does not exist in Core! Please give correct Account");
                    return;
                }


                //Check whether database has full access security employee

                if (!(ClassCoreUtility.checkIfCoreDBHasFullAccessUser(cmbDatabases.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString())))
                {
                    MessageBox.Show("No Employee with full access security/Set the security manually");
                    return;
                }


                // Set connection string with selected server and integrated security      
                connectString = "Data Source=" + this.server + ";Integrated Security=false;Initial Catalog=master;User ID=" +
                    txtUserID.Text + ";Password=" + txtPasswordsss.Text + ";";

                using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(connectString))
                {
                    // Open connection
                    con.Open();

                    //Get list of full access users
                    SQL = "SELECT EmployeeID FROM [" + cmbDatabases.Text + "].dbo.Employee WHERE Employee_ID IN (SELECT Employee_ID FROM [" + cmbDatabases.Text + "].dbo.Security WHERE SecurityTemplate_ID IN(SELECT SecurityTemplate_ID FROM [" + cmbDatabases.Text + "]. dbo.SecurityTemplate WHERE SecurityTemplateName = 'Full Access'));";

                    SqlCommand com =
                                 new SqlCommand(SQL, con);
                    SqlDataReader dr = com.ExecuteReader();

                    while (dr.Read())
                    {
                        lstFullAccessUsers.Add(dr[0].ToString());
                    }

                    // Set databases list as combobox’s datasource
                    this.cmbFullAccessUser.DataSource = lstFullAccessUsers;


                    //Get current login Core User
                    string st = ClassCoreUtility.getCurrentCoreLoginUser(cmbDatabases.Text, txtCoreAccountss.Text, this.server, txtUserID.Text, txtPasswordsss.Text);
                    txtCurrentCoreLoginUsersss.Text = st;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnClearDiscrepency_Click(object sender, EventArgs e)
        {
            if (richTxtDiscrepency.Text == "")
            {
                MessageBox.Show(" Discrepencies Already cleared");
            }
            else
            {
                richTxtDiscrepency.Text = "";
            }
        }

        private void btnMakeCoreLoginUsers_Click(object sender, EventArgs e)
        {
            try
            {


                if (String.IsNullOrEmpty(txtCoreAccountss.Text.ToString()) || string.IsNullOrEmpty(cmbDatabases.Text.ToString()))
                {
                    MessageBox.Show(" Please select Account and Coredatabase");
                    return;
                }

                string SQL;



                //Check whether Database exists on the SQL Server                

                if (!(ClassCoreUtility.checkIfDatabaseExistsOnSQL(cmbDatabases.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString())))
                {
                    MessageBox.Show("Database does not exist on SQL Server");
                    return;
                }


                //Check whether database is Core Database  

                if (!(ClassCoreUtility.checkIfCoreDatabase(cmbDatabases.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString())))
                {
                    MessageBox.Show("Selected Database not a Core Database!!");
                    return;
                }


                //Check whether account present in CORE

                if (!(ClassCoreUtility.checkIfAccountPresentInCore(txtCoreAccountss.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString(), cmbDatabases.Text.ToString())))
                {
                    MessageBox.Show("Account does not exist in Core! Please give correct Account");
                    return;
                }


                //Check whether database has full access security employee

                if (!(ClassCoreUtility.checkIfCoreDBHasFullAccessUser(cmbDatabases.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString())))
                {
                    MessageBox.Show("No Employee with full access security/Set the security manually");
                    return;
                }


                //Make selected user as Core login User
                SQL = "UPDATE [" + cmbDatabases.Text + "]. dbo.Employee SET HostAccount_ID=NULL WHERE HostAccount_ID IS NOT NULL;                          UPDATE [" + cmbDatabases.Text + "]. dbo.Employee SET HostAccount_ID = (SELECT ID FROM BQECoreHost.dbo.Account WHERE Email= '" + txtCoreAccountss.Text + "'), Password = (SELECT  Password FROM BQECoreHost.dbo.Account WHERE Email= '" + txtCoreAccountss.Text + "') WHERE EmployeeID = '" + cmbFullAccessUser.Text + "'; ";

                int count = ClassCoreUtility.executeSQLInsertUpdate(SQL, this.server, txtUserID.Text, txtPasswordsss.Text);
                if (count > 0)
                    MessageBox.Show("Selected Employee made as Core Login User sucessfully!!");
                else
                    MessageBox.Show("Opeartion not sucessful!!");



                //Get current login Core User
                string st = ClassCoreUtility.getCurrentCoreLoginUser(cmbDatabases.Text, txtCoreAccountss.Text, this.server, txtUserID.Text, txtPasswordsss.Text);
                txtCurrentCoreLoginUsersss.Text = st;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void metrobtnDeleteDatabaseSQLServer_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show("Do you really want to delete the database from SQL server?", "Delete database", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    txtCurrentCoreLoginUsersss.Text = "";
                    cmbFullAccessUser.Text = string.Empty;

                    if (string.IsNullOrEmpty(cmbDatabases.Text.ToString()) || string.IsNullOrEmpty(txtCoreAccountss.Text.ToString()))
                    {
                        MessageBox.Show("Please select  Coredatabase and Account");
                        return;
                    }

                    string SQL;

                    int count = 0;

                    //Check whether Database exists on the SQL Server                

                    if (!(ClassCoreUtility.checkIfDatabaseExistsOnSQL(cmbDatabases.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString())))
                    {
                        MessageBox.Show("Database does not exist on SQL Server");
                        return;
                    }


                    //Check whether account present in CORE

                    if (!(ClassCoreUtility.checkIfAccountPresentInCore(txtCoreAccountss.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString(), cmbDatabases.Text.ToString())))
                    {
                        MessageBox.Show("Account does not exist in Core! Please give correct Account");
                        return;
                    }


                    //Delete database from SQL Server
                    SQL = "SELECT * FROM  sys.databases WHERE name='" + cmbDatabases.Text + "';";
                    count = ClassCoreUtility.executeSQLSelect(SQL, this.server, txtUserID.Text, txtPasswordsss.Text, cmbDatabases.Text);
                    if (count == 0)
                    {
                        MessageBox.Show("Database does not exist on SQL Server");
                    }
                    SQL = "";
                    count = 0;


                    SQL = " DROP DATABASE [" + cmbDatabases.Text + "];";

                    count = ClassCoreUtility.executeSQLInsertUpdate(SQL, this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString());

                    if (count != 0)

                        MessageBox.Show("Database deleted sucessully from SQL Server");
                    else
                        MessageBox.Show("Database not deleted");

                    //Call connect databases button event explicitly               
                    btnConnectDB.PerformClick();
                }

            }
            catch (Exception ex)
            { MessageBox.Show("Cannot delete database as it is in use. Kindly close all open connections on SQL Server!!"); }
        }

        private void btnExitApp_Click(object sender, EventArgs e)
        {
            try
            {
                
                DialogResult dialogResult = MessageBox.Show("Do you really want to close the Application?","Exit Application", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    Application.Exit();
                }
                else if (dialogResult == DialogResult.No)
                {
                    
                }

                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void adminPortalToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            
            AdminPortal frmAdminPortal = new AdminPortal();
            frmAdminPortal.Show();
        }

        
    }
    }

