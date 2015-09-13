// Programmer: Matthew White
// File: LogicTier.cs
// Date: 05/06/2015
// The purpose of this class is to act as the mid tier of the EIAP program. This class communicates
// with the DataTier and the Presentation Tier.  All actions that touch the database will go through this class.
// This class also includes a method called createThread() that accepts a method as a parameter.  This class passes methods to the 
// createThread() which creates threads that run the method that was passed to it.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Data;

namespace EIAP
{
    class LogicTier
    {
        // Constructor
        public LogicTier()
        {
            instantiateDataMembers();
        }

        // ------------------------ PROPERTIES -----------------------------------------
        
        internal string currentIP { get; set; }
        internal string lastRefresh { get; set; }
        internal string currentAlias { get; set; }

        static private Exception _ThreadException { get; set; }
        private WebsiteCommunications externalIP { get; set; }
        private FilterIPAddress filterIP { get; set; }
        private EmailCommunications email { get; set; }
        private DatabaseCommunication databaseCommunications { get; set; }
        private string lastIP { get; set; }
        private List<SourceChoice> listSourceChoice { get; set; }
        

                                    //---------------------  INTERNAL  METHODS  -------------------------------------
        // Send a test email , for use in the Presentation Tier
        internal void testSendEmail()
        {
            // Update SMTP settings
            updateSMTPFromDatabase();

            // Update the Message
            updateTestEmailMessage();
            try
            {
                // Send email in different thread
                createThread(sendEmailMessage);
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                throw ex;
            }

        }

        // Returns the Auto Start value from dataset , for use in Presentation Tier
        internal bool getAutoStartDataSet()
        {
            // Refresh dataset
            databaseCommunications.fillTableAdapters();

            return databaseCommunications.getTable("TB_LOAD_OPTIONS").Rows[0].Field<bool>("AUTO_START");
        }

        // Returns the Minimize value from dataset, for use in the Presentation Tier
        internal bool getMinimizeDataSet()
        {
            // Refresh dataset
            databaseCommunications.fillTableAdapters();

            return databaseCommunications.getTable("TB_LOAD_OPTIONS").Rows[0].Field<bool>("MINIMIZE");
        }

        // Returns the Update Interval time from the dataset, for use in the Presentation Tier
        internal string getUpdateIntervalDataSet()
        {
            // Refresh dataset
            databaseCommunications.fillTableAdapters();

            return databaseCommunications.getTable("TB_LOAD_OPTIONS").Rows[0].Field<string>("UPDATE_INTERVAL");
        }

        // Return the table from the dataset by name.
        internal DataTable getTable(string tableName)
        {
            return databaseCommunications.getTable(tableName);
        }

        // Add Source row to the existing Data table
        // Accepts info for new row and existing data table in order to update and show changes on form
        internal void addSourceInfoToDataTable(DataTable aDataTable, string priority, string alias, string http, bool requiresUsername,
            string username, string password)
        {

            // Data validation
            if (priority.Trim() == "" || alias.Trim() == "" || http.Trim() == "")
            {
                throw new Exception("Please ensure the Priority , Alias, and Http text boxes have a value");
            }
            if (requiresUsername)
            {
                if (username.Trim() == "" || password.Trim() == "")
                {
                    throw new Exception("Please enter a username and password if authentication is required or uncheck authentication required.");
                }
            }

            // create a copy of the structure of the current DataTable
            DataRow tempDataRow = aDataTable.NewRow();
            // Assign values
            tempDataRow["PRIORITY"] = priority.Trim();
            tempDataRow["ALIAS"] = alias.Trim();
            tempDataRow["HTTP_ADDRESS"] = http.Trim();
            tempDataRow["REQUIRES_AUTHENTICATION"] = requiresUsername;
            tempDataRow["USERNAME"] = username.Trim();
            tempDataRow["PASSWORD"] = password.Trim();

            // add new row
            aDataTable.Rows.Add(tempDataRow);
            // Sort by Priority
            aDataTable.DefaultView.Sort = "PRIORITY";
            // commit the changes
            aDataTable.AcceptChanges();

            // Make changes to database
            addSourceInfoToDatabase(aDataTable);
        }

