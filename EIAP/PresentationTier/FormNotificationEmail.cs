// Programmer: Matthew White
// File: FormNotificationEmail.cs
// Date: 5/5/2015
// Purpose: This class is a windows form that represents the Notification Email options.  A user can modify the controls on the form
// and the methods will update the database.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EIAP
{
    public partial class FormNotificationEmail : Form
    {
        // Constructor
        public FormNotificationEmail()
        {
            InitializeComponent();
        }

        // --------- FIELD ------------------------------------------------
        private static FormNotificationEmail _AnInstance;

        // ----------------- PROPERTIES -----------------------------------
        internal static FormNotificationEmail instance
        {
            get
            {
                if (_AnInstance == null || _AnInstance.IsDisposed)
                {
                    _AnInstance = new FormNotificationEmail();
                }
                return _AnInstance;
            }
    
        }
        private DataTable aDataTable { get; set; }

        // ----------------------- END PROPERTIES-----------------------------------

        // Event --- Loads Binding settings for controls on form before it is displayed.
        private void FormNotificationEmail_Load(object sender, EventArgs e)
        {
            setupBindings();
        }

        
        // EVENT --- Button close event that will clear the label and then close the form
        private void buttonClose_Click(object sender, EventArgs e)
        {
            labelConfirm.Text = "";
            Close();
        }

        // EVENT --- Button Save event that will save the information in the text boxes/controls
        // Which will add to database and the refresh the form
        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {   // Save information to Database and notify user of success
                FormMain.midTier.addEmailNotification(textBoxTo.Text, textBoxFrom.Text, textBoxSubject.Text, textBoxBody.Text);
                labelConfirm.Text = "Saved successfully at " + DateTime.Now;
            }
            catch (Exception ex)  // IF the save is unsuccessful
            {
                labelConfirm.Text = "Operation Unsuccessful at " + DateTime.Now;
                MessageBox.Show("Unable to save values." + Environment.NewLine + ex.Message);
            }
            finally
            {   // Setup the bindings again 
                setupBindings();
            }
        }

        // Event --- Clear the text from the text boxes and labels
        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBoxTo.Clear();
            textBoxFrom.Clear();
            textBoxSubject.Clear();
            textBoxBody.Clear();
            labelConfirm.Text = "";
        }

        // Event --- Create a test email and send
        private void buttonSendTestEmail_Click(object sender, EventArgs e)
        {
            try
            {
                // First try to save 
                buttonSave_Click(sender, e);
                // send test email
                FormMain.midTier.testSendEmail();
                labelSent.Text = "Message Sent at " + DateTime.Now;
            }
            catch (System.ArgumentException)
            {
                MessageBox.Show("Please ensure all information has been entered correctly.");
                labelSent.Text = "Unsuccessful";
            }
            catch (Exception ex)  // If there is a problem then notify user
            {
                MessageBox.Show(ex.Message);
                labelSent.Text = "Unsuccessful";
            }
        }

        // METHOD --- Set up the data source/binding for 
        private void setupBindings()
        {
            // SMTP Form Data binding Setup
            try
            {   // clear the bindings
                clearBindings();
                // clear the data table
                aDataTable = null;
                // Instantiate a new data table
                aDataTable = new DataTable();
                // get a copy of the data base table and fill into instantiated table
                aDataTable = FormMain.midTier.getTable("TB_NOTIFICATION_EMAIL_SETTINGS").Copy();
                // add the data source/binding for the controls
                textBoxTo.DataBindings.Add("text", aDataTable, "TO", false, DataSourceUpdateMode.Never);
                textBoxFrom.DataBindings.Add("text", aDataTable, "FROM", false, DataSourceUpdateMode.Never);
                textBoxSubject.DataBindings.Add("text", aDataTable, "SUBJECT", false, DataSourceUpdateMode.Never);
                textBoxBody.DataBindings.Add("text", aDataTable, "BODY", false, DataSourceUpdateMode.Never);
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to load data.");
            }
            finally
            {   // clear the bindings
                clearBindings();
            }
        }

        // Method --- clear the bindings
        private void clearBindings()
        {
            textBoxTo.DataBindings.Clear();
            textBoxFrom.DataBindings.Clear();
            textBoxSubject.DataBindings.Clear();
            textBoxBody.DataBindings.Clear();
        }

    }
}
