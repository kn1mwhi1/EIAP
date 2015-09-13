// Programmer: Matthew White
// File: FormSettings.cs
// Date: 5/5/2015
// Purpose: This class is a windows form that represents the Settings Form.  
// The user can choose to bring up other forms from this form.

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
    public partial class FormSettings : Form
    {
        // Constructor
        public FormSettings()
        {
            InitializeComponent();
        }

        // ------------- FIELD  ----------------------------------
        private static FormSettings _AnInstance;

        // ------------------- PROPERTIES ------------------------
            // ------ Singleton design Pattern -------------
        internal static FormSettings instance
        {
            get
            {
                if (_AnInstance == null || _AnInstance.IsDisposed)
                {
                    _AnInstance = new FormSettings();
                }
                return _AnInstance;
            }
        }

        // --------------- END PROPERTIES ----------------------


        // EVENT -- Show the SMTP Settings Form
        private void buttonSMTP_Click(object sender, EventArgs e)
        {
            ShowFormSMTPSettings();
        }

        // EVENT --- Show the Email Form settings
        private void buttonEmailNotification_Click(object sender, EventArgs e)
        {
            ShowFormNotificationEmailsettings();
        }

        // Event --- Show the source form
        private void buttonSource_Click(object sender, EventArgs e)
        {
            ShowFormSource();
        }

        // Event --- Show the Start up form 
        private void buttonStartUp_Click(object sender, EventArgs e)
        {
            ShowFormStartUp();
        }

        // Method --- Show the SMTP settings 
        private void ShowFormSMTPSettings()
        {
            // instantiate the form and bring to focus and to front.
            FormSMTP form = FormSMTP.instance;
            form.BringToFront();
            form.Show();
            form.Focus();
        }

        // Method --- Show the Source settings form
        private void ShowFormSource()
        {
            // instantiate the form and bring to focus and to front.
            FormSource form = FormSource.instance;
            form.BringToFront();
            form.Show();
            form.Focus();
        }

        // Method --- Show the startup settings form
        private void ShowFormStartUp()
        {
            // instantiate the form and bring to focus and to front.
            FormStartUp form = FormStartUp.instance;
            form.BringToFront();
            form.Show();
            form.Focus();
        }

        // METHOD --- Show Email Notification Email Settings
        private void ShowFormNotificationEmailsettings()
        {
            // instantiate the form and bring to focus and to front.
            FormNotificationEmail form = FormNotificationEmail.instance;
            form.BringToFront();
            form.Show();
            form.Focus();
        }



    }
}