        // Adds DataTable to TB_SOURCE Database and refreshes DataSet
        // The imported Data table will essentially overwrite 
        // The table in the Database.
        internal void addSourceInfoToDatabase(DataTable aDatatable)
        {
            // Accept changes to DataTable 
            aDatatable.AcceptChanges();

            // Remove all Rows from TB_Source in Database
            databaseCommunications.deleteAllSourceTable();

            // Insert each row from parameter to TB_SOURCE
            foreach (DataRow dr in aDatatable.Rows)
            {
                databaseCommunications.insertRowSource(
                dr["PRIORITY"].ToString(),
                dr["ALIAS"].ToString(),
                dr["HTTP_ADDRESS"].ToString(),
                Convert.ToBoolean(dr["REQUIRES_AUTHENTICATION"]),
                dr["USERNAME"].ToString(),
                dr["PASSWORD"].ToString());
            }

            // Refresh Data Set after updating the Database
            databaseCommunications.fillTableAdapters();
        }

        // Add SMTP settings to Database
        internal void addRowSMTP(string username, string host, string password, string port, bool tls)
        {
            
            // Validate that port string has a number value, if not notify user.
            int number;
            bool result = Int32.TryParse(port.Trim(), out number);
            if (!result)
            {
                throw new Exception("Please enter a number value for the port text box.");
            }

            // validate that strings have values to ensure they have data before saving into database, if not notify user
            if (username.Trim() == "" || host.Trim() == "" || password.Trim() == "" || port.Trim() == "" )
            {
                throw new Exception("Ensure all text boxes have a value.");
            }

           // Add settings to database by first deleting all data, inserting and then filling the data set.
            databaseCommunications.deleteAllSMTPTable();
            databaseCommunications.insertRowSMTP(username.Trim(), password.Trim(), Convert.ToInt32(port.Trim()), tls, host.Trim());
            databaseCommunications.fillTableAdapters();
        }

        // Add Email Message/Notification settings to Database
        internal void addEmailNotification(string to, string from, string subject, string body)
        {
            // Validate if all fields have data in them, if not notify the user and don't accept changes
            // Also ensures user has the @ symbol in their Email address.

           
            if ( (to.Trim() == "" || to.Contains("@") == false ) || (from.Trim() == "" || from.Contains("@") == false ) || subject.Trim() == "" || body.Trim() == "")
            {
                throw new Exception("Please ensure all text boxes have a value and email addresses are correct, please try again.");
            }
            
            databaseCommunications.deleteAllEmailNotificationTable();
            databaseCommunications.insertRowNotificationEmail(to.Trim(), from.Trim(), subject.Trim(), body.Trim());
            databaseCommunications.fillTableAdapters();
        }

        // Add load/start up settings to Database
        internal void addLoadOptions(bool minimize, bool autoStart, string updateInterval)
        {

            // validates and ensures the update Interval string is a number greater than 0
            // if not then will notify user and not save
            int number;
            bool result = Int32.TryParse(updateInterval.Trim(), out number);
            if (number <= 0)
            {
                throw new Exception("Value for update Interval must be greater then 0.");
            }

                databaseCommunications.deleteAllLoadOptionsTable();
                databaseCommunications.insertRowLoadOptions(minimize, autoStart, updateInterval.Trim());
                databaseCommunications.fillTableAdapters();                        
        }


