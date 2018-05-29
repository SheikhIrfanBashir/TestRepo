using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Forms;
using MetroFramework.Controls;
using System.Data.Sql;
using System.Collections;
using System.Collections.Generic;


namespace COREUtlity
{
    public partial class frmDiscrepencies : MetroFramework.Forms.MetroForm
    {
        SqlDataSourceEnumerator servers;
        DataTable tableServers;
        String server;
        string databaseType;
        private string SQL;

        public frmDiscrepencies()
        {
            InitializeComponent();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            Form frmHome = new frmHome();
            frmHome.Show();
            btnEnableTrigger.BackColor = System.Drawing.Color.AliceBlue;
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {


        }

        private void frmDiscrepencies_Load(object sender, EventArgs e)
        {
            btnEnableTrigger.BackColor = Color.AliceBlue;
        }

        private void btnLoadServers_Click(object sender, EventArgs e)
        {
            try
            {
               
                // Create a instance of the SqlDataSourceEnumerator class
                servers = SqlDataSourceEnumerator.Instance;

                // Fetch all visible SQL server 2000 or SQL Server 2005 instances
                tableServers = new DataTable();

                ClassCoreUtility.CaluculateAll(PBarSQLServer);

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

        private void btnRemovePassword_Click(object sender, EventArgs e)
        {
            try
            {
                int count = 0;
                DialogResult dialogResult = MessageBox.Show("Do you really want to remove  the password?", "remove Password", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                   

                    richTxtDiscrepency.Text = "";
                    if (string.IsNullOrEmpty(cmbDatabases.Text.ToString()))
                    {
                        MessageBox.Show("Please select  Databases");
                        return;
                    }

                    string SQL;


                   

                    //Check whether Database exists on the SQL Server                

                    if (!(ClassCoreUtility.checkIfDatabaseExistsOnSQL(cmbDatabases.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString())))
                    {
                        MessageBox.Show("Database does not exist on SQL Server");
                        return;
                    }


                    //Check whether database is BQ/AO Database  

                    databaseType = ClassCoreUtility.CheckDatabaseType(cmbDatabases.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString());

                    if (databaseType == "CORE" || databaseType == "NONE")
                    {
                        MessageBox.Show("Selected database not a BQ/WS/AO database!!Please select a different database");
                        return;
                    }
                  
                }

                //Remove password from  database in BQ/AO

                if (databaseType == "BQWS")
                {
                    SQL = "";
                    SQL = "USE [" + cmbDatabases.Text.ToString() + "];";
                    SQL += " update SecurityTable SET Settings=REPLACE(Settings,(SUBSTRING(Settings,1,( CHARINDEX('|',Settings)-1))),'')  where EmployeeID='supervisor' and ModuleID=0;";

                }

                else if (databaseType == "AOEO")
                {
                    SQL = "";
                    SQL = "USE [" + cmbDatabases.Text.ToString() + "];";
                    SQL += " UPDATE user_table set user_password='',is_licensed=1  where security_group_id='00000001-0000-0000-0000-000000000000';";

                }
                else
                {
                    MessageBox.Show("Selected database not a BQ/WS/AO database!!Please select a different database");
                    return;
                }


                count = ClassCoreUtility.executeSQLInsertUpdate(SQL, this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString());

                if (count > 0)

                    MessageBox.Show("Password rempved sucessully ");
                else
                    MessageBox.Show("Error!! Please try again!!");

                //btnConnectDB.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }



        private void btnBackToCore_Click(object sender, EventArgs e)
        {
            this.Visible = false;

            frmHome form2 = new frmHome();
            form2.Show();
        }

        private void btnDeleteDatabase_Click_1(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show("Do you really want to delete the database from SQL server?", "Delete database", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {


                    if (string.IsNullOrEmpty(cmbDatabases.Text.ToString()))
                    {
                        MessageBox.Show("Please select  Database ");
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
                    //btnConnectDB.PerformClick();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot delete database,It is currently in use!!");
            }


        }

        private void btnExitApp_Click(object sender, EventArgs e)
        {
            try
            {

                DialogResult dialogResult = MessageBox.Show("Do you really want to close the Application?", "Exit Application", MessageBoxButtons.YesNo);
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

        private void btnGenerateDiscrepency_Click(object sender, EventArgs e)
        {
            try
            {

                richTxtDiscrepency.Text = "";
                
                //ClassCoreUtility.CaluculateAll(pgrDiscrepency);
                if (string.IsNullOrEmpty(cmbDatabases.Text.ToString()) )
                {
                    MessageBox.Show("Please select  Database!!");
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


                //Check whether database is BQ/AO Database  

                databaseType = ClassCoreUtility.CheckDatabaseType(cmbDatabases.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString());

                if(databaseType=="CORE" || databaseType=="NONE")
                {
                    MessageBox.Show("Selected database not a BQ/WS/AO/EO database");
                    return;
                }


                //Get discrepency in BQ/AO database
                Dictionary<string, string> dctDiscrepency;

                if (databaseType == "BQWS")
                {
                     dctDiscrepency = ClassCoreUtility.GetDiscrepencyBQ(cmbDatabases.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString());
                }

                else
                {
                     dctDiscrepency = ClassCoreUtility.GetDiscrepencyAO(cmbDatabases.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString());
                }

                ClassCoreUtility.CaluculateAll(PBarDiscrepency);

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

        private void btnINActiveCustomTriggers_Click(object sender, EventArgs e)
        {
            // Set connection string with selected server and integrated security      
           string connectString = "Data Source=" + this.server + ";Integrated Security=false;Initial Catalog=master;User ID=" +
                txtUserID.Text + ";Password=" + txtPasswordsss.Text + ";";


            try {
                if(cmbDatabases.Text=="" || cmbSQLServer.Text=="")
                {
                    MessageBox.Show("Please enter SQL Server and Database");
                }

                //Check whether Database exists on the SQL Server                

                if (!(ClassCoreUtility.checkIfDatabaseExistsOnSQL(cmbDatabases.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString())))
                {
                    MessageBox.Show("Database does not exist on SQL Server");
                    return;
                }


                //Check whether database is BQ/AO Database  

                databaseType = ClassCoreUtility.CheckDatabaseType(cmbDatabases.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString());

                if (databaseType == "CORE" || databaseType == "NONE")
                {
                    MessageBox.Show("Selected database not a BQ/WS/AO database!!Please select a different database");
                    return;
                }

                   List<string> lstInActiveTriggers = new List<string>();


                using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(connectString))
                {
                    // Open connection
                    con.Open();

                    //Get inactive triggers in database in a datareader 
                    SQL = " use[" + cmbDatabases.Text + "];"+
                        "SELECT sysobjects.name AS trigger_name FROM sysobjects  INNER JOIN sysusers ON sysobjects.uid = sysusers.uid INNER JOIN sys.tables t ON sysobjects.parent_obj = t.object_id  INNER JOIN sys.schemas s  ON t.schema_id = s.schema_id WHERE sysobjects.type = '" + "TR" + "' AND OBJECTPROPERTY(id, '" + "ExecIsTriggerDisabled" + "') <> 0      AND sysobjects.name NOT IN('" + "ProjectGroupDetail_UTrig" + "','" + "ProjectGroupDetail_ITrig" + "','" + "ProjectGroup_UTrig" + "','" + "ProjectGroup_DTrig" + "','" + "Project_UTrig" + "','" + "Project_DTrig" + "','" + "FeeScheduleExp_UTrig" + "','" + "FeeScheduleExp_DTrig" +
                    "','" + "FeeScheduleDetailExp_UTrig" + "','" + "FeeScheduleDetailExp_ITrig" + "','" + "FeeScheduleDetail_UTrig" + "','" + "FeeScheduleDetail_ITrig" + "','" + "FeeSchedule_UTrig" + "','" + "FeeSchedule_DTrig" + "','" + "ExpenseGroupDetail_UTrig" + "','" + "ExpenseGroupDetail_ITrig" + "','" + "ExpenseGroup_UTrig" + "','" + "ExpenseGroup_DTrig" + "','" + "Expense_UTrig" + "','" + "Expense_DTrig" + "','" + "EmployeeGroupDetail_UTrig" + "','" + "EmployeeGroupDetail_ITrig" + "','" + "EmployeeGroup_UTrig" + "','" + "EmployeeGroup_DTrig" + "','" + "Employee_UTrig" + "','" + "Employee_DTrig" + "','" + "ClientGroupDetail_UTrig" + "','" + "ClientGroupDetail_ITrig" +
                        "','" + "ClientGroup_UTrig" + "','" + "ClientGroup_DTrig" + "','" + "ClientContact_UTrig" + "','" + "ClientContact_ITrig" + "','" + "Client_UTrig" +
                        "','" + "Client_DTrig" + "','" + "BudgetExpense_UTrig" + "','" + "BudgetExpense_ITrig" + "','" + "BudgetActivity_UTrig" + "','" + "BudgetActivity_ITrig" +
                        "','" + "Budget_UTrig" + "','" + "Budget_DTrig" + "','" + "BillingScheduleDetail_UTrig" + "','" + "BillingScheduleDetail_ITrig" + "','" + "ActivityGroupDetail_UTrig" +
                        "','" + "ActivityGroupDetail_ITrig" + "','" + "ActivityGroup_UTrig" + "','" + "ActivityGroup_DTrig" + "','" + "Activity_UTrig" + "','" + "Activity_DTrig" + "');";
                        

                    System.Data.SqlClient.SqlCommand com =
                                 new System.Data.SqlClient.SqlCommand(SQL, con);
                    System.Data.SqlClient.SqlDataReader dr = com.ExecuteReader();

                    while (dr.Read())
                    {
                        lstInActiveTriggers.Add(dr[0].ToString());                        
                        
                    }

                    // Set databases list as combobox’s datasource
                    this.cmbINActiveCustomTriggers.DataSource = lstInActiveTriggers;
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnActiveCustomTriggers_Click(object sender, EventArgs e)
        {
            // Set connection string with selected server and integrated security      
            string connectString = "Data Source=" + this.server + ";Integrated Security=false;Initial Catalog=master;User ID=" +
                 txtUserID.Text + ";Password=" + txtPasswordsss.Text + ";";


            try
            {
                if (cmbDatabases.Text == "" || cmbSQLServer.Text == "")
                {
                    MessageBox.Show("Please enter SQL Server and Database");
                }

                //Check whether Database exists on the SQL Server                

                if (!(ClassCoreUtility.checkIfDatabaseExistsOnSQL(cmbDatabases.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString())))
                {
                    MessageBox.Show("Database does not exist on SQL Server");
                    return;
                }


                //Check whether database is BQ/AO Database  

                databaseType = ClassCoreUtility.CheckDatabaseType(cmbDatabases.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString());

                if (databaseType == "CORE" || databaseType == "NONE")
                {
                    MessageBox.Show("Selected database not a BQ/WS/AO database!!Please select a different database");
                    return;
                }

                List<string> lstActiveTriggers = new List<string>();


                using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(connectString))
                {
                    // Open connection
                    con.Open();

                    //Get inactive triggers in database in a datareader 
                    SQL = " use[" + cmbDatabases.Text + "];" +
                        "SELECT sysobjects.name AS trigger_name FROM sysobjects  INNER JOIN sysusers ON sysobjects.uid = sysusers.uid INNER JOIN sys.tables t ON sysobjects.parent_obj = t.object_id  INNER JOIN sys.schemas s  ON t.schema_id = s.schema_id WHERE sysobjects.type = '" + "TR" + "' AND OBJECTPROPERTY(id, '" + "ExecIsTriggerDisabled" + "') = 0      AND sysobjects.name NOT IN('" + "ProjectGroupDetail_UTrig" + "','" + "ProjectGroupDetail_ITrig" + "','" + "ProjectGroup_UTrig" + "','" + "ProjectGroup_DTrig" + "','" + "Project_UTrig" + "','" + "Project_DTrig" + "','" + "FeeScheduleExp_UTrig" + "','" + "FeeScheduleExp_DTrig" +
                    "','" + "FeeScheduleDetailExp_UTrig" + "','" + "FeeScheduleDetailExp_ITrig" + "','" + "FeeScheduleDetail_UTrig" + "','" + "FeeScheduleDetail_ITrig" + "','" + "FeeSchedule_UTrig" + "','" + "FeeSchedule_DTrig" + "','" + "ExpenseGroupDetail_UTrig" + "','" + "ExpenseGroupDetail_ITrig" + "','" + "ExpenseGroup_UTrig" + "','" + "ExpenseGroup_DTrig" + "','" + "Expense_UTrig" + "','" + "Expense_DTrig" + "','" + "EmployeeGroupDetail_UTrig" + "','" + "EmployeeGroupDetail_ITrig" + "','" + "EmployeeGroup_UTrig" + "','" + "EmployeeGroup_DTrig" + "','" + "Employee_UTrig" + "','" + "Employee_DTrig" + "','" + "ClientGroupDetail_UTrig" + "','" + "ClientGroupDetail_ITrig" +
                        "','" + "ClientGroup_UTrig" + "','" + "ClientGroup_DTrig" + "','" + "ClientContact_UTrig" + "','" + "ClientContact_ITrig" + "','" + "Client_UTrig" +
                        "','" + "Client_DTrig" + "','" + "BudgetExpense_UTrig" + "','" + "BudgetExpense_ITrig" + "','" + "BudgetActivity_UTrig" + "','" + "BudgetActivity_ITrig" +
                        "','" + "Budget_UTrig" + "','" + "Budget_DTrig" + "','" + "BillingScheduleDetail_UTrig" + "','" + "BillingScheduleDetail_ITrig" + "','" + "ActivityGroupDetail_UTrig" +
                        "','" + "ActivityGroupDetail_ITrig" + "','" + "ActivityGroup_UTrig" + "','" + "ActivityGroup_DTrig" + "','" + "Activity_UTrig" + "','" + "Activity_DTrig" + "');";


                    System.Data.SqlClient.SqlCommand com =
                                 new System.Data.SqlClient.SqlCommand(SQL, con);
                    System.Data.SqlClient.SqlDataReader dr = com.ExecuteReader();

                    while (dr.Read())
                    {
                        lstActiveTriggers.Add(dr[0].ToString());

                    }

                    // Set databases list as combobox’s datasource
                    this.cmbActiveCustomTriggers.DataSource = lstActiveTriggers;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnEnableTrigger_Click(object sender, EventArgs e)
        {
            // Set connection string with selected server and integrated security      
            string connectString = "Data Source=" + this.server + ";Integrated Security=false;Initial Catalog=master;User ID=" +
                 txtUserID.Text + ";Password=" + txtPasswordsss.Text + ";";


            try
            {
                if (cmbDatabases.Text == "" || cmbSQLServer.Text == "")
                {
                    MessageBox.Show("Please enter SQL Server and Database");
                }

                //Check whether Database exists on the SQL Server                

                if (!(ClassCoreUtility.checkIfDatabaseExistsOnSQL(cmbDatabases.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString())))
                {
                    MessageBox.Show("Database does not exist on SQL Server");
                    return;
                }


                //Check whether database is BQ/AO Database  

                databaseType = ClassCoreUtility.CheckDatabaseType(cmbDatabases.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString());

                if (databaseType == "CORE" || databaseType == "NONE")
                {
                    MessageBox.Show("Selected database not a BQ/WS/AO database!!Please select a different database");
                    return;
                }

                string tableName="";


                using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(connectString))
                {
                    // Open connection
                    con.Open();

                    //Get table name of the Inactive trigger to be activated
                    SQL = " use[" + cmbDatabases.Text + "];" +
                        "SELECT OBJECT_NAME(parent_obj) AS table_name FROM sysobjects  INNER JOIN sysusers ON sysobjects.uid = sysusers.uid INNER JOIN sys.tables t  ON sysobjects.parent_obj = t.object_id  INNER JOIN sys.schemas s ON t.schema_id = s.schema_id WHERE sysobjects.type = 'TR' AND OBJECTPROPERTY(id, 'ExecIsTriggerDisabled')<> 0 AND sysobjects.name IN('" + cmbINActiveCustomTriggers.Text +   "'); ";


                    System.Data.SqlClient.SqlCommand com =
                                 new System.Data.SqlClient.SqlCommand(SQL, con);
                    System.Data.SqlClient.SqlDataReader dr = com.ExecuteReader();

                    while (dr.Read())
                    {
                        tableName = dr[0].ToString();

                    }

                    // Set Query to activate the Inactive trigger
                    String SQLEnableTGR;
                    SQLEnableTGR = "Use [" + cmbDatabases.Text +  " ] ; ALTER TABLE " + tableName + " ENABLE TRIGGER [" + cmbINActiveCustomTriggers.Text +"];";

                   int count = ClassCoreUtility.executeSQLInsertUpdate(SQLEnableTGR, this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString());

                    if (count != 0)

                        MessageBox.Show("Trigger enabled sucessully ");
                    else
                        MessageBox.Show("Error!! Please try again!!");

                    btnINActiveCustomTriggers.PerformClick();
                    btnActiveCustomTriggers.PerformClick();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnDisableTrigger_Click(object sender, EventArgs e)
        {
            // Set connection string with selected server and integrated security      
            string connectString = "Data Source=" + this.server + ";Integrated Security=false;Initial Catalog=master;User ID=" +
                 txtUserID.Text + ";Password=" + txtPasswordsss.Text + ";";


            try
            {
                if (cmbDatabases.Text == "" || cmbSQLServer.Text == "")
                {
                    MessageBox.Show("Please enter SQL Server and Database");
                }

                //Check whether Database exists on the SQL Server                

                if (!(ClassCoreUtility.checkIfDatabaseExistsOnSQL(cmbDatabases.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString())))
                {
                    MessageBox.Show("Database does not exist on SQL Server");
                    return;
                }


                //Check whether database is BQ/AO Database  

                databaseType = ClassCoreUtility.CheckDatabaseType(cmbDatabases.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString());

                if (databaseType == "CORE" || databaseType == "NONE")
                {
                    MessageBox.Show("Selected database not a BQ/WS/AO database!!Please select a different database");
                    return;
                }

                string tableName = "";


                using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(connectString))
                {
                    // Open connection
                    con.Open();

                    //Get table name of the Active trigger to be disabled
                    SQL = " use[" + cmbDatabases.Text + "];" +
                        "SELECT OBJECT_NAME(parent_obj) AS table_name FROM sysobjects  INNER JOIN sysusers ON sysobjects.uid = sysusers.uid INNER JOIN sys.tables t  ON sysobjects.parent_obj = t.object_id  INNER JOIN sys.schemas s ON t.schema_id = s.schema_id WHERE sysobjects.type = 'TR' AND OBJECTPROPERTY(id, 'ExecIsTriggerDisabled')= 0 AND sysobjects.name IN('" + cmbActiveCustomTriggers.Text + "'); ";


                    System.Data.SqlClient.SqlCommand com =
                                 new System.Data.SqlClient.SqlCommand(SQL, con);
                    System.Data.SqlClient.SqlDataReader dr = com.ExecuteReader();

                    while (dr.Read())
                    {
                        tableName = dr[0].ToString();

                    }

                    // Set Query to disable the Active trigger
                    String SQLEnableTGR;
                    SQLEnableTGR = "Use [" + cmbDatabases.Text + "] ; ALTER TABLE " + tableName + " DISABLE TRIGGER [" + cmbActiveCustomTriggers.Text + "];";

                    int count = ClassCoreUtility.executeSQLInsertUpdate(SQLEnableTGR, this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString());

                    if (count != 0)

                        MessageBox.Show("Trigger disabled sucessully ");
                    else
                        MessageBox.Show("Error!! Please try again!!");

                    btnActiveCustomTriggers.PerformClick();
                    btnINActiveCustomTriggers.PerformClick();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnApplyLicense_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbDatabases.Text == "" || cmbSQLServer.Text == "")
                {
                    MessageBox.Show("Please enter SQL Server and Database");
                }

                //Check whether Database exists on the SQL Server                

                if (!(ClassCoreUtility.checkIfDatabaseExistsOnSQL(cmbDatabases.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString())))
                {
                    MessageBox.Show("Database does not exist on SQL Server");
                    return;
                }


                //Check whether database is BQ/AO Database  

                databaseType = ClassCoreUtility.CheckDatabaseType(cmbDatabases.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString());

                if (databaseType == "CORE" || databaseType == "NONE")
                {
                    MessageBox.Show("Selected database not a BQ/WS/AO database!!Please select a different database");
                    return;
                }

               


                //BQ 2018 Enterprise

                if(cmbLicense.Text== "BQ 2018 Enterprise")
                {              

                          //check whether BQ database

                       string databaseType = ClassCoreUtility.CheckDatabaseType(cmbDatabases.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString());

                        if (databaseType == "CORE" || databaseType=="AOEO" || databaseType == "NONE")
                        {
                            MessageBox.Show("Selected database not a BQ database!!Please select a different database");
                            return;
                        }


                    // check whether BQ database if of 2018 version

                    string databaseVersion = ClassCoreUtility.CheckBQDatabaseVersionNumber(cmbDatabases.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString());

                    if(databaseVersion != "19.00")
                    {
                        MessageBox.Show("Selected BQ database is not of Version 2018!!");
                        return;
                    }


                    string SQL = "use [" + cmbDatabases.Text + "];"; 
                        SQL+="UPDATE Company SET CoName ='" + "BQ - TEST - 18" + "', LastUpdated=GETDATE();";
                    SQL += "DELETE FROM BQLicense WHERE ProductCode ='" + "BQ2018E" + "';";
                     SQL+= "INSERT INTO BQLicense (ProductCode, LicKey, NumofUsers,LastUpdated) VALUES('BQ2018E','" + "988C - GQR7 - N6Q0 - 0AX4" + "','" + 50 + "',GETDATE());";
                    SQL += "UPDATE WebUsers SET Misc1='" + "BQ2018E" +"', Misc2= '', LastUpdated = GETDATE() , ProductID='BQ2018E'  WHERE ProductID LIKE 'BQ2018E%';DELETE FROM BQSTable WHERE ParamName = 'BQ2017_REGKEY';";

                    SQL += "INSERT INTO BQSTable VALUES ('BQ2017_REGKEY','988C-GQR7-N6Q0-0AX4',GETDATE());";

                   int count = ClassCoreUtility.executeSQLInsertUpdate(SQL, this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString());

                    if (count > 0)

                        MessageBox.Show("License applied sucessully ");
                    else
                        MessageBox.Show("Error!! Please try again!!");

                }




                //BQ 2018 BASIC

                if (cmbLicense.Text == "BQ 2018 Basic")
                {

                    //check whether BQ database

                    string databaseType = ClassCoreUtility.CheckDatabaseType(cmbDatabases.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString());

                    if (databaseType == "CORE" || databaseType == "AOEO" || databaseType == "NONE")
                    {
                        MessageBox.Show("Selected database not a BQ database!!Please select a different database");
                        return;
                    }


                    // check whether BQ database if of 2018 version

                    string databaseVersion = ClassCoreUtility.CheckBQDatabaseVersionNumber(cmbDatabases.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString());

                    if (databaseVersion != "19.00")
                    {
                        MessageBox.Show("Selected BQ database is not of Version 2018!!");
                        return;
                    }


                    string SQL = "use [" + cmbDatabases.Text + "];";
                    SQL += "UPDATE Company SET CoName = 'BQ-TESTBASIC-18 ', LastUpdated=GETDATE();DELETE FROM BQLicense WHERE ProductCode = 'BQ2018B';INSERT INTO BQLicense (ProductCode, LicKey, NumofUsers,LastUpdated) VALUES('BQ2018B','7Q7K-YYRR-LYJW-EN4E','50',GETDATE());UPDATE WebUsers SET Misc1='BQ2018B', Misc2= '', LastUpdated = GETDATE() , ProductID='BQ2018B'  WHERE ProductID LIKE 'BQ2018B%';DELETE FROM BQSTable WHERE ParamName = 'BQ2018_REGKEY';INSERT INTO BQSTable VALUES ('BQ2018_REGKEY','7Q7K-YYRR-LYJW-EN4E',GETDATE());";

                    int count = ClassCoreUtility.executeSQLInsertUpdate(SQL, this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString());

                    if (count > 0)

                        MessageBox.Show("License applied sucessully ");
                    else
                        MessageBox.Show("Error!! Please try again!!");

                }


                //BQ 2018 Professional

                if (cmbLicense.Text == "BQ 2018 Professional")
                {

                    //check whether BQ database

                    string databaseType = ClassCoreUtility.CheckDatabaseType(cmbDatabases.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString());

                    if (databaseType == "CORE" || databaseType == "AOEO" || databaseType == "NONE")
                    {
                        MessageBox.Show("Selected database not a BQ database!!Please select a different database");
                        return;
                    }


                    // check whether BQ database if of 2018 version

                    string databaseVersion = ClassCoreUtility.CheckBQDatabaseVersionNumber(cmbDatabases.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString());

                    if (databaseVersion != "19.00")
                    {
                        MessageBox.Show("Selected BQ database is not of Version 2018!!");
                        return;
                    }


                    string SQL = "use [" + cmbDatabases.Text + "];";
                    SQL += "UPDATE Company SET CoName = 'BQ-TESTPRO-18 ', LastUpdated=GETDATE();DELETE FROM BQLicense WHERE ProductCode = 'BQ2018P';INSERT INTO BQLicense (ProductCode, LicKey, NumofUsers,LastUpdated) VALUES('BQ2018P','TGUU-MD4K-7BJY-82RJ','50',GETDATE());UPDATE WebUsers SET Misc1='BQ2018P', Misc2= '', LastUpdated = GETDATE() , ProductID='BQ2018P'  WHERE ProductID LIKE 'BQ2018P%';DELETE FROM BQSTable WHERE ParamName = 'BQ2018_REGKEY';INSERT INTO BQSTable VALUES ('BQ2018_REGKEY','TGUU-MD4K-7BJY-82RJ',GETDATE());";

                    int count = ClassCoreUtility.executeSQLInsertUpdate(SQL, this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString());

                    if (count > 0)

                        MessageBox.Show("License applied sucessully ");
                    else
                        MessageBox.Show("Error!! Please try again!!");

                }



                //BQ 2017 Enterprise

                if (cmbLicense.Text == "BQ 2017 Enterprise")
                {

                    //check whether BQ database

                    string databaseType = ClassCoreUtility.CheckDatabaseType(cmbDatabases.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString());

                    if (databaseType == "CORE" || databaseType == "AOEO" || databaseType == "NONE")
                    {
                        MessageBox.Show("Selected database not a BQ database!!Please select a different database");
                        return;
                    }


                    // check whether BQ database if of 2017 version

                    string databaseVersion = ClassCoreUtility.CheckBQDatabaseVersionNumber(cmbDatabases.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString());

                    if (databaseVersion != "18.00")
                    {
                        MessageBox.Show("Selected BQ database is not of Version 2017!!");
                        return;
                    }


                    string SQL = "use [" + cmbDatabases.Text + "];";
                    SQL += "UPDATE Company SET CoName = 'WS2017ENT', LastUpdated=GETDATE();DELETE FROM BQLicense WHERE ProductCode = 'WS2017E';INSERT INTO BQLicense (ProductCode, LicKey, NumofUsers,LastUpdated) VALUES('WS2017E','D3GK-7YA8-GWT3-XDEX','50',GETDATE());UPDATE WebUsers SET Misc1='WS2017E', Misc2= '', LastUpdated = GETDATE() , ProductID='WS2017E'  WHERE ProductID LIKE 'WS2017%';DELETE FROM BQSTable WHERE ParamName = 'WS2017_REGKEY';INSERT INTO BQSTable VALUES ('WS2017_REGKEY','CVU4-TNX6-9UJN-TY5L',GETDATE());";

                    int count = ClassCoreUtility.executeSQLInsertUpdate(SQL, this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString());

                    if (count > 0)

                        MessageBox.Show("License applied sucessully ");
                    else
                        MessageBox.Show("Error!! Please try again!!");

                }





                //BQ 2017 Basic

                if (cmbLicense.Text == "BQ 2017 Basic")
                {

                    //check whether BQ database

                    string databaseType = ClassCoreUtility.CheckDatabaseType(cmbDatabases.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString());

                    if (databaseType == "CORE" || databaseType == "AOEO" || databaseType == "NONE")
                    {
                        MessageBox.Show("Selected database not a BQ database!!Please select a different database");
                        return;
                    }


                    // check whether BQ database if of 2017 version

                    string databaseVersion = ClassCoreUtility.CheckBQDatabaseVersionNumber(cmbDatabases.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString());

                    if (databaseVersion != "18.00")
                    {
                        MessageBox.Show("Selected BQ database is not of Version 2017!!");
                        return;
                    }


                    string SQL = "use [" + cmbDatabases.Text + "];";
                    SQL += "UPDATE Company SET CoName = 'WS2017BASIC', LastUpdated=GETDATE();DELETE FROM BQLicense WHERE ProductCode = 'WS2017B';INSERT INTO BQLicense (ProductCode, LicKey, NumofUsers,LastUpdated) VALUES('WS2017B','GH7U-4KQY-3E1F-RHVL','30',GETDATE());UPDATE WebUsers SET Misc1='WS2017B', Misc2= '', LastUpdated = GETDATE() , ProductID='WS2017B'  WHERE ProductID LIKE 'WS2017%';DELETE FROM BQSTable WHERE ParamName = 'WS2017_REGKEY';INSERT INTO BQSTable VALUES ('WS2017_REGKEY','R5AQ-4M9Y-CDUL-KVXH',GETDATE());";

                    int count = ClassCoreUtility.executeSQLInsertUpdate(SQL, this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString());

                    if (count > 0)

                        MessageBox.Show("License applied sucessully ");
                    else
                        MessageBox.Show("Error!! Please try again!!");

                }



                //BQ 2017 Professional

                if (cmbLicense.Text == "BQ 2017 Professional")
                {

                    //check whether BQ database

                    string databaseType = ClassCoreUtility.CheckDatabaseType(cmbDatabases.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString());

                    if (databaseType == "CORE" || databaseType == "AOEO" || databaseType == "NONE")
                    {
                        MessageBox.Show("Selected database not a BQ database!!Please select a different database");
                        return;
                    }


                    // check whether BQ database if of 2017 version

                    string databaseVersion = ClassCoreUtility.CheckBQDatabaseVersionNumber(cmbDatabases.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString());

                    if (databaseVersion != "18.00")
                    {
                        MessageBox.Show("Selected BQ database is not of Version 2017!!");
                        return;
                    }


                    string SQL = "use [" + cmbDatabases.Text + "];";
                    SQL += "UPDATE Company SET CoName = 'WS2017PRO', LastUpdated=GETDATE();DELETE FROM BQLicense WHERE ProductCode = 'WS2017P';INSERT INTO BQLicense (ProductCode, LicKey, NumofUsers,LastUpdated) VALUES('WS2017P','QP6B-VECW-XNJN-CJ9A','30',GETDATE());UPDATE WebUsers SET Misc1='WS2017P', Misc2= '', LastUpdated = GETDATE() , ProductID='WS2017P'  WHERE ProductID LIKE 'WS2017%';DELETE FROM BQSTable WHERE ParamName = 'WS2017_REGKEY';INSERT INTO BQSTable VALUES ('WS2017_REGKEY','CGEF-4QMJ-DGAF-RCY9',GETDATE());";

                    int count = ClassCoreUtility.executeSQLInsertUpdate(SQL, this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString());

                    if (count > 0)

                        MessageBox.Show("License applied sucessully ");
                    else
                        MessageBox.Show("Error!! Please try again!!");

                }




                //AO 2018 Enterprise

                if (cmbLicense.Text == "AO 2018 Enterprise")
                {

                    //check whether AO database

                    string databaseType = ClassCoreUtility.CheckDatabaseType(cmbDatabases.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString());

                    if (databaseType == "CORE" || databaseType == "BQWS" || databaseType == "NONE")
                    {
                        MessageBox.Show("Selected database not a AO database!!Please select a different database");
                        return;
                    }


                    // check whether AO database if of 2018 version

                    string databaseVersion = ClassCoreUtility.CheckAODatabaseVersionNumber(cmbDatabases.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString());

                    if (databaseVersion != "2018")
                    {
                        MessageBox.Show("Selected AO database is not of Version 2018!!");
                        return;
                    }


                    string SQL = "use [" + cmbDatabases.Text + "];";
                    SQL += "UPDATE ol_client SET company_name = 'BQ-TESTAO-18', license = 'W7PW-MYTH-7W37-M8LK', ProductCode = 'AO2018E', license_count = 50;";

                    int count = ClassCoreUtility.executeSQLInsertUpdate(SQL, this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString());

                    if (count > 0)

                        MessageBox.Show("License applied sucessully ");
                    else
                        MessageBox.Show("Error!! Please try again!!");

                }





                //AO 2018 BASIC

                if (cmbLicense.Text == "AO 2018 Basic")
                {

                    //check whether AO database

                    string databaseType = ClassCoreUtility.CheckDatabaseType(cmbDatabases.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString());

                    if (databaseType == "CORE" || databaseType == "BQWS" || databaseType == "NONE")
                    {
                        MessageBox.Show("Selected database not a AO database!!Please select a different database");
                        return;
                    }


                    // check whether AO database if of 2018 version

                    string databaseVersion = ClassCoreUtility.CheckAODatabaseVersionNumber(cmbDatabases.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString());

                    if (databaseVersion != "2018")
                    {
                        MessageBox.Show("Selected AO database is not of Version 2018!!");
                        return;
                    }


                    string SQL = "use [" + cmbDatabases.Text + "];";
                    SQL += "UPDATE ol_client SET company_name = 'BQ-TESTAO-18', license = '6YH5-FCTV-RMVC-LXMV', ProductCode = 'AO2018B', license_count = 50;";

                    int count = ClassCoreUtility.executeSQLInsertUpdate(SQL, this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString());

                    if (count > 0)

                        MessageBox.Show("License applied sucessully ");
                    else
                        MessageBox.Show("Error!! Please try again!!");

                }







                //AO 2018 Professional

                if (cmbLicense.Text == "AO 2018 Professional")
                {

                    //check whether AO database

                    string databaseType = ClassCoreUtility.CheckDatabaseType(cmbDatabases.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString());

                    if (databaseType == "CORE" || databaseType == "BQWS" || databaseType == "NONE")
                    {
                        MessageBox.Show("Selected database not a AO database!!Please select a different database");
                        return;
                    }


                    // check whether AO database if of 2018 version

                    string databaseVersion = ClassCoreUtility.CheckAODatabaseVersionNumber(cmbDatabases.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString());

                    if (databaseVersion != "2018")
                    {
                        MessageBox.Show("Selected AO database is not of Version 2018!!");
                        return;
                    }


                    string SQL = "use [" + cmbDatabases.Text + "];";
                    SQL += "UPDATE ol_client SET company_name = 'BQ-TESTAO-18', license = '6DRE-NNHV-BLYC-UVE6', ProductCode = 'AO2018P', license_count = 50;";

                    int count = ClassCoreUtility.executeSQLInsertUpdate(SQL, this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString());

                    if (count > 0)

                        MessageBox.Show("License applied sucessully ");
                    else
                        MessageBox.Show("Error!! Please try again!!");

                }





                //AO 2017 Enterprise

                if (cmbLicense.Text == "AO 2017 Enterprise")
                {

                    //check whether AO database

                    string databaseType = ClassCoreUtility.CheckDatabaseType(cmbDatabases.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString());

                    if (databaseType == "CORE" || databaseType == "BQWS" || databaseType == "NONE")
                    {
                        MessageBox.Show("Selected database not a AO database!!Please select a different database");
                        return;
                    }


                    // check whether AO database if of 2018 version

                    string databaseVersion = ClassCoreUtility.CheckAODatabaseVersionNumber(cmbDatabases.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString());

                    if (databaseVersion != "2017")
                    {
                        MessageBox.Show("Selected AO database is not of Version 2017!!");
                        return;
                    }


                    string SQL = "use [" + cmbDatabases.Text + "];";
                    SQL += "UPDATE ol_client SET company_name = 'AO2017ENT', license = '34FW-A803-5W57-DP3U', ProductCode = 'AO2017E', license_count = 50;";

                    int count = ClassCoreUtility.executeSQLInsertUpdate(SQL, this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString());

                    if (count > 0)

                        MessageBox.Show("License applied sucessully ");
                    else
                        MessageBox.Show("Error!! Please try again!!");

                }







                //AO 2017 Basic

                if (cmbLicense.Text == "AO 2017 Enterprise")
                {

                    //check whether AO database

                    string databaseType = ClassCoreUtility.CheckDatabaseType(cmbDatabases.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString());

                    if (databaseType == "CORE" || databaseType == "BQWS" || databaseType == "NONE")
                    {
                        MessageBox.Show("Selected database not a AO database!!Please select a different database");
                        return;
                    }


                    // check whether AO database if of 2018 version

                    string databaseVersion = ClassCoreUtility.CheckAODatabaseVersionNumber(cmbDatabases.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString());

                    if (databaseVersion != "2017")
                    {
                        MessageBox.Show("Selected AO database is not of Version 2017!!");
                        return;
                    }


                    string SQL = "use [" + cmbDatabases.Text + "];";
                    SQL += "UPDATE ol_client SET company_name = 'AO2017BASIC', license = 'KUYL-XKK7-NMYM-8UB7', ProductCode = 'AO2017B', license_count = 50;";

                    int count = ClassCoreUtility.executeSQLInsertUpdate(SQL, this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString());

                    if (count > 0)

                        MessageBox.Show("License applied sucessully ");
                    else
                        MessageBox.Show("Error!! Please try again!!");

                }



                //AO 2017 Professional

                if (cmbLicense.Text == "AO 2017 Professional")
                {

                    //check whether AO database

                    string databaseType = ClassCoreUtility.CheckDatabaseType(cmbDatabases.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString());

                    if (databaseType == "CORE" || databaseType == "BQWS" || databaseType == "NONE")
                    {
                        MessageBox.Show("Selected database not a AO database!!Please select a different database");
                        return;
                    }


                    // check whether AO database if of 2018 version

                    string databaseVersion = ClassCoreUtility.CheckAODatabaseVersionNumber(cmbDatabases.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString());

                    if (databaseVersion != "2017")
                    {
                        MessageBox.Show("Selected AO database is not of Version 2017!!");
                        return;
                    }


                    string SQL = "use [" + cmbDatabases.Text + "];";
                    SQL += "UPDATE ol_client SET company_name = 'AO2017PRO', license = 'CM6H-K6TF-EK27-H3UE', ProductCode = 'AO2017P', license_count = 50;";

                    int count = ClassCoreUtility.executeSQLInsertUpdate(SQL, this.server.ToString(), txtUserID.Text.ToString(), txtPasswordsss.Text.ToString());

                    if (count > 0)

                        MessageBox.Show("License applied sucessully ");
                    else
                        MessageBox.Show("Error!! Please try again!!");

                }






            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnLoadLicence_Click(object sender, EventArgs e)
        {
            List<string> lstlicenceType = new List<string>();
            lstlicenceType.Add("AO 2018 Enterprise");
            lstlicenceType.Add("AO 2018 Basic");
            lstlicenceType.Add("AO 2018 Professional");

            lstlicenceType.Add("AO 2017 Enterprise");
            lstlicenceType.Add("AO 2017 Basic");
            lstlicenceType.Add("AO 2017 Professional");

            lstlicenceType.Add("BQ 2018 Enterprise");
            lstlicenceType.Add("BQ 2018 Basic");
            lstlicenceType.Add("BQ 2018 Professional");


            lstlicenceType.Add("BQ 2017 Enterprise");
            lstlicenceType.Add("BQ 2017 Basic");
            lstlicenceType.Add("BQ 2017 Professional");

            cmbLicense.DataSource = lstlicenceType;
        }
    }

   
   
}