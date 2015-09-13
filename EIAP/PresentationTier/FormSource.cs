// Programmer: Matthew White
// File: FormSource.cs
// Date: 5/5/2015
// Purpose: This class is a windows form with the purpose of updating the Source information in the database.
// A user can change the controls located on the form and the methods in the class will update the database.

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
    public partial class FormSource : Form
    {   
        // Constructor
        public FormSource()
        {
            InitializeComponent();
        }

       // --------------------- FIELD ----------------------------------------
        private static FormSource _AnInstance;

        // --------------------- PROPERTIES -----------------------------------
        // SingleTon design Pattern
        internal static FormSource instance
        {
            get
            {
                if (_AnInstance == null || _AnInstance.IsDisposed)
                {
                    _AnInstance = new FormSource();
                }
                return _AnInstance;
            }

        }

        private DataTable aDataTable { get; set; }
        private DataRow dr { get; set; }

        // ------------------------ END PROPERTIES ----------------------------

        // EVENT -- When form loads the DataGride and controls need to be
        // linked to the database, this will create the binding
        private void FormSource_Load(object sender, EventArgs e)
        {
            setUpDataGrideBinding();
        }

        // EVENT --- Closes the form when the user presses the close button
        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        // EVENT --- Add new button event on Form which will add a new user to the database
        private void buttonAddNew_Click(object sender, EventArgs e)
        {
            try
            {   // Pass the DataTable and new row to Logic Tier to Add to Database.
                FormMain.midTier.addSourceInfoToDataTable(aDataTable, textBoxPriority.Text, textBoxAlias.Text, textBoxHttpAddress.Text,
                    checkBoxRequiresUsername.Checked, textBoxUsername.Text, textBoxPassword.Text);
            }
            catch (System.Data.ConstraintException)
            {
                MessageBox.Show("The priority must be unique, please try again.");
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to update, please ensure all values are entered.");
            }
        }

        // EVENT --- Remove button click event which will remove an item from the Data grid
        //  And will update the database
        private void buttonRemove_Click(object sender, EventArgs e)
        {
            try
            {
                // Delete selection from datagridview
                dataGridViewSource.Rows.RemoveAt(dataGridViewSource.SelectedRows[0].Index);
               
                // Update DataTable to Database
                FormMain.midTier.addSourceInfoToDatabase(aDataTable);

                // Clear data binding on DataGridView
                dataGridViewSource.DataBindings.Clear();
               
                // Refresh DataGridView and Data
                setUpDataGrideBinding();
            }
            catch (System.InvalidOperationException)
            {
                MessageBox.Show("Please select an item before pressing delete.");
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to remove.");
            }
        }


        // EVENT --- When a user clicks on a row in the Data grid view the information will 
        // be displayed to the text boxes
        private void dataGridViewSource_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {   // Variable that will hold the current row index
                int rowInteger = dataGridViewSource.CurrentCell.RowIndex;
          
                // Update text box with row the user clicked on in Datagridview
                textBoxPriority.Text = dataGridViewSource[0, rowInteger].Value.ToString();
                textBoxAlias.Text = dataGridViewSource[1, rowInteger].Value.ToString();
                textBoxHttpAddress.Text = dataGridViewSource[2, rowInteger].Value.ToString();
                checkBoxRequiresUsername.Checked = (Convert.ToBoolean(dataGridViewSource[3, rowInteger].Value));
                textBoxUsername.Text = dataGridViewSource[4, rowInteger].Value.ToString();
                textBoxPassword.Text = dataGridViewSource[5, rowInteger].Value.ToString();
            }
            catch
            {
                // Do nothing
            }
        }

        // EVENT --- When a user clicks the clear button the event will call two methods
        // These two methods will clear the binding and text in the text box controls
        private void buttonClear_Click(object sender, EventArgs e)
        {
            // Clear bindings
            clearBindings();
            // Clear text in text boxes
            clearData();
        }

        // EVENT -- When a user checks or unchecks the Requires Username check box
        // this will hide or show the text box for the username and password on form
        private void checkBoxRequiresUsername_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxRequiresUsername.Checked)
            {
                // Show the text boxes
                textBoxPassword.Enabled = true;
                textBoxUsername.Enabled = true;
            }
            else
            {
                // Hide the text boxes
                textBoxPassword.Enabled = false;
                textBoxUsername.Enabled = false;
            }
        }

        // Method that will setup the Data Grid on the Main Group Box
        // Setups up binding source which will supply the controls with data
        // From the database
        private void setUpDataGrideBinding()
        {
            // Data Grid Setup
            try
            {
                // Clear the data table on form
                aDataTable = null;
                // instantiate a new data table
                aDataTable = new DataTable();
                // copy information from database to the data table
                aDataTable = FormMain.midTier.getTable("TB_SOURCE").Copy();
                // ensure the data table is sorted by priority
                aDataTable.DefaultView.Sort = "PRIORITY";

                // Set the data source of the dataGridView to the DataTable
                dataGridViewSource.DataSource = aDataTable;
            }
            catch (Exception)
            {
                // If there is a problem show the message to the user.
                MessageBox.Show("Unable to load values.");
            }
            finally // no matter what do this Format the DataGridView
            {
                // Sets properties of the DataGridView
                dataGridViewSource.AutoGenerateColumns = false;
                dataGridViewSource.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                try
                {
                    // Priority Column
                    dataGridViewSource.Columns[0].HeaderText = "Priority";
                    dataGridViewSource.Columns[0].Width = 40;
                    // Alias Column
                    dataGridViewSource.Columns[1].HeaderText = "Alias";
                    dataGridViewSource.Columns[1].Width = 90;
                    // HTTP Address Name Column
                    dataGridViewSource.Columns[2].HeaderText = "HTTP Address";
                    dataGridViewSource.Columns[2].Width = 315;
                    // Authentication
                    dataGridViewSource.Columns[3].Visible = false;
                    // User Name
                    dataGridViewSource.Columns[4].Visible = false;
                    // Password
                    dataGridViewSource.Columns[5].Visible = false;
                }
                catch
                {
                    // do nothing
                }

            }
        }

        // Method will clear any bindings that the controls may have
        private void clearBindings()
        {
            textBoxAlias.DataBindings.Clear();
            textBoxHttpAddress.DataBindings.Clear();
            textBoxUsername.DataBindings.Clear();
            textBoxPassword.DataBindings.Clear();
            textBoxPriority.DataBindings.Clear();
            checkBoxRequiresUsername.DataBindings.Clear();
        }

        // Method will remove all text in controls
        private void clearData()
        {
            textBoxAlias.Clear();
            textBoxHttpAddress.Clear();
            textBoxUsername.Clear();
            textBoxPassword.Clear();
            textBoxPriority.Clear();
            checkBoxRequiresUsername.Checked = false;
        }
    }
}
