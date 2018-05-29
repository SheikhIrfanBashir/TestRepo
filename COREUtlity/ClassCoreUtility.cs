using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace COREUtlity
{
    class ClassCoreUtility
    {
        public static string DatabaseType;
        public static int executeSQLSelect(string SQL,string ServerName, string User_ID, string Password, string database)
        {
            
                // Set connection string with selected server and integrated security      
                string connectString;
                SqlCommand com, comdb;
                SqlDataReader rd;



                connectString = "Data Source=" + ServerName + ";Integrated Security=false;Initial Catalog=master;User ID=" +
                    User_ID + ";Password=" + Password + ";";

                using (SqlConnection con = new SqlConnection(connectString))
                {
                    // Open connection
                    con.Open();

                    // Use current database
                    if (database != "CoreUtility")
                    {
                        comdb = new SqlCommand("USE [" + database + "];", con);
                        comdb.ExecuteNonQuery();
                    }

                    //get number of rows effected/Present                 
                    com = new SqlCommand(SQL, con);

                    rd = com.ExecuteReader();
                    if (rd.HasRows)
                        return (1);
                    else
                        return (0);

                }
            
            
        }

        public static int executeSQLInsertUpdate(string SQL, string ServerName, string User_ID, string Password)
        {
            // Set connection string with selected server and integrated security      
            string connectString;
            SqlCommand com;

            connectString = "Data Source=" + ServerName + ";Integrated Security=false;Initial Catalog=master;User ID=" +
                User_ID + ";Password=" + Password+ ";";

            using (SqlConnection con = new SqlConnection(connectString))
            {
                // Open connection
                con.Open();

                //get number of rows effected/Present                 
                com = new SqlCommand(SQL, con);
                return (com.ExecuteNonQuery());

            }
        }

        public static Boolean checkIfDatabaseExistsOnSQL(string Database_ID, string ServerName, string User_ID, string Password)
        {
            string SQL = "SELECT * FROM sys.databases WHERE name='" + Database_ID + "';";
            int count= executeSQLSelect(SQL,ServerName,User_ID,Password,Database_ID);
            if (count == 1)
                return (true);
            else
                return (false);
        }

        public static Boolean checkIfCoreDatabase(string Database_ID, string ServerName, string User_ID, string Password)
        {
           string SQL = "Use [" + Database_ID + "];";
            SQL += "SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME='company' AND COLUMN_NAME='CoreDBVersion';";
            int count = executeSQLSelect(SQL, ServerName, User_ID, Password,Database_ID);
            if (count == 1)
                return (true);
            else
                return (false);
        }


        public static Boolean checkIfBQWSDatabase(string Database_ID, string ServerName, string User_ID, string Password)
        {
            string SQL = "Use [" + Database_ID + "];";
            SQL += "SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME='BQUsers';";
            int count = executeSQLSelect(SQL, ServerName, User_ID, Password, Database_ID);
            if (count == 1)
                return (true);
            else
                return (false);
        }


        public static Boolean checkIfAOEODatabase(string Database_ID, string ServerName, string User_ID, string Password)
        {
            string SQL = "Use [" + Database_ID + "];";
            SQL += "SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME='AOSTable';";
            int count = executeSQLSelect(SQL, ServerName, User_ID, Password, Database_ID);
            if (count == 1)
                return (true);
            else
                return (false);
        }




        public static Boolean checkIfDatabaseAlreadyAddedToCore(string Database_ID, string ServerName, string User_ID, string Password)
        {
            string SQL = "SELECT DatabaseID FROM BQECoreHost.dbo.Company WHERE DatabaseID ='" + Database_ID + "';";
            int count = executeSQLSelect(SQL, ServerName, User_ID, Password,Database_ID);
            if (count == 1)
                return (true);
            else
                return (false);
        }

        public static Boolean checkIfAccountPresentInCore(string Account, string ServerName, string User_ID, string Password,string database)
        {
           string SQL = "SELECT ID FROM BQECoreHost.dbo.Account WHERE Email = '" + Account + "'; ";
            int count = executeSQLSelect(SQL, ServerName, User_ID, Password,database);
            if (count == 1)
                return (true);
            else
                return (false);
        }

        public static Boolean checkIfCoreDBHasFullAccessUser(string Database_Id, string ServerName, string User_ID, string Password)
        {
            string SQL = "SELECT TOP 1 Employee_ID FROM [" + Database_Id + "].dbo.Employee WHERE  Employee_ID IN( SELECT Employee_ID FROM [" + Database_Id + "].dbo.Security WHERE SecurityTemplate_ID = '5DE836B0-F3F2-45DD-8A44-0C689E8B2ACD')";
            int count = executeSQLSelect(SQL, ServerName, User_ID, Password,Database_Id);
            if (count == 1)
                return (true);
            else
                return (false);
        }




        public static void CaluculateAll(System.Windows.Forms.ProgressBar progressBar)
        {
            progressBar.Value = 1;
            progressBar.Maximum = 1000000;
            progressBar.Minimum = 1;
            progressBar.Step = 1;

            for (int j = 0; j < 1000000; j++)
            {
                double pow = Math.Pow(j, j); //Calculation
                progressBar.PerformStep();
            }
                        
        }

      

        public static string getCurrentCoreLoginUser(string database, string CoreAccount, string server, string userID, string password)
        {
            //Get current login Core User           

            // Set connection string with selected server and integrated security      
            string connectString = "Data Source=" + server + ";Integrated Security=false;Initial Catalog=master;User ID=" +
                 userID + ";Password=" + password + ";";
            string strr="";

            using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(connectString))
            {
                // Open connection
                con.Open();

                string SQL = "SELECT EmployeeID FROM [" + database + "].dbo.Employee WHERE HostAccount_ID IN (SELECT ID FROM BQECoreHost.dbo.Account WHERE Email='" + CoreAccount + "')";

                SqlCommand com =
                             new SqlCommand(SQL, con);
                SqlDataReader dr = com.ExecuteReader();
               
                while (dr.Read())
                {
                  strr =  dr[0].ToString();
                }
                
                return (strr);

            }
        }

        //Check type of database whether CORE/BQ/AO
        public static string CheckDatabaseType(string Database_ID, string ServerName, string User_ID, string Password)
        {
            if (checkIfCoreDatabase(Database_ID, ServerName, User_ID, Password))
            {
                DatabaseType = "CORE";
            }

           else if (checkIfBQWSDatabase(Database_ID, ServerName, User_ID, Password))
            {
                DatabaseType = "BQWS";
            }

           else if (checkIfAOEODatabase(Database_ID, ServerName, User_ID, Password))
            {
                DatabaseType = "AOEO";
            }

            else
            {
                DatabaseType = "NONE";
            }

            return (DatabaseType);
        }


        //Check BQ database version number

        public static string CheckBQDatabaseVersionNumber(string Database_ID, string ServerName, string User_ID, string Password)
        {
            // Set connection string with selected server and integrated security      
            string connectString = "Data Source=" + ServerName + ";Integrated Security=false;Initial Catalog=master;User ID=" +
                 User_ID + ";Password=" + Password + ";";

            string versionNumber = "";

            string SQL = " use[" + Database_ID + "];" +
                           " SELECT VersionNum FROM dbo.Miscellaneous ";

            using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(connectString))
            {
                con.Open();
                System.Data.SqlClient.SqlCommand com =
                         new System.Data.SqlClient.SqlCommand(SQL, con);
                System.Data.SqlClient.SqlDataReader dr = com.ExecuteReader();

                

                        while (dr.Read())
                {
                    versionNumber = dr[0].ToString();

                }
            }
            return (versionNumber);
        }



        //Check AO database version number

        public static string CheckAODatabaseVersionNumber(string Database_ID, string ServerName, string User_ID, string Password)
        {
            // Set connection string with selected server and integrated security      
            string connectString = "Data Source=" + ServerName + ";Integrated Security=false;Initial Catalog=master;User ID=" +
                 User_ID + ";Password=" + Password + ";";

            string versionNumber = "";

            string SQL = " use[" + Database_ID + "];" +
                           " SELECT major_version FROM dbo.ol_client; ";

            using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(connectString))
            {
                con.Open();
                System.Data.SqlClient.SqlCommand com =
                         new System.Data.SqlClient.SqlCommand(SQL, con);
                System.Data.SqlClient.SqlDataReader dr = com.ExecuteReader();



                while (dr.Read())
                {
                    versionNumber = dr[0].ToString();

                }
            }
            return (versionNumber);
        }



        //Get discrepency in BQ database
        public static Dictionary<string, string> GetDiscrepencyBQ(string Database_Id, string ServerName, string User_ID, string Password)
        {
            Dictionary<string, string> lstDiscrepency = new Dictionary<string, string>();
            Dictionary<string, string> lstDiscrepencyinDatabase = new Dictionary<string, string>();

            //Add sql queries to check discrepency
            

            lstDiscrepency.Add(" -- ISSUE: Problem in APTransactionDetails regarding the difference in the Sum of Credits and Debits  ", "select sum(creditamount) , sum(debitamount), aptransactionid from aptransactiondetails group by aptransactionid having sum(creditamount) <> sum(debitamount);");
            

            foreach (var item in lstDiscrepency)
            {
                string SQL = item.Value;
                int count = executeSQLSelect(SQL, ServerName, User_ID, Password, Database_Id);
                if (count == 1)
                {
                    string comment = item.Key.ToString();
                    lstDiscrepencyinDatabase.Add(comment, SQL);
                }
            }

            return (lstDiscrepencyinDatabase);
        }




        //Get discrepency in AO database
        public static Dictionary<string, string> GetDiscrepencyAO(string Database_Id, string ServerName, string User_ID, string Password)
        {
            Dictionary<string, string> lstDiscrepency = new Dictionary<string, string>();
            Dictionary<string, string> lstDiscrepencyinDatabase = new Dictionary<string, string>();

            //Add sql queries to check discrepency


            lstDiscrepency.Add("-- ISSUE: Job Code that has been used in Invoice Line Items doesn't exist in the Project Job Code table  ", "SELECT * FROM dbo.invoice_lineitems il LEFT JOIN dbo.project_job_code pjc ON pjc.project_job_code_id = il.project_jobcode_id WHERE pjc.project_job_code_id IS NULL AND il.project_jobcode_id IS NOT NULL;");


            foreach (var item in lstDiscrepency)
            {
                string SQL = item.Value;
                int count = executeSQLSelect(SQL, ServerName, User_ID, Password, Database_Id);
                if (count == 1)
                {
                    string comment = item.Key.ToString();
                    lstDiscrepencyinDatabase.Add(comment, SQL);
                }
            }

            return (lstDiscrepencyinDatabase);
        }


        //Get discrepency in Core database from Core utility Database
        public static Dictionary<string, string> GetDiscrepency(string Database_Id, string ServerName, string User_ID, string Password)
        {
            string connectString, SQL;
            Dictionary<string, string> lstDiscrepency = new Dictionary<string, string>();
            Dictionary<string, string> lstDiscrepencyinDatabase = new Dictionary<string, string>();

            //Add sql queries to check discrepency

            // Set connection string with selected server and integrated security  

            connectString = "Data Source=" + ServerName + ";Integrated Security=false;Initial Catalog=master;User ID=" +
                User_ID + ";Password=" + Password + ";";

            using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(connectString))
            {
                // Open connection
                con.Open();

                //Get QueryList names in server in a datareader 
                SQL = "SELECT Comment,Query FROM CoreUtility.dbo.QueryList WHERE Software='CORE' AND IsActive=1;";

                System.Data.SqlClient.SqlCommand com =
                             new System.Data.SqlClient.SqlCommand(SQL, con);
                System.Data.SqlClient.SqlDataReader dr = com.ExecuteReader();

                while (dr.Read())
                {
                    lstDiscrepency.Add(dr["Comment"].ToString(), dr["Query"].ToString());
                }


                SQL = "";

                foreach (var item in lstDiscrepency)
                {
                    SQL = item.Value;
                    int count = executeSQLSelect(SQL, ServerName, User_ID, Password, Database_Id);
                    if (count == 1)
                    {
                        string comment = item.Key.ToString();
                        lstDiscrepencyinDatabase.Add(comment, SQL);
                    }
                }

                return (lstDiscrepencyinDatabase);

            }
        }



        //Get discrepency in Core database SIMPLE
        public static Dictionary<string,string> GetDiscrepency2(string Database_Id, string ServerName, string User_ID, string Password)
        {
            Dictionary<string, string> lstDiscrepency = new Dictionary<string, string>();
            Dictionary<string, string> lstDiscrepencyinDatabase = new Dictionary<string, string>();

            //Add sql queries to check discrepency
            lstDiscrepency.Add("--Penny issue in invoices and Paymnets", "SELECT inv.InvoiceDetail_ID,inv.amt,pay.Payamt FROM (SELECT InvoiceDetail_ID, SUM(Amount) AS amt FROM  dbo.InvoiceDetail GROUP BY InvoiceDetail_ID) inv JOIN (SELECT InvoiceDetail_ID, SUM(AmountPaid) AS Payamt FROM  dbo.PaymentDetail GROUP BY InvoiceDetail_ID) pay ON pay.InvoiceDetail_ID = inv.InvoiceDetail_ID WHERE inv.amt <> pay.Payamt AND(ABS(inv.amt - pay.Payamt) BETWEEN 0 AND 1); ");

            lstDiscrepency.Add("--Payment more than invoice", "SELECT inv.InvoiceDetail_ID,inv.amt,pay.Payamt FROM (SELECT InvoiceDetail_ID, SUM(Amount) AS amt FROM  dbo.InvoiceDetail GROUP BY InvoiceDetail_ID) inv JOIN (SELECT InvoiceDetail_ID, SUM(AmountPaid) AS Payamt FROM  dbo.PaymentDetail GROUP BY InvoiceDetail_ID) pay ON pay.InvoiceDetail_ID = inv.InvoiceDetail_ID WHERE inv.amt <> pay.Payamt AND pay.Payamt > inv.amt; ");

            lstDiscrepency.Add("--Different amount in Invoice and InvoiceDetail for same invoice", "SELECT i.Invoice_ID,i.InvoiceAmount,inv.amt FROM (SELECT Invoice_ID, SUM(Amount) AS amt FROM dbo.InvoiceDetail GROUP BY Invoice_ID)inv JOIN  dbo.Invoice i ON i.Invoice_ID = inv.Invoice_ID  WHERE i.InvoiceAmount <> inv.amt; ");

            lstDiscrepency.Add("--Different amount in Payment and PaymentDetail", "  SELECT p.Payment_ID,p.PayAmt,pay.paymentamount FROM (SELECT Payment_ID, SUM(AmountPaid) AS paymentamount FROM  dbo.PaymentDetail GROUP BY Payment_ID) pay JOIN dbo.Payment p ON p.Payment_ID = pay.Payment_ID WHERE p.PayAmt <> pay.paymentamount; ");

            lstDiscrepency.Add("--Duplicate workflow entry for same Time/Expense Entry", "SELECT Entity_ID,COUNT(*) FROM dbo.Workflow GROUP BY Entity_ID HAVING COUNT(*)>1; ");

            lstDiscrepency.Add("--Vendor bill payments present in BillPayment/BillPaymentdetails table but not present in Accounting table", "SELECT * FROM dbo.BillPayment WHERE BillPayment_ID NOT IN (SELECT  MasterTransaction_Id FROM  dbo.Accounting WHERE MasterTransaction_Id IS NOT NULL);");

            lstDiscrepency.Add("--Different client in InvoiceDetails table and Project table/Wrong client in Invoice", "SELECT id.Client_ID AS InvoiceClient, p.Client_ID AS ProjClient,id.Invoice_ID FROM dbo.InvoiceDetail id JOIN dbo.Project p ON p.Project_ID = id.Project_ID  WHERE id.Client_ID <> p.Client_ID; ");

            lstDiscrepency.Add("--Sum  of Debit/Credits not equal", "SELECT debt.DebtAmount,credit.CreditAmount,credit.Accounting_ID FROM (SELECT SUM(ISNULL(Amount, 0)) AS DebtAmount, Accounting_ID FROM dbo.AccountingDetail WHERE IsDebit = 1 GROUP BY Accounting_ID)debt INNER JOIN (SELECT SUM(ISNULL(Amount, 0)) AS CreditAmount, Accounting_ID FROM dbo.AccountingDetail WHERE IsDebit = 0 GROUP BY Accounting_ID)credit ON credit.Accounting_ID = debt.Accounting_ID WHERE debt.DebtAmount <> credit.CreditAmount; ");

            lstDiscrepency.Add("--Orphan default group in Projects", "SELECT DefaultGroup_ID,* FROM dbo.Project WHERE DefaultGroup_ID NOT IN (SELECT Group_ID FROM dbo.Groups );");

            lstDiscrepency.Add("--Orphan vendor bills/vendor bill details present in accounting tables but deleted from vendor bill tables", "SELECT * FROM dbo.Accounting WHERE TransactionType=450 AND Transaction_Id NOT  IN(SELECT VendorBill_ID FROM  dbo.VendorBill );");

            //lstDiscrepency.Add("--Orphan Bill Payments/Bill payment present but the vendor bill has been deleted", "SELECT * FROM dbo.BillPaymentDetail WHERE  EntityTransaction_ID not  IN(SELECT VendorBill_ID FROM  dbo.VendorBill);");

            lstDiscrepency.Add("--Orphan VB's in Time Entry", "SELECT * FROM dbo.TimeEntry WHERE VendorBill_ID IS NOT NULL AND VendorBill_ID  NOT IN (SELECT VendorBill_ID FROM dbo.VendorBill);");

            lstDiscrepency.Add("--Orphan VB's in Expense Entry", "SELECT * FROM dbo.ExpenseLog WHERE VendorBill_ID IS NOT NULL AND VendorBill_ID  NOT IN (SELECT VendorBill_ID FROM dbo.VendorBill);");

            lstDiscrepency.Add("--Orphan Invoices in Time Entry", "SELECT * FROM dbo.TimeEntry WHERE Invoice_ID IS NOT NULL AND Invoice_ID  NOT IN (SELECT Invoice_ID FROM dbo.Invoice);");

            lstDiscrepency.Add("--Orphan Invoices in Expense Entry", "SELECT * FROM dbo.ExpenseLog WHERE Invoice_ID IS NOT NULL AND Invoice_ID  NOT IN (SELECT Invoice_ID FROM dbo.Invoice);");

            lstDiscrepency.Add("--Orphan Checks in CheckExpenseLineItem", "SELECT * FROM dbo.CheckExpenseLineItem WHERE Check_ID NOT IN (SELECT Check_ID FROM dbo.Checks);");

            lstDiscrepency.Add("--Orphan check in Checks Payment deleted in BillPaymnet", "SELECT * FROM dbo.Checks WHERE IsBillPayment =1 AND Check_ID NOT IN (SELECT Entity_ID FROM dbo.BillPayment WHERE Entity_ID IS NOT NULL);");

            lstDiscrepency.Add("--Orphan VB in VendorBillExpenseItem", "SELECT * FROM dbo.VendorBillExpenseItem WHERE VendorBill_ID NOT IN (SELECT VendorBill_ID FROM dbo.VendorBill);");

            lstDiscrepency.Add("--Orphan bill paymnet in BillPaymentDetail ", "SELECT * FROM dbo.BillPaymentDetail WHERE BillPayment_ID NOT IN (SELECT BillPayment_ID FROM dbo.BillPayment);");

            lstDiscrepency.Add("--Orphan Invoices in InvoiceDetail  ", "SELECT * FROM dbo.InvoiceDetail WHERE Invoice_ID NOT IN (SELECT Invoice_ID FROM dbo.Invoice);");

            lstDiscrepency.Add("--Orphan Credit card payment in CreditCardExpenseItem  ", "SELECT * FROM dbo.CreditCardExpenseItem WHERE CreditCardPayment_ID NOT IN (SELECT CreditCardPayment_ID FROM dbo.CreditCardPayment);");

            lstDiscrepency.Add("--Orphan Invoice Payments in PaymentDetail  ", "SELECT * FROM dbo.PaymentDetail WHERE Payment_ID NOT IN (SELECT Payment_ID FROM dbo.Payment);");

            lstDiscrepency.Add("-- Different amount in Invoice and InvoiceDetails for same invoice  ", "SELECT * FROM dbo.Invoice i JOIN(SELECT Invoice_ID, SUM(Amount) AS amt FROM dbo.InvoiceDetail GROUP BY Invoice_ID)id ON id.Invoice_ID = i.Invoice_ID WHERE i.InvoiceAmount <> id.amt; ");

            lstDiscrepency.Add("-- Different amount in Payment and PaymentDetails for same Payment  ", " SELECT * FROM dbo.Payment i JOIN (SELECT Payment_ID, SUM(AmountPaid) AS amt FROM dbo.PaymentDetail GROUP BY Payment_ID)id ON id.Payment_ID = i.Payment_ID WHERE i.PayAmt <> id.amt;");

            lstDiscrepency.Add(" ----Approved Reimbursable Expenses with NULL Vendor Bill ID's ", "SELECT el.VendorBill_ID,el.Reimbursable,el.ExpenseLog_ID,* FROM dbo.ExpenseLog el JOIN dbo.Workflow wf ON el.ExpenseLog_ID=wf.Entity_ID WHERE el.Reimbursable = 1 AND el.VendorBill_ID IS NULL AND wf.WorkflowAction = 2 ; ");


            lstDiscrepency.Add("-- --Retainer applied more than retainer taken  ", " SELECT p1.Client_ID, p1.RetainerPaymentTaken,p2.RetainerAmtApplied FROM  (SELECT Client_ID, SUM(PayAmt) AS RetainerPaymentTaken FROM dbo.Payment WHERE PayRetainer = 1 GROUP BY Client_ID) p1 JOIN (SELECT Client_ID, SUM(PayAmt) AS RetainerAmtApplied FROM dbo.Payment WHERE PayMethod = -1 GROUP BY Client_ID) p2 ON p2.Client_ID = p1.Client_ID WHERE p2.RetainerAmtApplied > p1.RetainerPaymentTaken; ");


            lstDiscrepency.Add("-- CRA+PRA+PPRA <> AMOUNT PAID  ", "SELECT * FROM dbo.PaymentDetail WHERE Payment_ID IN (SELECT Payment_ID FROM dbo.Payment WHERE PayMethod=-1) AND AmountPaid<> CRA+PRA+PPRA; ");

            lstDiscrepency.Add("----Child project set as Root Project ", " SELECT * FROM dbo.Project WHERE Project_ID IN (SELECT RootProject_ID FROM dbo.InvoiceDetail WHERE RootProject_ID IS NOT NULL) AND ProjectLevel <>0; ");

            lstDiscrepency.Add(" ---Root project as NULL in Invoice Details for retainer applied ", " SELECT id.RootProject_ID,p.ParentProject_ID,id.Project_ID,* FROM dbo.InvoiceDetail id JOIN dbo.Project p ON p.Project_ID = id.Project_ID WHERE p.ParentProject_ID IS NOT NULL AND id.RootProject_ID IS NULL AND id.InvoiceDetail_ID IN (SELECT InvoiceDetail_ID FROM dbo.PaymentDetail WHERE(PPRA <> 0 OR PRA <> 0) AND InvoiceDetail_ID IS NOT NULL) ; ");

            lstDiscrepency.Add("------- Different amount for Invoices in Invoice table and Accounting table ", "SELECT a.TransactionAmount,i.InvoiceAmount, (a.TransactionAmount-i.InvoiceAmount) AS diff,i.Invoice_ID FROM dbo.Accounting a JOIN dbo.Invoice i ON i.Invoice_ID=a.MasterTransaction_Id WHERE a.TransactionType IN ('250','12','22','200') AND  a.TransactionAmount<>i.InvoiceAmount; ");


            foreach (var item in lstDiscrepency)
            {
                string SQL = item.Value;
                int count = executeSQLSelect(SQL, ServerName, User_ID, Password,Database_Id);
                if(count==1)
                {
                    string comment = item.Key.ToString();
                    lstDiscrepencyinDatabase.Add(comment, SQL);
                }
            }

            return (lstDiscrepencyinDatabase);
        }


     

    }
}