        // Main function-- controls the logic for checking ip address and sending email.
        internal void startForUI(ref RichTextBox aTextbox)
        {
            // Retrieve Source data from Database and fill/create a list
            getAllSourceChoices();

            // Assign a value to temporary variables
            string info = "";
            string temp = "";


            // 1st check to see if there are any source information is loaded

             // Check to see if user has entered source websites to check external ip.
             // Display that there are no options in database , so user can enter a HTTP address
                if (listSourceChoice.Count == 0)
                {
                    aTextbox.AppendText("Please enter a HTTP address into the Source option!." + Environment.NewLine);
                    currentAlias = "Enter a website!";
                    aTextbox.ScrollToCaret();

                    // set properties
                    currentIP = "No websites entered!";
                    currentAlias = "No alias entered.";
                    lastRefresh = DateTime.Now.ToString();
                }


           // 2nd Iterate through sources and get an external IP and display on UI.
                // Iterate through list (sources the user has entered).
                for (int x = 0; x <= (listSourceChoice.Count - 1); x++)
                {



           // 3rd Check to see if the user defined source requires authentication
                    // Then get IP address, update variables and then display on ui
                    // If the Source requires authentication then..
                    if (listSourceChoice[x].requiresAuthentication)
                    {
                        // Pass and get External IP with authentication information
                        temp = getExternalIPWithAuthentication(listSourceChoice[x].httpAddress, listSourceChoice[x].userName, listSourceChoice[x].password);

                        // Checks result of the web client class and displays the proper message (IP or 404 error)
                        info = checkIPResult(temp, listSourceChoice[x].alias) + Environment.NewLine;

                        // update variables with external ip information and alias in order to be displayed on UI
                        currentIP = temp;
                        currentAlias = listSourceChoice[x].alias;

                        // update text box on ui
                        aTextbox.AppendText(info);
                        aTextbox.ScrollToCaret();

                        // set properties

                        lastRefresh = DateTime.Now.ToString();



                        // If an IP was successfully retrieved then exit this For loop
                        if (temp.Length > 0)
                        {

                            // Check if current IP is different from last IP.
                            checkCurrentIPvsLastIP(ref aTextbox);

                            // Exit function since IP address was found
                            return;
                        }
                    }

          // 4th If the source website does not require authentication 
                    // Then get IP address, update variables and then display on ui
                    else  // If the website from source does not require authentication
                    {
                        temp = getExternalIP(listSourceChoice[x].httpAddress);
                        info = checkIPResult(temp, listSourceChoice[x].alias);

                        aTextbox.AppendText(info);
                        aTextbox.ScrollToCaret();

                        // set properties
                        currentIP = temp;
                        currentAlias = listSourceChoice[x].alias;
                        lastRefresh = DateTime.Now.ToString();

                        try
                        {
                            // if an IP was successfully retrieved then exit this For loop
                            if (temp.Length > 0)
                            {
                                // Check if current IP is different from last IP.
                                checkCurrentIPvsLastIP(ref aTextbox);

                                if (_ThreadException != null)
                                {
                                    // copy to temp variable
                                    Exception ex = _ThreadException;
                                    // Assign a null value
                                    _ThreadException = null;
                                    // Throw to calling method
                                    throw ex;
                                }
                                return;
                            }
                        }
                        catch (System.Net.Mail.SmtpException ex)
                        {
                            throw ex;
                        }

                    }

                    // ** Used when all sources have failed to retrieve a IP 
                    // If there are no more websites/sources to use display.
                    if (x == listSourceChoice.Count - 1)
                    {
                        aTextbox.AppendText("All websites have failed, please enter another source");
                        aTextbox.ScrollToCaret();
                        // set properties
                        currentIP = "Error!";
                        currentAlias = "Error connecting, please enter another aliases";
                        lastRefresh = DateTime.Now.ToString();
                    }
                }
        }

        // Instantiate and fill in values of variables/properties in class
        private void instantiateDataMembers()
        {
            externalIP = new WebsiteCommunications();
            email = new EmailCommunications();
            filterIP = new FilterIPAddress();
            databaseCommunications = new DatabaseCommunication();
            _ThreadException = null;

            // Load Last IP
            lastIP = getLastIPDataSet();
        }

        // Accepts a HTTP address, returns an IP address
        // Get External IP 
        private string getExternalIP(string httpAddress)
        {                    
            // Set HttpAddress in WebsiteCommunications Class
            externalIP.httpAddress = httpAddress.Trim();

            // Create a new thread and run loadWebsite
            createThread(externalIP.loadWebsite);

            // filter and return External IP address
            return filterIP.filterExternalIPAddress(externalIP.websiteText);
        }

