// Programmer: Matthew White
// File: FormStartUp.cs
// Date: 5/5/2015
// Purpose: This class is a windows form that represents the Start up options.  A user can modify the controls on the form
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
    public partial class FormStartUp : Form
    {   // Constructor
        public FormStartUp()
        {
            InitializeComponent();
        }


        // Field
        private static FormStartUp _AnInstance;

        // End Field

        // -------------------- PROPERTY --------------------------------

        internal static FormStartUp instance
        {
            get
            {
                if (_AnInstance == null || _AnInstance.IsDisposed)
                {
                    _AnInstance = new FormStartUp();
                }
                return _AnInstance;
            }
           
        }

        private DataTable aDataTable { get; set; }
        private string lastUpdateValue { get; set; }

        // --------------------- END PROPERTY  -------------------------------

        // EVENT --- Form start up event
        private void FormStartUp_Load(object sender, EventArgs e)
        {
            // creating bindings on form components
            setupBinding();
        }

        // EVENT --- Button OK Click event which retrieve values from controls
        // and save to database when the button is clicked.
        private void buttonOK_Click(object sender, EventArgs e)
        {
            try
            {   // Add values from controls on form to database
                FormMain.midTier.addLoadOptions(checkBoxMinimize.Checked, checkBoxAutoStart.Checked, textBoxUpdateInterval.Text);

                // Detects if the update interval text box has changed its value
                // notifies user to start and stop the program to take effect the new settings
                if (textBoxUpdateInterval.Text != lastUpdateValue)
                {
                    MessageBox.Show("Please start and stop the application for the Update time to take effect.");
                }

                // Close the Form
                Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to update, please enter data all data.");
            }
            finally
            {
                setupBinding();
            } 
        }

        // Create a binding source based upon a data table and bind to controls on form.
        private void setupBinding()
        {
            try
            {
                // Clear any binding that may exist
                clearBindings();
                // clear the Data Table
                aDataTable = null;

                // Instantiate a new Data Table
                aDataTable = new DataTable();
                // Fill with information from Database
                aDataTable = FormMain.midTier.getTable("TB_LOAD_OPTIONS").Copy();
                
                // Add bindings to controls on form
                checkBoxMinimize.DataBindings.Add("Checked", aDataTable, "MINIMIZE", false, DataSourceUpdateMode.Never);
                checkBoxAutoStart.DataBindings.Add("Checked", aDataTable, "AUTO_START", false, DataSourceUpdateMode.Never);
                textBoxUpdateInterval.DataBindings.Add("text", aDataTable, "UPDATE_INTERVAL", false, DataSourceUpdateMode.Never);
                lastUpdateValue = textBoxUpdateInterval.Text;
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to load values.");
            }
            finally
            {
               clearBindings();
            }
        }

        // Clear all bindings on controls on form
        private void clearBindings()
        {
            checkBoxMinimize.DataBindings.Clear();
            checkBoxAutoStart.DataBindings.Clear();
            textBoxUpdateInterval.DataBindings.Clear();
        }
    }
}
