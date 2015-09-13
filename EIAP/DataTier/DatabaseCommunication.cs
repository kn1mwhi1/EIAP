// File: DatabaseCommunication.cs
// Programmer: Matthew White
// Date: 5/4/2015
// This class is responsible for updating, retrieve, and deleting value in the database tables
// DataTier

using System;
using System.Data;
using System.Data.SqlClient;

namespace EIAP
{
    class DatabaseCommunication
    {
        //----------------------------------------------  Properties ----------------------------------------------------
        // DataSet Property
        private EIAPDBDataSet A_EIAPDBDataSet { get; set; }

        // Table Adapter Properties
        private EIAPDBDataSetTableAdapters.TB_SMTP_SETTINGSTableAdapter SMTPTableAdapter { get; set; }
        private EIAPDBDataSetTableAdapters.TB_APPLICATION_HISTORYTableAdapter ApplicationHistoryTableAdapter { get; set; }
        private EIAPDBDataSetTableAdapters.TB_LOAD_OPTIONSTableAdapter LoadOptionsTableAdapter { get; set; }
        private EIAPDBDataSetTableAdapters.TB_NOTIFICATION_EMAIL_SETTINGSTableAdapter NotificationEmailTableAdapter { get; set; }
        private EIAPDBDataSetTableAdapters.TB_SOURCETableAdapter SourceTableAdapter { get; set; }

        // ----------------------------------------- End Properties ------------------------------------------------------


        // Constructor
        public DatabaseCommunication()
        {
            getConnectionString();
            instantiateVariables();
            fillTableAdapters();
        }

        // Instantiate all Variables needed for class
        private void instantiateVariables()
        {
                // Instantiate DataSet
                A_EIAPDBDataSet = new EIAPDBDataSet();

                // Instantiate Table Adapters
                SMTPTableAdapter = new EIAPDBDataSetTableAdapters.TB_SMTP_SETTINGSTableAdapter();
                ApplicationHistoryTableAdapter = new EIAPDBDataSetTableAdapters.TB_APPLICATION_HISTORYTableAdapter();
                LoadOptionsTableAdapter = new EIAPDBDataSetTableAdapters.TB_LOAD_OPTIONSTableAdapter();
                NotificationEmailTableAdapter = new EIAPDBDataSetTableAdapters.TB_NOTIFICATION_EMAIL_SETTINGSTableAdapter();
                SourceTableAdapter = new EIAPDBDataSetTableAdapters.TB_SOURCETableAdapter();
        }

        // Retrieve the Executing directory and update the connection string in order for the program to access database
        private void getConnectionString()
        {
            // Detect current executing directory
            string executeDirectory = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string path = (System.IO.Path.GetDirectoryName(executeDirectory));

            // Create a connection string
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(Properties.Settings.Default.Database1ConnectionString);
            builder.DataSource = builder.DataSource.Replace("|DataDirectory|", path);
            string desiredConnectionString = builder.ConnectionString;

            // Change Connection String of program to the executing directory
            Properties.Settings.Default["Database1ConnectionString"] = desiredConnectionString;
        }


        // Fill/Refresh Data Set from Database 
        public void fillTableAdapters()
        {
            SMTPTableAdapter.Fill(A_EIAPDBDataSet.TB_SMTP_SETTINGS);
            ApplicationHistoryTableAdapter.Fill(A_EIAPDBDataSet.TB_APPLICATION_HISTORY);
            LoadOptionsTableAdapter.Fill(A_EIAPDBDataSet.TB_LOAD_OPTIONS);
            NotificationEmailTableAdapter.Fill(A_EIAPDBDataSet.TB_NOTIFICATION_EMAIL_SETTINGS);
            SourceTableAdapter.Fill(A_EIAPDBDataSet.TB_SOURCE);
        }


        // Remove all data from TB_SMTP_SETTINGS <-- Database
        public void deleteAllSMTPTable()
        { 
                SMTPTableAdapter.DeleteQuery("%", "%");
        }

        // Insert a new row of data to TB_SMTP_SETTINGS <-- Database
        public void insertRowSMTP(string username, string password, int port, bool tls, string host)
        {
                SMTPTableAdapter.InsertQuery(username, password, port, tls, host);     
        }

        // Remove all data from TB_NOTIFICATION_EMAIL_SETTINGS <-- Database
        public void deleteAllEmailNotificationTable()
        {
                NotificationEmailTableAdapter.DeleteQuery("%", "%");
        }

        // Insert a new row of data to TB_NOTIFICATION_EMAIL_SETTINGS <-- Database
        public void insertRowNotificationEmail(string to, string from, string subject, string body)
        {
                NotificationEmailTableAdapter.InsertQuery(to, from, subject, body);
        }

        // Delete all data from TB_LOAD_OPTIONS <-- Database
        public void deleteAllLoadOptionsTable()
        {
                LoadOptionsTableAdapter.DeleteQuery("%");
        }

        // Insert a new row of data to TB_LOAD_OPTIONS <-- Database
        public void insertRowLoadOptions(bool minimize, bool autoStart, string updateInterval)
        {

                LoadOptionsTableAdapter.InsertQuery(minimize, autoStart, updateInterval);
        }

        // Delete all data from TB_SOURCE <-- Database
        public void deleteAllSourceTable()
        {
                SourceTableAdapter.DeleteQuery("%");
        }

        // insert a row into TB_SOURCE Table <-- Database
        public void insertRowSource(string priority, string alias, string http, bool reqAuthentication, string username, string password)
        {
                SourceTableAdapter.InsertQuery(priority, alias, http, reqAuthentication, username, password);
        }

        // Delete all data from TB_SOURCE <-- Database
        public void deleteAllApplicationHistoryTable()
        {
                ApplicationHistoryTableAdapter.DeleteQuery("%");
        }

        // insert a row into TB_SOURCE Table <-- Database
        public void insertRowApplicationHistory(string sourceAlias, string lastIP, DateTime lastIPDateTime)
        {
                ApplicationHistoryTableAdapter.InsertQuery(sourceAlias, lastIP, lastIPDateTime);
        }


        // Remove and Merge table into Dataset
        public void saveTableDataSet(string tableName, DataTable aTable)
        {
            // Clear old Table
            A_EIAPDBDataSet.Tables[tableName].Rows.Clear();

            // Add Table
            A_EIAPDBDataSet.Tables[tableName].Merge(aTable, true, MissingSchemaAction.Add);
        }

        // Retrieve a copy of data table from Dataset
        public DataTable getTable(string aTable)
        {
            DataTable dt = new DataTable();
            dt = A_EIAPDBDataSet.Tables[aTable].Copy();
            return dt;
        }

        // Retrieve DataSet
        public EIAPDBDataSet getDataSet()
        {
            return A_EIAPDBDataSet;
        }
    }
}