        // Accepts a HTTP address, returns an IP address (With Authentication)
        // Get External IP with Authentication
        private string getExternalIPWithAuthentication(string httpAddress, string username, string password)
        {
         
                // Set HttpAddress in WebsiteCommunications Class
                externalIP.httpAddress = httpAddress.Trim();

                // Set Username in WebsiteCommunications Class
                externalIP.userName = username.Trim();

                // Set Password in WebsiteCommunications Class
                externalIP.passWord = password.Trim();

                // Create a new thread and run loadWebsite
                createThread(externalIP.loadWebsiteWithAuthentication);
       
            // filter and return External IP address
            return filterIP.filterExternalIPAddress(externalIP.websiteText);
        }

        // Check the Results of web client class and return string for Presentation Tier
        private string checkIPResult(string temp, string alias)
        {
            if (temp == "404")
            {
                return DateTime.Now + " using Alias: " + alias + Environment.NewLine +
                "IP Address was not found due to wrong http address... trying other sources" + Environment.NewLine;
            }
            
            if (temp.Length > 0)
            {
                return DateTime.Now + " using Alias: " + alias + Environment.NewLine +
                "IP Address: " + temp + Environment.NewLine;
            }
            else
            {
                return DateTime.Now + " using Alias: " + alias + Environment.NewLine +
                "IP Address was not found... trying other sources" + Environment.NewLine;
            }
        }

        // Update the Email Message values from Database to Email Communications Class
        private void updateMailMessageFromDatabase()
        {
            DataTable aTempDataTable = getTable("TB_NOTIFICATION_EMAIL_SETTINGS").Copy();

            foreach (DataRow dr in aTempDataTable.Rows)
            {
                updateMailMessage(dr["FROM"].ToString(), dr["TO"].ToString(),
                    dr["SUBJECT"].ToString() + " Current IP: " + currentIP,
                    dr["BODY"].ToString() + " Current IP: " + currentIP);
            }
        }

        // Retrieves values from Database and updates Email communications class properties
        private void updateSMTPFromDatabase()
        {
            // copies values for dataset
            DataTable aTempDataTable = getTable("TB_SMTP_SETTINGS").Copy();

            //Iterates through each row, (in this case only one) and update the EmailCommunications Class
            foreach (DataRow dr in aTempDataTable.Rows)
            {
                updateSMTPSettings(dr["HOST"].ToString(), dr["PORT"].ToString(),
                    dr["USERNAME"].ToString(), dr["PASSWORD"].ToString(), Convert.ToBoolean(dr["TLS"]));
            }
        }

        // Update SMTP Settings in the EmailCommunications Class
        private void updateSMTPSettings(string host, string port, string username, string password, bool tls)
        { 
            email.updateSMTPSettings(host.Trim(), port.Trim(), username.Trim(), password.Trim(), tls);
        }
        // Update the Notification Mail Message in the EmailCommunications Class
        private void updateMailMessage(string from, string to, string subject, string body)
        {
            email.updateMailMessage(from.Trim(), to.Trim(), subject.Trim(), body.Trim());
        }

        // Send Email Notification in different thread
        private void sendEmail()
        {
            
            
            // Update SMTP settings
            updateSMTPFromDatabase();

            // Update Main Message
            updateMailMessageFromDatabase();

            try
            {
                // Send email in different thread
                createThread(sendEmailMessage);
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                // Send back to calling method
                throw ex;
            }

        }

        // Update the Email message for the test in the Email Communications Class
        private void updateTestEmailMessage()
        {
            DataTable aTempDataTable = getTable("TB_NOTIFICATION_EMAIL_SETTINGS").Copy();
            string subject = "Test Email Message from EIAP" + " Current IP: " + currentIP;
           
            foreach (DataRow dr in aTempDataTable.Rows)
            {
                updateMailMessage(dr["FROM"].ToString(), dr["TO"].ToString(),
                    subject, subject);
            }
        }

        

