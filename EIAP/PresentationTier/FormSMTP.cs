// Programmer: Matthew White
// File: FormSMTP.cs
// Date: 5/5/2015
// Purpose: This class is a windows form that represents the SMTP options.  A user can modify the controls on the form
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
    public partial class FormSMTP : Form
    {   
        // Constructor
        public FormSMTP()
        {
            InitializeComponent();
        }

        // -------------  FIELD --------------------------------
        private static FormSMTP _AnInstance;

        // ------------------ PROPERTIES ----------------------
            // Singleton Design Pattern to ensure forms will only be instantiated
            // one at a time.
        internal static FormSMTP instance
        {
            get
            {
                if (_AnInstance == null || _AnInstance.IsDisposed)
                {
                    _AnInstance = new FormSMTP();
                }
                return _AnInstance;
            }

        }
        private DataTable aDataTable { get; set; }

        // --------------------- END PROPERTIES -------------------

        //EVENT --- Loads the bindings before the form is displayed
        private void FormSMTP_Load(object sender, EventArgs e)
        {
            setUpSMTPGUIBindings();
        }
        // EVENT ---- Clears the confirm lable and closes the form
        private void buttonClose_Click(object sender, EventArgs e)
        {
            labelConfirm.Text = "";
            Close();
        }
        // EVENT --- Clears the label and clears the text from the 
        // other controls on the form
        private void buttonClear_Click(object sender, EventArgs e)
        {
            labelConfirm.Text = "";
            clearControls();
        }
        // EVENT --- Saves the information from the text boxes to the database
        // Also updates the labels on the form
        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {   // Add information from Text Boxes to database
                FormMain.midTier.addRowSMTP(textBoxUserName.Text, textBoxHost.Text, textBoxPassword.Text, textBoxPort.Text, checkBoxTLS.Checked);
                // Update label on form
                labelConfirm.Text = "Saved Successfully at " + DateTime.Now;
            }
            catch (Exception)
            {
                // Update label on form
                labelConfirm.Text = "Unsuccessful Save at " + DateTime.Now;
                MessageBox.Show("Unable to update, please ensure all values have been entered.");
            }
            finally
            {
                // Last thing to do, setup the SMTP control bindings so it will 
                // read the database and display in controls
                setUpSMTPGUIBindings();
            }
        }
       
        // Sets up a data source/binding for each control after a copy of the database is downloaded
        //  Also displays that information in each control
        private void setUpSMTPGUIBindings()
        {
            // SMTP Form Data binding Setup
            try
            {
                // Clear all bindings on controls 
                clearBindings();
                // Clear the data table on form
                aDataTable = null; 
               // Instantiate a New data table
                aDataTable = new DataTable();
                // Retrieve a copy of the table from database
                aDataTable = FormMain.midTier.getTable("TB_SMTP_SETTINGS").Copy();
                // setup data bindings with controls so they will display the information in the table
                textBoxUserName.DataBindings.Add("text", aDataTable, "USERNAME", false, DataSourceUpdateMode.Never);
                textBoxPassword.DataBindings.Add("text", aDataTable, "PASSWORD", false, DataSourceUpdateMode.Never);
                textBoxHost.DataBindings.Add("text", aDataTable, "HOST", false, DataSourceUpdateMode.Never);
                textBoxPort.DataBindings.Add("text", aDataTable, "PORT", false, DataSourceUpdateMode.Never);
                checkBoxTLS.DataBindings.Add("Checked", aDataTable, "TLS", false, DataSourceUpdateMode.Never);
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to load values.");
            }
            finally
            {
                // After displaying the information from the database clear the connection
                clearBindings();
            }
        }
        // Clears the data source/ data binding from the controls
        private void clearBindings()
        {
            textBoxUserName.DataBindings.Clear();
            textBoxPassword.DataBindings.Clear();
            textBoxHost.DataBindings.Clear();
            textBoxPort.DataBindings.Clear();
            checkBoxTLS.DataBindings.Clear();
        }
        // Clears the text from each control or sets control false
        private void clearControls()
        {
            textBoxUserName.Clear();
            textBoxPassword.Clear();
            textBoxHost.Clear();
            textBoxPort.Clear();
            checkBoxTLS.Checked = false;
        }
    }
}
