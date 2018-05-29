using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COREUtlity
{
    public partial class AdminPortal : MetroFramework.Forms.MetroForm
    {
        DataTable tableServers;
        SqlDataSourceEnumerator servers;
        String server;        

        public AdminPortal()
        {
            InitializeComponent();
        }

        private void AdminPortal_Load(object sender, EventArgs e)
        {

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

        private void btnBackToCore_Click(object sender, EventArgs e)
        {
            this.Visible = false;

            frmHome form2 = new frmHome();
            form2.Show();
        }

        private void btnCreateQuerylistTable_Click(object sender, EventArgs e)
        {
            try
            {

                string SQL;
                int count;

                if (String.IsNullOrEmpty(cmbSQLServer.Text.ToString()) || string.IsNullOrEmpty(cmbDatabases.Text.ToString()))
                {
                    MessageBox.Show(" Please select UserID, Password, SQL Server or Coredatabase");
                    return;
                }



                //Check whether Database Coreutility exists on the SQL Server                

                if ((ClassCoreUtility.checkIfDatabaseExistsOnSQL("CoreUtility", cmbSQLServer.Text.ToString(), txtUserID.Text.ToString(), txtPassword.Text.ToString())))
                {
                    MessageBox.Show("Database already  exist on SQL Server");
                    return;
                }

                //Create Datbase CoreUtility

                SQL = "";
                SQL += "CREATE DATABASE [CoreUtility];";

                count = ClassCoreUtility.executeSQLInsertUpdate(SQL, cmbSQLServer.Text.ToString(), txtUserID.Text.ToString(), txtPassword.Text.ToString());



                // Create table Query List, Add Default valuse to the QueryList table
                SQL = "";
                SQL += "USE [CoreUtility]; CREATE TABLE[dbo].[QueryList]([ID][numeric](18, 0) IDENTITY(1,1) NOT NULL,[Query] [nvarchar](max) NOT NULL,[Comment] [nvarchar](max) NULL,	[Software] [nchar](10) NULL,[IsActive] [bit]  NULL, CONSTRAINT[PK_QueryList] PRIMARY KEY CLUSTERED(   [ID] ASC )WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY] ) ON[PRIMARY] TEXTIMAGE_ON[PRIMARY]  ;SET IDENTITY_INSERT [dbo].[QueryList] ON ;  ";


                SQL += " INSERT[dbo].[QueryList]([ID], [Query], [Comment], [Software], [IsActive]) VALUES(CAST(1 AS Numeric(18, 0)), N'SELECT inv.InvoiceDetail_ID,inv.amt,pay.Payamt FROM (SELECT InvoiceDetail_ID, SUM(Amount) AS amt FROM  dbo.InvoiceDetail GROUP BY InvoiceDetail_ID) inv JOIN (SELECT InvoiceDetail_ID, SUM(AmountPaid) AS Payamt FROM  dbo.PaymentDetail GROUP BY InvoiceDetail_ID) pay ON pay.InvoiceDetail_ID = inv.InvoiceDetail_ID WHERE inv.amt <> pay.Payamt AND(ABS(inv.amt - pay.Payamt) BETWEEN 0 AND 1);', N'--Penny issue in invoices and Paymnets', N'CORE      ', 1) ;  INSERT[dbo].[QueryList] ([ID], [Query], [Comment], [Software], [IsActive]) VALUES(CAST(2 AS Numeric(18, 0)), N'SELECT inv.InvoiceDetail_ID,inv.amt,pay.Payamt FROM (SELECT InvoiceDetail_ID, SUM(Amount) AS amt FROM  dbo.InvoiceDetail GROUP BY InvoiceDetail_ID) inv JOIN (SELECT InvoiceDetail_ID, SUM(AmountPaid) AS Payamt FROM  dbo.PaymentDetail GROUP BY InvoiceDetail_ID) pay ON pay.InvoiceDetail_ID = inv.InvoiceDetail_ID WHERE inv.amt <> pay.Payamt AND pay.Payamt > inv.amt;', N'--Payment more than invoice', N'CORE      ', 1) ; INSERT[dbo].[QueryList] ([ID], [Query], [Comment], [Software], [IsActive]) VALUES(CAST(3 AS Numeric(18, 0)), N'SELECT i.Invoice_ID,i.InvoiceAmount,inv.amt FROM (SELECT Invoice_ID, SUM(Amount) AS amt FROM dbo.InvoiceDetail GROUP BY Invoice_ID)inv JOIN  dbo.Invoice i ON i.Invoice_ID = inv.Invoice_ID  WHERE i.InvoiceAmount <> inv.amt;', N'--Different amount in Invoice and InvoiceDetail for same invoice', N'CORE      ', 1)  ; INSERT[dbo].[QueryList] ([ID], [Query], [Comment], [Software], [IsActive]) VALUES(CAST(4 AS Numeric(18, 0)), N'SELECT p.Payment_ID,p.PayAmt,pay.paymentamount FROM (SELECT Payment_ID, SUM(AmountPaid) AS paymentamount FROM  dbo.PaymentDetail GROUP BY Payment_ID) pay JOIN dbo.Payment p ON p.Payment_ID = pay.Payment_ID WHERE p.PayAmt <> pay.paymentamount;', N'--Different amount in Payment and PaymentDetail', N'CORE      ', 1) ; INSERT[dbo].[QueryList] ([ID], [Query], [Comment], [Software], [IsActive]) VALUES(CAST(5 AS Numeric(18, 0)), N'SELECT Entity_ID,COUNT(*) FROM dbo.Workflow GROUP BY Entity_ID HAVING COUNT(*)>1;', N'--Duplicate workflow entry for same Time/Expense Entry', N'CORE      ', 1) ; INSERT[dbo].[QueryList] ([ID], [Query], [Comment], [Software], [IsActive]) VALUES(CAST(6 AS Numeric(18, 0)), N'SELECT * FROM dbo.BillPayment WHERE BillPayment_ID NOT IN (SELECT  MasterTransaction_Id FROM  dbo.Accounting WHERE MasterTransaction_Id IS NOT NULL);', N'--Vendor bill payments present in BillPayment/BillPaymentdetails table but not present in Accounting table', N'CORE      ', 1) ; INSERT[dbo].[QueryList] ([ID], [Query], [Comment], [Software], [IsActive]) VALUES(CAST(7 AS Numeric(18, 0)), N'SELECT id.Client_ID AS InvoiceClient, p.Client_ID AS ProjClient,id.Invoice_ID FROM dbo.InvoiceDetail id JOIN dbo.Project p ON p.Project_ID = id.Project_ID  WHERE id.Client_ID <> p.Client_ID;', N'--Different client in InvoiceDetails table and Project table/Wrong client in Invoice', N'CORE      ', 1) ; INSERT[dbo].[QueryList] ([ID], [Query], [Comment], [Software], [IsActive]) VALUES(CAST(8 AS Numeric(18, 0)), N'SELECT debt.DebtAmount,credit.CreditAmount,credit.Accounting_ID FROM (SELECT SUM(ISNULL(Amount, 0)) AS DebtAmount, Accounting_ID FROM dbo.AccountingDetail WHERE IsDebit = 1 GROUP BY Accounting_ID)debt INNER JOIN (SELECT SUM(ISNULL(Amount, 0)) AS CreditAmount, Accounting_ID FROM dbo.AccountingDetail WHERE IsDebit = 0 GROUP BY Accounting_ID)credit ON credit.Accounting_ID = debt.Accounting_ID WHERE debt.DebtAmount <> credit.CreditAmount;', N'--Sum  of Debit/Credits not equal', N'CORE      ', 1) ; INSERT[dbo].[QueryList] ([ID], [Query], [Comment], [Software], [IsActive]) VALUES(CAST(9 AS Numeric(18, 0)), N'SELECT DefaultGroup_ID,* FROM dbo.Project WHERE DefaultGroup_ID NOT IN (SELECT Group_ID FROM dbo.Groups );', N'--Orphan default group in Projects', N'CORE      ', 1) ; INSERT[dbo].[QueryList] ([ID], [Query], [Comment], [Software], [IsActive]) VALUES(CAST(10 AS Numeric(18, 0)), N'SELECT * FROM dbo.Accounting WHERE TransactionType=450 AND Transaction_Id NOT  IN(SELECT VendorBill_ID FROM  dbo.VendorBill );', N'--Orphan vendor bills/vendor bill details present in accounting tables but deleted from vendor bill tables', N'CORE      ', 1) ; INSERT[dbo].[QueryList] ([ID], [Query], [Comment], [Software], [IsActive]) VALUES(CAST(11 AS Numeric(18, 0)), N'SELECT * FROM dbo.TimeEntry WHERE VendorBill_ID IS NOT NULL AND VendorBill_ID  NOT IN (SELECT VendorBill_ID FROM dbo.VendorBill);', N'--Orphan VBs in Time Entry', N'CORE      ', 1) ; INSERT[dbo].[QueryList] ([ID], [Query], [Comment], [Software], [IsActive]) VALUES(CAST(12 AS Numeric(18, 0)), N'SELECT * FROM dbo.ExpenseLog WHERE VendorBill_ID IS NOT NULL AND VendorBill_ID  NOT IN (SELECT VendorBill_ID FROM dbo.VendorBill);', N'--Orphan VBs in Expense Entry', N'CORE      ', 1) ; INSERT[dbo].[QueryList] ([ID], [Query], [Comment], [Software], [IsActive]) VALUES(CAST(14 AS Numeric(18, 0)), N'SELECT * FROM dbo.TimeEntry WHERE Invoice_ID IS NOT NULL AND Invoice_ID  NOT IN (SELECT Invoice_ID FROM dbo.Invoice);', N'--Orphan Invoices in Time Entry', N'CORE      ', 1) ; INSERT[dbo].[QueryList] ([ID], [Query], [Comment], [Software], [IsActive]) VALUES(CAST(15 AS Numeric(18, 0)), N'SELECT * FROM dbo.ExpenseLog WHERE Invoice_ID IS NOT NULL AND Invoice_ID  NOT IN (SELECT Invoice_ID FROM dbo.Invoice);', N'--Orphan Invoices in Expense Entry', N'CORE      ', 1) ; INSERT[dbo].[QueryList] ([ID], [Query], [Comment], [Software], [IsActive]) VALUES(CAST(16 AS Numeric(18, 0)), N'SELECT * FROM dbo.CheckExpenseLineItem WHERE Check_ID NOT IN (SELECT Check_ID FROM dbo.Checks);', N'--Orphan Checks in CheckExpenseLineItem', N'CORE      ', 1) ; INSERT[dbo].[QueryList] ([ID], [Query], [Comment], [Software], [IsActive]) VALUES(CAST(17 AS Numeric(18, 0)), N'SELECT * FROM dbo.Checks WHERE IsBillPayment =1 AND Check_ID NOT IN (SELECT Entity_ID FROM dbo.BillPayment WHERE Entity_ID IS NOT NULL);', N'--Orphan check in Checks Payment deleted in BillPaymnet', N'CORE      ', 1) ;  INSERT[dbo].[QueryList] ([ID], [Query], [Comment], [Software], [IsActive]) VALUES(CAST(19 AS Numeric(18, 0)), N'SELECT * FROM dbo.VendorBillExpenseItem WHERE VendorBill_ID NOT IN (SELECT VendorBill_ID FROM dbo.VendorBill);', N'--Orphan VB in VendorBillExpenseItem', N'CORE      ', 1) ;  INSERT[dbo].[QueryList] ([ID], [Query], [Comment], [Software], [IsActive]) VALUES(CAST(20 AS Numeric(18, 0)), N'SELECT * FROM dbo.BillPaymentDetail WHERE BillPayment_ID NOT IN (SELECT BillPayment_ID FROM dbo.BillPayment);', N'--Orphan bill paymnet in BillPaymentDetail', N'CORE      ', 1) ;  INSERT[dbo].[QueryList] ([ID], [Query], [Comment], [Software], [IsActive]) VALUES(CAST(21 AS Numeric(18, 0)), N'SELECT * FROM dbo.InvoiceDetail WHERE Invoice_ID NOT IN (SELECT Invoice_ID FROM dbo.Invoice);', N'--Orphan Invoices in InvoiceDetail', N'CORE      ', 1) ;  INSERT[dbo].[QueryList] ([ID], [Query], [Comment], [Software], [IsActive]) VALUES(CAST(22 AS Numeric(18, 0)), N'SELECT * FROM dbo.CreditCardExpenseItem WHERE CreditCardPayment_ID NOT IN (SELECT CreditCardPayment_ID FROM dbo.CreditCardPayment);', N'--Orphan Credit card payment in CreditCardExpenseItem ', N'CORE      ', 1) ; INSERT[dbo].[QueryList] ([ID], [Query], [Comment], [Software], [IsActive]) VALUES(CAST(23 AS Numeric(18, 0)), N'SELECT * FROM dbo.PaymentDetail WHERE Payment_ID NOT IN (SELECT Payment_ID FROM dbo.Payment);', N'--Orphan Invoice Payments in PaymentDetail', N'CORE      ', 1) ;  INSERT[dbo].[QueryList] ([ID], [Query], [Comment], [Software], [IsActive]) VALUES(CAST(24 AS Numeric(18, 0)), N'SELECT * FROM dbo.Invoice i JOIN(SELECT Invoice_ID, SUM(Amount) AS amt FROM dbo.InvoiceDetail GROUP BY Invoice_ID)id ON id.Invoice_ID = i.Invoice_ID WHERE i.InvoiceAmount <> id.amt;', N'-- Different amount in Invoice and InvoiceDetails for same invoice ', N'CORE      ', 1) ;  INSERT[dbo].[QueryList] ([ID], [Query], [Comment], [Software], [IsActive]) VALUES(CAST(26 AS Numeric(18, 0)), N'SELECT * FROM dbo.Payment i JOIN (SELECT Payment_ID, SUM(AmountPaid) AS amt FROM dbo.PaymentDetail GROUP BY Payment_ID)id ON id.Payment_ID = i.Payment_ID WHERE i.PayAmt <> id.amt;', N'-- Different amount in Payment and PaymentDetails for same Payment', N'CORE      ', 1) ; INSERT[dbo].[QueryList] ([ID], [Query], [Comment], [Software], [IsActive]) VALUES(CAST(27 AS Numeric(18, 0)), N'SELECT el.VendorBill_ID,el.Reimbursable,el.ExpenseLog_ID,* FROM dbo.ExpenseLog el JOIN dbo.Workflow wf ON el.ExpenseLog_ID=wf.Entity_ID WHERE el.Reimbursable = 1 AND el.VendorBill_ID IS NULL AND wf.WorkflowAction = 2 ;', N'----Approved Reimbursable Expenses with NULL Vendor Bill IDs', N'CORE      ', 1) ;  INSERT[dbo].[QueryList] ([ID], [Query], [Comment], [Software], [IsActive]) VALUES(CAST(28 AS Numeric(18, 0)), N'SELECT p1.Client_ID, p1.RetainerPaymentTaken,p2.RetainerAmtApplied FROM  (SELECT Client_ID, SUM(PayAmt) AS RetainerPaymentTaken FROM dbo.Payment WHERE PayRetainer = 1 GROUP BY Client_ID) p1 JOIN (SELECT Client_ID, SUM(PayAmt) AS RetainerAmtApplied FROM dbo.Payment WHERE PayMethod = -1 GROUP BY Client_ID) p2 ON p2.Client_ID = p1.Client_ID WHERE p2.RetainerAmtApplied > p1.RetainerPaymentTaken;', N'-- --Retainer applied more than retainer taken  ', N'CORE      ', 1) ;  INSERT[dbo].[QueryList] ([ID], [Query], [Comment], [Software], [IsActive]) VALUES(CAST(29 AS Numeric(18, 0)), N'SELECT * FROM dbo.PaymentDetail WHERE Payment_ID IN (SELECT Payment_ID FROM dbo.Payment WHERE PayMethod=-1) AND AmountPaid<> CRA+PRA+PPRA;', N'-- CRA+PRA+PPRA <> AMOUNT PAID', N'CORE      ', 1) ;  INSERT[dbo].[QueryList] ([ID], [Query], [Comment], [Software], [IsActive]) VALUES(CAST(30 AS Numeric(18, 0)), N'SELECT * FROM dbo.Project WHERE Project_ID IN (SELECT RootProject_ID FROM dbo.InvoiceDetail WHERE RootProject_ID IS NOT NULL) AND ProjectLevel <>0;', N'----Child project set as Root Project ', N'CORE      ', 1) ;  INSERT[dbo].[QueryList] ([ID], [Query], [Comment], [Software], [IsActive]) VALUES(CAST(31 AS Numeric(18, 0)), N'SELECT id.RootProject_ID,p.ParentProject_ID,id.Project_ID,* FROM dbo.InvoiceDetail id JOIN dbo.Project p ON p.Project_ID = id.Project_ID WHERE p.ParentProject_ID IS NOT NULL AND id.RootProject_ID IS NULL AND id.InvoiceDetail_ID IN (SELECT InvoiceDetail_ID FROM dbo.PaymentDetail WHERE(PPRA <> 0 OR PRA <> 0) AND InvoiceDetail_ID IS NOT NULL) ;', N'---Root project as NULL in Invoice Details for retainer applied', N'CORE      ', 1) ;  INSERT[dbo].[QueryList] ([ID], [Query], [Comment], [Software], [IsActive]) VALUES(CAST(32 AS Numeric(18, 0)), N'SELECT a.TransactionAmount,i.InvoiceAmount, (a.TransactionAmount-i.InvoiceAmount) AS diff,i.Invoice_ID FROM dbo.Accounting a JOIN dbo.Invoice i ON i.Invoice_ID=a.MasterTransaction_Id WHERE a.TransactionType IN (''250'',''12'',''22'',''200'') AND  a.TransactionAmount<>i.InvoiceAmount;', N'---- Different amount for Invoices in Invoice table and Accounting table', N'CORE      ', 1) ;SET IDENTITY_INSERT [dbo].[QueryList] OFF; ";

                count = ClassCoreUtility.executeSQLInsertUpdate(SQL, cmbSQLServer.Text.ToString(), txtUserID.Text.ToString(), txtPassword.Text.ToString());

                if (count > 0)
                {
                    
                    MessageBox.Show("QueryList table created sucessfully inside Database CoreUtility ");
                }
                else
                    MessageBox.Show("Querylist table not created!! try again");


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }



        }

        private void btnLoadServers_Click(object sender, EventArgs e)
        {
            try
            {
                ClassCoreUtility.CaluculateAll(metroProgressBar2);
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

        private void btnConnectDB_Click(object sender, EventArgs e)
        {
            try
            {
               
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

                if (String.IsNullOrEmpty(txtPassword.Text.ToString()) || string.IsNullOrEmpty(txtUserID.Text.ToString()))
                    MessageBox.Show(" Please enter correct User credentials");


                // Set connection string with selected server and integrated security      
                connectString = "Data Source=" + this.server + ";Integrated Security=false;Initial Catalog=master;User ID=" +
                    txtUserID.Text + ";Password=" + txtPassword.Text + ";";

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

        private void btnLoadQueries_Click(object sender, EventArgs e)
        {
            string SQL;
            string connectString;
            List<string> listDataBases = new List<string>();

            try
            {
                //Check whether Database Coreutility exists on the SQL Server                

                if (!(ClassCoreUtility.checkIfDatabaseExistsOnSQL("CoreUtility", cmbSQLServer.Text.ToString(), txtUserID.Text.ToString(), txtPassword.Text.ToString())))
                {
                    MessageBox.Show("Create Querylist table first!!");
                    return;
                }


                // Set connection string with selected server and integrated security      
                connectString = "Data Source=" + this.server + ";Integrated Security=false;Initial Catalog=master;User ID=" +
                    txtUserID.Text + ";Password=" + txtPassword.Text + ";";

                using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(connectString))
                {
                    // Open connection
                    con.Open();

                    //Get QueryList names in server in a datareader 
                    SQL = "SELECT Comment FROM CoreUtility.dbo.QueryList WHERE Software='CORE' AND IsActive=1;";

                    System.Data.SqlClient.SqlCommand com =
                                 new System.Data.SqlClient.SqlCommand(SQL, con);
                    System.Data.SqlClient.SqlDataReader dr = com.ExecuteReader();

                    while (dr.Read())
                    {
                        listDataBases.Add(dr[0].ToString());
                    }

                    // Set Querylist comment list as combobox’s datasource
                    this.cmbQueryList.DataSource = listDataBases;
                   


                }
            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnClearQuery_Click(object sender, EventArgs e)
        {
            txtComment.Text = "";
            txtSQLQuery.Text = "";
        }

        private void btnLoadQueryDetails_Click(object sender, EventArgs e)
        {
            string SQL;
            string connectString;            

            try
            {
                //Check whether Database Coreutility exists on the SQL Server                

                if (!(ClassCoreUtility.checkIfDatabaseExistsOnSQL("CoreUtility", cmbSQLServer.Text.ToString(), txtUserID.Text.ToString(), txtPassword.Text.ToString())))
                {
                    MessageBox.Show("Create Querylist table first!!");
                    return;
                }

                //Check if Queries loaded

                if (cmbQueryList.Text=="")
                {
                    MessageBox.Show("Load Queries First ");
                    return;
                }
                

                // Set connection string with selected server and integrated security      
                connectString = "Data Source=" + this.server + ";Integrated Security=false;Initial Catalog=master;User ID=" +
                    txtUserID.Text + ";Password=" + txtPassword.Text + ";";

                txtComment.Text = cmbQueryList.Text;

                using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(connectString))
                {
                    // Open connection
                    con.Open();

                    //Get QueryList names in server in a datareader 
                    SQL = "SELECT Query FROM CoreUtility.dbo.QueryList WHERE Software='CORE' AND IsActive=1 and Comment='"  + cmbQueryList.Text+  "';";

                    System.Data.SqlClient.SqlCommand com =
                                 new System.Data.SqlClient.SqlCommand(SQL, con);
                    System.Data.SqlClient.SqlDataReader dr = com.ExecuteReader();

                    while (dr.Read())
                    {
                        txtSQLQuery.Text = dr["Query"].ToString();
                    }

                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnCopyQuery_Click(object sender, EventArgs e)
        {
            if (txtSQLQuery.Text == "")
            {
                MessageBox.Show("Nothing to be Copied");
            }
            else
            {
                                
                Clipboard.SetText(txtSQLQuery.Text);
                MessageBox.Show("Query Copied!!");
            }
        }

        private void btnValidateQuery_Click(object sender, EventArgs e)
        {
            string SQL;
            int count;
            try
            {
                if(txtSQLQuery.Text=="")
                {
                    MessageBox.Show("Kindly load the Query first");
                    return;
                }

                //Check whether database is Core Database  

                if (!(ClassCoreUtility.checkIfCoreDatabase(cmbDatabases.Text.ToString(), this.server.ToString(), txtUserID.Text.ToString(), txtPassword.Text.ToString())))
                {
                    MessageBox.Show("Selected Database not a Core Database!!");
                    return;
                }



                //Validate Query

                SQL = txtSQLQuery.Text;
                count = ClassCoreUtility.executeSQLSelect(SQL, this.server, txtUserID.Text, txtPassword.Text, cmbDatabases.Text);
                if (count == 1 ) 
                {
                    MessageBox.Show("Query validated sucessfully on SQL Server");
                }
                if (count == 0)
                {
                    MessageBox.Show("Query validated sucessfully on SQL Server");
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show("Error in Query. Correct your Query!!!");
            }
        }

        private void btnAddQuery_Click(object sender, EventArgs e)
        {
            string SQL;
            int count;

            try
            {
                if (txtSQLQuery.Text == "")
                {
                    MessageBox.Show("Kindly Write the Query first");
                    return;
                }

                //Check whether Database Coreutility exists on the SQL Server                

                if (!(ClassCoreUtility.checkIfDatabaseExistsOnSQL("CoreUtility", cmbSQLServer.Text.ToString(), txtUserID.Text.ToString(), txtPassword.Text.ToString())))
                {
                    MessageBox.Show("Create Querylist table first!!");
                    return;
                }


                //Check if Query already added to Querylist table

                SQL = "SELECT * FROM CoreUtility.dbo.QueryList WHERE Query='" +  txtSQLQuery.Text + "';";
                count = ClassCoreUtility.executeSQLSelect(SQL, this.server, txtUserID.Text, txtPassword.Text, cmbDatabases.Text);
                if (count == 1)
                {
                    MessageBox.Show("Query already added to QueryList table");
                    return;
                }


                //Check if Comment already used in  Querylist table

                SQL = "";
                SQL = "SELECT * FROM CoreUtility.dbo.QueryList WHERE Comment='" + txtComment.Text + "';";
                count = ClassCoreUtility.executeSQLSelect(SQL, this.server, txtUserID.Text, txtPassword.Text, cmbDatabases.Text);
                if (count == 1)
                {
                    MessageBox.Show("Comment already usedin QueryList table. Please choose different comment!!");
                    return;
                }

                //Validate Query
                SQL = "";
                SQL = txtSQLQuery.Text;
                count = ClassCoreUtility.executeSQLSelect(SQL, this.server, txtUserID.Text, txtPassword.Text, cmbDatabases.Text);


                //Insert Query in QueryList table
                SQL = "";
                SQL += "INSERT INTO CoreUtility. dbo.QueryList(Query,Comment,Software,IsActive) VALUES( '" + txtSQLQuery.Text + "','" + txtComment.Text + "','CORE',1);";

                count = ClassCoreUtility.executeSQLInsertUpdate(SQL, this.server.ToString(), txtUserID.Text.ToString(), txtPassword.Text.ToString());

                if (count != 0)
                {

                    MessageBox.Show("Query added sucessfully");
                    btnLoadQueries.PerformClick();
                    txtComment.Text = "";
                    txtSQLQuery.Text = "";
                }
                else
                    MessageBox.Show("Query not added, Try Again !!");


            }
            catch(Exception ex)
            {
                MessageBox.Show("Error in Query.Kindly correct youq query first");
            }
        }

        private void btnDeleteQuery_Click(object sender, EventArgs e)
        {
            string SQL;
            int count;

            try
            {
                if (txtSQLQuery.Text == "")
                {
                    MessageBox.Show("Kindly Write the Query first");
                    return;
                }

                //Check whether Database Coreutility exists on the SQL Server                

                if (!(ClassCoreUtility.checkIfDatabaseExistsOnSQL("CoreUtility", cmbSQLServer.Text.ToString(), txtUserID.Text.ToString(), txtPassword.Text.ToString())))
                {
                    MessageBox.Show("Create Querylist table first!!");
                    return;
                }


                //Check if Query exists Querylist table

                SQL = "SELECT * FROM CoreUtility.dbo.QueryList WHERE Query='" + txtSQLQuery.Text + "';";
                count = ClassCoreUtility.executeSQLSelect(SQL, this.server, txtUserID.Text, txtPassword.Text, cmbDatabases.Text);
                if (count == 0)
                {
                    MessageBox.Show("Query not present in the QueryList table");
                    return;
                }


                //Delete Query in QueryList table
                SQL = "";
                SQL += "DELETE FROM CoreUtility.dbo.QueryList WHERE Query='" + txtSQLQuery.Text + "'AND Comment='" + txtComment.Text + "';";

                count = ClassCoreUtility.executeSQLInsertUpdate(SQL, this.server.ToString(), txtUserID.Text.ToString(), txtPassword.Text.ToString());

                if (count != 0)
                {
                    MessageBox.Show("Query deleted sucessfully");
                    btnLoadQueries.PerformClick();
                    txtComment.Text = "";
                    txtSQLQuery.Text = "";
                }
                else
                    MessageBox.Show("Query not deleted, Try Again !!");


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnEditQuery_Click(object sender, EventArgs e)
        {
            string SQL;
            int count;

            try
            {
                if (txtSQLQuery.Text == "")
                {
                    MessageBox.Show("Kindly Write the Query first");
                    return;
                }

                //Check whether Database Coreutility exists on the SQL Server                

                if (!(ClassCoreUtility.checkIfDatabaseExistsOnSQL("CoreUtility", cmbSQLServer.Text.ToString(), txtUserID.Text.ToString(), txtPassword.Text.ToString())))
                {
                    MessageBox.Show("Create Querylist table first!!");
                    return;
                }


                //Check if Query exists Querylist table

                SQL = "SELECT * FROM CoreUtility.dbo.QueryList WHERE comment='" + txtComment.Text + "';";
                count = ClassCoreUtility.executeSQLSelect(SQL, this.server, txtUserID.Text, txtPassword.Text, cmbDatabases.Text);
                if (count == 0)
                {
                    MessageBox.Show("Query not present in the QueryList table");
                    return;
                }


                //Validate Query
                //Validate Query
                SQL = "";
                SQL = txtSQLQuery.Text;
                count = ClassCoreUtility.executeSQLSelect(SQL, this.server, txtUserID.Text, txtPassword.Text, cmbDatabases.Text);


                //Edit Query in QueryList table
                SQL = "";
                SQL += "UPDATE CoreUtility.dbo.QueryList SET Query='" + txtSQLQuery.Text + "'WHERE Comment='" + txtComment.Text + "';";

                count = ClassCoreUtility.executeSQLInsertUpdate(SQL, this.server.ToString(), txtUserID.Text.ToString(), txtPassword.Text.ToString());

                if (count != 0)
                {

                    MessageBox.Show("Query edited sucessfully");
                    btnLoadQueries.PerformClick();
                    txtComment.Text = "";
                    txtSQLQuery.Text = "";
                }
                else
                    MessageBox.Show("Query not edited, Try Again !!");


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in Query.Kindly correct youq query first");
            }
        }

        private void btnDeleteQuerylistTable_Click(object sender, EventArgs e)
        {
            try
            {

                string SQL;
                int count;

                if (String.IsNullOrEmpty(cmbSQLServer.Text.ToString()) || string.IsNullOrEmpty(cmbDatabases.Text.ToString()))
                {
                    MessageBox.Show(" Please select UserID, Password, SQL Server or Coredatabase");
                    return;
                }



                //Check whether Database Coreutility exists on the SQL Server                

                if (!(ClassCoreUtility.checkIfDatabaseExistsOnSQL("CoreUtility", cmbSQLServer.Text.ToString(), txtUserID.Text.ToString(), txtPassword.Text.ToString())))
                {
                    MessageBox.Show("Database does not exist on SQL Server");
                    return;
                }

                //restore Querylist table to original form

                SQL = "";
                

             //   count = ClassCoreUtility.executeSQLInsertUpdate(SQL, cmbSQLServer.Text.ToString(), txtUserID.Text.ToString(), txtPassword.Text.ToString());



                // Create table Query List, Add Default valuse to the QueryList table
                SQL = "";
                SQL += "USE [CoreUtility]; DELETE FROM dbo.QueryList; SET IDENTITY_INSERT [dbo].[QueryList] ON ;  ";


                SQL += " INSERT[dbo].[QueryList]([ID], [Query], [Comment], [Software], [IsActive]) VALUES(CAST(1 AS Numeric(18, 0)), N'SELECT inv.InvoiceDetail_ID,inv.amt,pay.Payamt FROM (SELECT InvoiceDetail_ID, SUM(Amount) AS amt FROM  dbo.InvoiceDetail GROUP BY InvoiceDetail_ID) inv JOIN (SELECT InvoiceDetail_ID, SUM(AmountPaid) AS Payamt FROM  dbo.PaymentDetail GROUP BY InvoiceDetail_ID) pay ON pay.InvoiceDetail_ID = inv.InvoiceDetail_ID WHERE inv.amt <> pay.Payamt AND(ABS(inv.amt - pay.Payamt) BETWEEN 0 AND 1);', N'--Penny issue in invoices and Paymnets', N'CORE      ', 1) ;  INSERT[dbo].[QueryList] ([ID], [Query], [Comment], [Software], [IsActive]) VALUES(CAST(2 AS Numeric(18, 0)), N'SELECT inv.InvoiceDetail_ID,inv.amt,pay.Payamt FROM (SELECT InvoiceDetail_ID, SUM(Amount) AS amt FROM  dbo.InvoiceDetail GROUP BY InvoiceDetail_ID) inv JOIN (SELECT InvoiceDetail_ID, SUM(AmountPaid) AS Payamt FROM  dbo.PaymentDetail GROUP BY InvoiceDetail_ID) pay ON pay.InvoiceDetail_ID = inv.InvoiceDetail_ID WHERE inv.amt <> pay.Payamt AND pay.Payamt > inv.amt;', N'--Payment more than invoice', N'CORE      ', 1) ; INSERT[dbo].[QueryList] ([ID], [Query], [Comment], [Software], [IsActive]) VALUES(CAST(3 AS Numeric(18, 0)), N'SELECT i.Invoice_ID,i.InvoiceAmount,inv.amt FROM (SELECT Invoice_ID, SUM(Amount) AS amt FROM dbo.InvoiceDetail GROUP BY Invoice_ID)inv JOIN  dbo.Invoice i ON i.Invoice_ID = inv.Invoice_ID  WHERE i.InvoiceAmount <> inv.amt;', N'--Different amount in Invoice and InvoiceDetail for same invoice', N'CORE      ', 1)  ; INSERT[dbo].[QueryList] ([ID], [Query], [Comment], [Software], [IsActive]) VALUES(CAST(4 AS Numeric(18, 0)), N'SELECT p.Payment_ID,p.PayAmt,pay.paymentamount FROM (SELECT Payment_ID, SUM(AmountPaid) AS paymentamount FROM  dbo.PaymentDetail GROUP BY Payment_ID) pay JOIN dbo.Payment p ON p.Payment_ID = pay.Payment_ID WHERE p.PayAmt <> pay.paymentamount;', N'--Different amount in Payment and PaymentDetail', N'CORE      ', 1) ; INSERT[dbo].[QueryList] ([ID], [Query], [Comment], [Software], [IsActive]) VALUES(CAST(5 AS Numeric(18, 0)), N'SELECT Entity_ID,COUNT(*) FROM dbo.Workflow GROUP BY Entity_ID HAVING COUNT(*)>1;', N'--Duplicate workflow entry for same Time/Expense Entry', N'CORE      ', 1) ; INSERT[dbo].[QueryList] ([ID], [Query], [Comment], [Software], [IsActive]) VALUES(CAST(6 AS Numeric(18, 0)), N'SELECT * FROM dbo.BillPayment WHERE BillPayment_ID NOT IN (SELECT  MasterTransaction_Id FROM  dbo.Accounting WHERE MasterTransaction_Id IS NOT NULL);', N'--Vendor bill payments present in BillPayment/BillPaymentdetails table but not present in Accounting table', N'CORE      ', 1) ; INSERT[dbo].[QueryList] ([ID], [Query], [Comment], [Software], [IsActive]) VALUES(CAST(7 AS Numeric(18, 0)), N'SELECT id.Client_ID AS InvoiceClient, p.Client_ID AS ProjClient,id.Invoice_ID FROM dbo.InvoiceDetail id JOIN dbo.Project p ON p.Project_ID = id.Project_ID  WHERE id.Client_ID <> p.Client_ID;', N'--Different client in InvoiceDetails table and Project table/Wrong client in Invoice', N'CORE      ', 1) ; INSERT[dbo].[QueryList] ([ID], [Query], [Comment], [Software], [IsActive]) VALUES(CAST(8 AS Numeric(18, 0)), N'SELECT debt.DebtAmount,credit.CreditAmount,credit.Accounting_ID FROM (SELECT SUM(ISNULL(Amount, 0)) AS DebtAmount, Accounting_ID FROM dbo.AccountingDetail WHERE IsDebit = 1 GROUP BY Accounting_ID)debt INNER JOIN (SELECT SUM(ISNULL(Amount, 0)) AS CreditAmount, Accounting_ID FROM dbo.AccountingDetail WHERE IsDebit = 0 GROUP BY Accounting_ID)credit ON credit.Accounting_ID = debt.Accounting_ID WHERE debt.DebtAmount <> credit.CreditAmount;', N'--Sum  of Debit/Credits not equal', N'CORE      ', 1) ; INSERT[dbo].[QueryList] ([ID], [Query], [Comment], [Software], [IsActive]) VALUES(CAST(9 AS Numeric(18, 0)), N'SELECT DefaultGroup_ID,* FROM dbo.Project WHERE DefaultGroup_ID NOT IN (SELECT Group_ID FROM dbo.Groups );', N'--Orphan default group in Projects', N'CORE      ', 1) ; INSERT[dbo].[QueryList] ([ID], [Query], [Comment], [Software], [IsActive]) VALUES(CAST(10 AS Numeric(18, 0)), N'SELECT * FROM dbo.Accounting WHERE TransactionType=450 AND Transaction_Id NOT  IN(SELECT VendorBill_ID FROM  dbo.VendorBill );', N'--Orphan vendor bills/vendor bill details present in accounting tables but deleted from vendor bill tables', N'CORE      ', 1) ; INSERT[dbo].[QueryList] ([ID], [Query], [Comment], [Software], [IsActive]) VALUES(CAST(11 AS Numeric(18, 0)), N'SELECT * FROM dbo.TimeEntry WHERE VendorBill_ID IS NOT NULL AND VendorBill_ID  NOT IN (SELECT VendorBill_ID FROM dbo.VendorBill);', N'--Orphan VBs in Time Entry', N'CORE      ', 1) ; INSERT[dbo].[QueryList] ([ID], [Query], [Comment], [Software], [IsActive]) VALUES(CAST(12 AS Numeric(18, 0)), N'SELECT * FROM dbo.ExpenseLog WHERE VendorBill_ID IS NOT NULL AND VendorBill_ID  NOT IN (SELECT VendorBill_ID FROM dbo.VendorBill);', N'--Orphan VBs in Expense Entry', N'CORE      ', 1) ; INSERT[dbo].[QueryList] ([ID], [Query], [Comment], [Software], [IsActive]) VALUES(CAST(14 AS Numeric(18, 0)), N'SELECT * FROM dbo.TimeEntry WHERE Invoice_ID IS NOT NULL AND Invoice_ID  NOT IN (SELECT Invoice_ID FROM dbo.Invoice);', N'--Orphan Invoices in Time Entry', N'CORE      ', 1) ; INSERT[dbo].[QueryList] ([ID], [Query], [Comment], [Software], [IsActive]) VALUES(CAST(15 AS Numeric(18, 0)), N'SELECT * FROM dbo.ExpenseLog WHERE Invoice_ID IS NOT NULL AND Invoice_ID  NOT IN (SELECT Invoice_ID FROM dbo.Invoice);', N'--Orphan Invoices in Expense Entry', N'CORE      ', 1) ; INSERT[dbo].[QueryList] ([ID], [Query], [Comment], [Software], [IsActive]) VALUES(CAST(16 AS Numeric(18, 0)), N'SELECT * FROM dbo.CheckExpenseLineItem WHERE Check_ID NOT IN (SELECT Check_ID FROM dbo.Checks);', N'--Orphan Checks in CheckExpenseLineItem', N'CORE      ', 1) ; INSERT[dbo].[QueryList] ([ID], [Query], [Comment], [Software], [IsActive]) VALUES(CAST(17 AS Numeric(18, 0)), N'SELECT * FROM dbo.Checks WHERE IsBillPayment =1 AND Check_ID NOT IN (SELECT Entity_ID FROM dbo.BillPayment WHERE Entity_ID IS NOT NULL);', N'--Orphan check in Checks Payment deleted in BillPaymnet', N'CORE      ', 1) ;  INSERT[dbo].[QueryList] ([ID], [Query], [Comment], [Software], [IsActive]) VALUES(CAST(19 AS Numeric(18, 0)), N'SELECT * FROM dbo.VendorBillExpenseItem WHERE VendorBill_ID NOT IN (SELECT VendorBill_ID FROM dbo.VendorBill);', N'--Orphan VB in VendorBillExpenseItem', N'CORE      ', 1) ;  INSERT[dbo].[QueryList] ([ID], [Query], [Comment], [Software], [IsActive]) VALUES(CAST(20 AS Numeric(18, 0)), N'SELECT * FROM dbo.BillPaymentDetail WHERE BillPayment_ID NOT IN (SELECT BillPayment_ID FROM dbo.BillPayment);', N'--Orphan bill paymnet in BillPaymentDetail', N'CORE      ', 1) ;  INSERT[dbo].[QueryList] ([ID], [Query], [Comment], [Software], [IsActive]) VALUES(CAST(21 AS Numeric(18, 0)), N'SELECT * FROM dbo.InvoiceDetail WHERE Invoice_ID NOT IN (SELECT Invoice_ID FROM dbo.Invoice);', N'--Orphan Invoices in InvoiceDetail', N'CORE      ', 1) ;  INSERT[dbo].[QueryList] ([ID], [Query], [Comment], [Software], [IsActive]) VALUES(CAST(22 AS Numeric(18, 0)), N'SELECT * FROM dbo.CreditCardExpenseItem WHERE CreditCardPayment_ID NOT IN (SELECT CreditCardPayment_ID FROM dbo.CreditCardPayment);', N'--Orphan Credit card payment in CreditCardExpenseItem ', N'CORE      ', 1) ; INSERT[dbo].[QueryList] ([ID], [Query], [Comment], [Software], [IsActive]) VALUES(CAST(23 AS Numeric(18, 0)), N'SELECT * FROM dbo.PaymentDetail WHERE Payment_ID NOT IN (SELECT Payment_ID FROM dbo.Payment);', N'--Orphan Invoice Payments in PaymentDetail', N'CORE      ', 1) ;  INSERT[dbo].[QueryList] ([ID], [Query], [Comment], [Software], [IsActive]) VALUES(CAST(24 AS Numeric(18, 0)), N'SELECT * FROM dbo.Invoice i JOIN(SELECT Invoice_ID, SUM(Amount) AS amt FROM dbo.InvoiceDetail GROUP BY Invoice_ID)id ON id.Invoice_ID = i.Invoice_ID WHERE i.InvoiceAmount <> id.amt;', N'-- Different amount in Invoice and InvoiceDetails for same invoice ', N'CORE      ', 1) ;  INSERT[dbo].[QueryList] ([ID], [Query], [Comment], [Software], [IsActive]) VALUES(CAST(26 AS Numeric(18, 0)), N'SELECT * FROM dbo.Payment i JOIN (SELECT Payment_ID, SUM(AmountPaid) AS amt FROM dbo.PaymentDetail GROUP BY Payment_ID)id ON id.Payment_ID = i.Payment_ID WHERE i.PayAmt <> id.amt;', N'-- Different amount in Payment and PaymentDetails for same Payment', N'CORE      ', 1) ; INSERT[dbo].[QueryList] ([ID], [Query], [Comment], [Software], [IsActive]) VALUES(CAST(27 AS Numeric(18, 0)), N'SELECT el.VendorBill_ID,el.Reimbursable,el.ExpenseLog_ID,* FROM dbo.ExpenseLog el JOIN dbo.Workflow wf ON el.ExpenseLog_ID=wf.Entity_ID WHERE el.Reimbursable = 1 AND el.VendorBill_ID IS NULL AND wf.WorkflowAction = 2 ;', N'----Approved Reimbursable Expenses with NULL Vendor Bill IDs', N'CORE      ', 1) ;  INSERT[dbo].[QueryList] ([ID], [Query], [Comment], [Software], [IsActive]) VALUES(CAST(28 AS Numeric(18, 0)), N'SELECT p1.Client_ID, p1.RetainerPaymentTaken,p2.RetainerAmtApplied FROM  (SELECT Client_ID, SUM(PayAmt) AS RetainerPaymentTaken FROM dbo.Payment WHERE PayRetainer = 1 GROUP BY Client_ID) p1 JOIN (SELECT Client_ID, SUM(PayAmt) AS RetainerAmtApplied FROM dbo.Payment WHERE PayMethod = -1 GROUP BY Client_ID) p2 ON p2.Client_ID = p1.Client_ID WHERE p2.RetainerAmtApplied > p1.RetainerPaymentTaken;', N'-- --Retainer applied more than retainer taken  ', N'CORE      ', 1) ;  INSERT[dbo].[QueryList] ([ID], [Query], [Comment], [Software], [IsActive]) VALUES(CAST(29 AS Numeric(18, 0)), N'SELECT * FROM dbo.PaymentDetail WHERE Payment_ID IN (SELECT Payment_ID FROM dbo.Payment WHERE PayMethod=-1) AND AmountPaid<> CRA+PRA+PPRA;', N'-- CRA+PRA+PPRA <> AMOUNT PAID', N'CORE      ', 1) ;  INSERT[dbo].[QueryList] ([ID], [Query], [Comment], [Software], [IsActive]) VALUES(CAST(30 AS Numeric(18, 0)), N'SELECT * FROM dbo.Project WHERE Project_ID IN (SELECT RootProject_ID FROM dbo.InvoiceDetail WHERE RootProject_ID IS NOT NULL) AND ProjectLevel <>0;', N'----Child project set as Root Project ', N'CORE      ', 1) ;  INSERT[dbo].[QueryList] ([ID], [Query], [Comment], [Software], [IsActive]) VALUES(CAST(31 AS Numeric(18, 0)), N'SELECT id.RootProject_ID,p.ParentProject_ID,id.Project_ID,* FROM dbo.InvoiceDetail id JOIN dbo.Project p ON p.Project_ID = id.Project_ID WHERE p.ParentProject_ID IS NOT NULL AND id.RootProject_ID IS NULL AND id.InvoiceDetail_ID IN (SELECT InvoiceDetail_ID FROM dbo.PaymentDetail WHERE(PPRA <> 0 OR PRA <> 0) AND InvoiceDetail_ID IS NOT NULL) ;', N'---Root project as NULL in Invoice Details for retainer applied', N'CORE      ', 1) ;  INSERT[dbo].[QueryList] ([ID], [Query], [Comment], [Software], [IsActive]) VALUES(CAST(32 AS Numeric(18, 0)), N'SELECT a.TransactionAmount,i.InvoiceAmount, (a.TransactionAmount-i.InvoiceAmount) AS diff,i.Invoice_ID FROM dbo.Accounting a JOIN dbo.Invoice i ON i.Invoice_ID=a.MasterTransaction_Id WHERE a.TransactionType IN (''250'',''12'',''22'',''200'') AND  a.TransactionAmount<>i.InvoiceAmount;', N'---- Different amount for Invoices in Invoice table and Accounting table', N'CORE      ', 1) ; SET IDENTITY_INSERT [dbo].[QueryList] OFF;  ";

                count = ClassCoreUtility.executeSQLInsertUpdate(SQL, cmbSQLServer.Text.ToString(), txtUserID.Text.ToString(), txtPassword.Text.ToString());

                if (count > 0)
                {

                    MessageBox.Show("QueryList table successfully restored ");
                }
                else
                    MessageBox.Show("Operation not successfull,,Try again!!");


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
    }
}