        // Sends the email message after checking to see if the Message is Null
        private void sendEmailMessage()
        {
           if (email.isMessageNull())
           {
              _ThreadException = new Exception("Empty Email, please update settings");     
           }
           else
           {
               try
               {
                   email.sendEMail();
               }
               catch (System.Net.Mail.SmtpException ex)
               {
                   _ThreadException = ex;
               }
               catch (Exception general)
               {
                   _ThreadException = general;
               }
              
           }
        }

        // return the last IP that is in the Data Set
        private string getLastIPDataSet()
        {
            // Refresh dataset
            databaseCommunications.fillTableAdapters();

            return databaseCommunications.getTable("TB_APPLICATION_HISTORY").Rows[0].Field<string>("Last_IP");
        }
       
        // Add Last IP info to the TB_APPLICATION_HISTORY
        private void addApplicatoinHistoryRow(string sourceAlias, string lastIP, DateTime lastIPDateTime)
        {
                // Remove all data in Table
                databaseCommunications.deleteAllApplicationHistoryTable();
                // Add Row 
                databaseCommunications.insertRowApplicationHistory(sourceAlias.Trim(), lastIP.Trim(), lastIPDateTime);
                // Refresh DataSet
                databaseCommunications.fillTableAdapters();
        }
    
        // Sends email if current IP is different then last IP
        // update variables in this class
        private void checkCurrentIPvsLastIP(ref RichTextBox aTextbox)
        {
            if (currentIP.Trim() != lastIP.Trim())
            {
                aTextbox.AppendText("IP address has changed, sending Email" + Environment.NewLine);
                aTextbox.ScrollToCaret();
                
                // Send Email that IP has changed
                sendEmail();
            
                // Save Current IP to last IP (database and currentIP)
                addApplicatoinHistoryRow(currentAlias, currentIP, DateTime.Now);

                // Update Variables
                lastIP = getLastIPDataSet();
            }
        }

        // Accepts a method that does not return a value
        // Creates a thread to run a function/method
        private void createThread(Action method)
        {
            // Create the thread object, passing in a method
            Thread aThread = new Thread(new ThreadStart(method));
            
            try
            {

                // Add a name for the thread
                aThread.Name = method.GetType().FullName.ToString();

                // Start the thread
                aThread.Start();

                // Wait for thread to start
                while (!aThread.IsAlive)
                {
                    // Do nothing, but wait
                }

                // Wait for thread to finish 
                aThread.Join();

            }
            catch (System.Net.Mail.SmtpException ex)
            {
                // Wait for thread to finish 
                aThread.Join();
                MessageBox.Show(ex.Message);
                
            }
            catch (Exception ex)
            {
                // Wait for thread to finish 
                aThread.Join();
                throw ex;
            }

        }

        // Loads all Source information from Dataset to a list
        private void getAllSourceChoices()
        {
            // Instantiate List if null
            if (listSourceChoice == null)
            {
                listSourceChoice = new List<SourceChoice>();
            }

            // Clear the list if it has items in it
            if (listSourceChoice.Count > 0)
            {
                listSourceChoice.Clear();
            }

            // Copy data from Dataset
            DataTable tempDataTable = databaseCommunications.getDataSet().TB_SOURCE.Copy();

            // For each row in dataset add values to a temporary SourceChoice type.
            foreach (DataRow dr in tempDataTable.Rows)
            {
                SourceChoice temp = new SourceChoice();

                temp.priority = Convert.ToInt32(dr.Field<string>("PRIORITY"));
                temp.alias = dr.Field<string>("ALIAS");
                temp.httpAddress = dr.Field<string>("HTTP_ADDRESS");
                temp.requiresAuthentication = dr.Field<bool>("REQUIRES_AUTHENTICATION");
                temp.userName = dr.Field<string>("USERNAME");
                temp.password = dr.Field<string>("PASSWORD");


                // Add temp SourceChoice to list
                listSourceChoice.Add(temp);
            }
        }

        // Structure -- used to hold source information
        private struct SourceChoice
        {
            public int priority { get; set; }
            public string alias { get; set; }
            public string httpAddress { get; set; }
            public bool requiresAuthentication { get; set; }
            public string userName { get; set; }
            public string password { get; set; }
        }
    }
}

