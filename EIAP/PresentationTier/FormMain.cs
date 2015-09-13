// Programmer: Matthew White
// File: FormMain.cs
// Date: 5/5/2014
// Main form of the External IP Analyzer Program
// The presentation Tier comprises of all the Forms (UI).  The Middle Tier or Logical Tier is responsible
// for communication with the database and the Presentation Tier.  In order for the Presentation Tier to retrieve or
// Save information the Presentation Tier will communicate with the Middle Tier and the Middle Tier will communicate
// to the Database Tier.

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
    public partial class FormMain : Form
    {   
        // Constructor
        public FormMain()
        {
            InitializeComponent();
        }

        // -----------------------  PROPERTY  ---------------------------

        internal static LogicTier midTier { get; set; }

        // -----------------------  END PROPERTY  -----------------------


        // ---------------------  EVENTS -----------------------------------

        // Event --- After the Form is displayed 
            // Display Retrieving Options
            // Instantiate the mid tier
            // Get Load options and execute autostart and/or minimize form
        private void FormMain_Shown(object sender, EventArgs e)
        {

            try
            {
                // Display Loading Message when first load
                labelIPAddress.Text = "Loading..";
                
                // Display Loading options when program first loads
                displayMessage("Retrieving Options...");

         

                   try
                   {

                       // Instantiate the logic/mid Tier
                       instantiateMidTier();

                        // checks to see if user has selected minimize on load from Database
                        if (FormMain.midTier.getMinimizeDataSet())
                        {
                        displayMessage("Minimize on load has been activated");
                        this.WindowState = FormWindowState.Minimized;
                        checkMinimizeToTray();
                        }
 

                        // Checks to see if user has selected Autostart in Database
                        if (FormMain.midTier.getAutoStartDataSet())
                        {
                             displayMessage("Auto-start on load has been activated with a refresh time of " + FormMain.midTier.getUpdateIntervalDataSet() + " minutes.");
                             timerUpdateInterval.Interval = (Convert.ToInt32(FormMain.midTier.getUpdateIntervalDataSet()) * 60000);
                             buttonStart_Click(sender, e);
                        }
                        else
                        {
                            displayMessage("Press start to activate the program.");
                            labelIPAddress.Text = "Press start";
                        }
                     }
                    catch (Exception ex)
                    {
                        displayMessage("Error when loading program options. " + ex.Message);
                        labelIPAddress.Text = "Error";
                    }

                }
            
            catch (System.NullReferenceException exs)
            {
                MessageBox.Show("Could not load Database values" + exs.StackTrace);
                // Display Error
                labelIPAddress.Text = "Error";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                labelIPAddress.Text = "Error";
            }

        }

        // Event ---  Do certain actions when and if the form is minimized
        private void FormMain_Resize(object sender, EventArgs e)
        {
            checkMinimizeToTray();
        }

        // ---------------------------  Button Events  -------------------------------------------

        // Event --- Button Quit event.. closes the program
        private void buttonQuit_Click(object sender, EventArgs e)
        {
            // Exit Application
            Application.Exit();
        }

        // Event --- Start Button Event
        private void buttonStart_Click(object sender, EventArgs e)
        {
            // Try to instantiate the mid tier if null
            if (midTier == null)
            {
                instantiateMidTier();
            }


            // Run program if Logic tier has been instantiated
            if (midTier != null)
            {
                // Call the function that will start the logic to get external ip and
                // start timer (after getting interval from database).

                try
                {   // Pass the text box to function which will update the control
                    FormMain.midTier.startForUI(ref richTextBoxDisplay);
                    richTextBoxDisplay.ScrollToCaret();
                    timerUpdateInterval.Interval = (Convert.ToInt32(FormMain.midTier.getUpdateIntervalDataSet()) * 60000);
                    timerUpdateInterval.Enabled = true;
                    timerUpdateInterval.Start();
                }
                catch (System.Net.Mail.SmtpException smtp )
                {
                    richTextBoxDisplay.AppendText(Environment.NewLine + "Detailed Error: " + smtp.Message + Environment.NewLine + "Please ensure all SMTP settings are correct.");
                    richTextBoxDisplay.ScrollToCaret();
                }
                catch (Exception ex)
                {
                    richTextBoxDisplay.AppendText(Environment.NewLine + "Detailed Error: " + ex.Message + Environment.NewLine + "Please ensure the correct emails are being used in notification settings.");
                    richTextBoxDisplay.ScrollToCaret();
                }

                // Refresh labels on RichTextBox
                refreshLabels();
            }
        }

        // Event --- Timer tick event
        private void timerUpdateInterval_Tick_1(object sender, EventArgs e)
        {
            // Call start button event
            buttonStart_Click(sender, e);
            // Stop and start the timer
            timerUpdateInterval.Stop();
            timerUpdateInterval.Start();
            timerUpdateInterval.Enabled = true;

            // Update BallonTipText with IP Address
            notifyIcon.BalloonTipText = "IP: " + midTier.currentIP;
            notifyIcon.Text = "EIAP: Active" + Environment.NewLine + "IP: " + midTier.currentIP;
        }

        // Event --- Button Settings Event
        private void buttonSettings_Click(object sender, EventArgs e)
        {
            ShowFormSettings();
        }

        // Event --- Button Stop - Stops the timer and updates the text on the UI
        private void buttonStop_Click(object sender, EventArgs e)
        {
            // Stop timer
            timerUpdateInterval.Stop();
            // update GUI to show that program has disconnected and stopped
            labelConnectedAlias.Text = "Disconnected";
            displayMessage(Environment.NewLine + "The program has stopped.");
            notifyIcon.Text = "EIAP: Stopped";
        }


       // ------------------------ Menu Drop down Events ----------------------------------

        // Event --- Shows the Application when user double clicks on Notify Icon
        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Maximize form and show
            maximizeShowForm();

            // Scroll text box to bottom
            richTextBoxDisplay.ScrollToCaret();
        }

        // Event --- Tray context menu to Start the program
        private void toolStripMenuItemStart_Click(object sender, EventArgs e)
        {
            // Call the start button click event
            buttonStart_Click(sender, e);
            notifyIcon.BalloonTipText = "EIAP: Active" + Environment.NewLine + "IP: " + midTier.currentIP;
            notifyIcon.Text = "EIAP: Active" + Environment.NewLine + "IP: " + midTier.currentIP;
            notifyIcon.ShowBalloonTip(300);
        }

        // Event --- Tray context menu to Stop the program
        private void toolStripMenuItemStop_Click(object sender, EventArgs e)
        {
            // Call the start button click event
            buttonStop_Click(sender, e);
            notifyIcon.BalloonTipText = "EIAP: Stopped";
            notifyIcon.ShowBalloonTip(300);
        }

        // Event --- Tray context menu to show the Settings Form
        private void toolStripMenuItemSettings_Click(object sender, EventArgs e)
        {
            // Call the settings button click event
            buttonSettings_Click(sender, e);
        }

        // Event --- Tray context menu to Quit the program
        private void toolStripMenuItemQuit_Click(object sender, EventArgs e)
        {
            // Call the quite button click event
            buttonQuit_Click(sender, e);
        }
        // Event --- Menu drop down Quit event
        private void quiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Call the button quit event
            buttonQuit_Click(sender, e);
        }

        // ------------------------  Context Menu for Tray Icon  ------------------------------
        // Event --- Tray context menu show and maximize the form
        private void toolStripMenuItemShow_Click(object sender, EventArgs e)
        {
            // Maximize form and show
            maximizeShowForm();
        }

        // Event --- Context Menu for tray icon - Hide and minimize form
        private void toolStripMenuItemHide_Click(object sender, EventArgs e)
        {
            //MinimizeBox form
            this.WindowState = FormWindowState.Minimized;
            checkMinimizeToTray();
        }

        // Event --- Menu drop down start
        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Call Start button event
            buttonStart_Click(sender, e);
        }

        // Event --- Stop on Dropdown menu.
        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Call Stop button event
            buttonStop_Click(sender, e);
        }

        // Event ---- Hide option in dropdown menu.
        private void hideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            checkMinimizeToTray();
        }

        // Event --- Menu drop down event loads settings
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Button settings event
            buttonSettings_Click(sender, e);
        }

        // Event --- Menu drop down Help menu About box
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowAboutBox();
        }

        // Event --- Copy IP to clipboard
        private void copyIPToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Copy current IP to Clipboard
            System.Windows.Forms.Clipboard.SetText(midTier.currentIP);
        }

        // ---------------------  END EVENTS -----------------------------------


        // ----------------------  METHODS  --------------------------------------
        // Function that will instantiate the mid tier
        private void instantiateMidTier()
        {
            try
            {
                midTier = new LogicTier();
            }
            catch (System.IndexOutOfRangeException) // Catch if user does not have data in database fields
            {
                // Stop the timer
                timerUpdateInterval.Stop();
                // Update the labels and text boxes for user
                labelIPAddress.Text = "Please enter Data";
                displayMessage("Please enter data in settings.");

            }
            catch (Exception ex)
            {
                richTextBoxDisplay.AppendText("Unable to open Database " + ex.ToString());
                richTextBoxDisplay.ScrollToCaret();
            }
        }


        // Minimize program to tray function
        private void checkMinimizeToTray()
        {
            // Check to see if window is minimized
            if (FormWindowState.Minimized == this.WindowState)
            {   // Show tray Icon
                ShowInTaskbar = false;
                notifyIcon.Visible = true;

                if (timerUpdateInterval.Enabled)
                {
                    notifyIcon.Text = "EIAP: Active" + Environment.NewLine + "IP: " + midTier.currentIP;
                    notifyIcon.BalloonTipText = "EIAP: Active" + Environment.NewLine + "IP: " + midTier.currentIP;
                }
                else
                {
                    notifyIcon.Text = "EIAP: Stopped";
                    notifyIcon.BalloonTipText = "EIAP: Stopped";
                }
                
                notifyIcon.ShowBalloonTip(300);
                this.Hide();
            }
                // Check to see if window is maximized
            else if (FormWindowState.Normal == this.WindowState)
            {   // Do not show Tray Icon
                notifyIcon.Visible = false;
            }
        }

        // Function accepts a string and will display the string in
        // the text box on this main form.
        private void displayMessage(string aMessage)
        {   // Add to richTextBox
            richTextBoxDisplay.AppendText(aMessage + Environment.NewLine);
            // scroll down so user can see new entries first
            richTextBoxDisplay.ScrollToCaret();
        }

       
        // Instantiate the Settings Form
        private void ShowFormSettings()
        {
            // instantiate the form and bring to focus and to front.
            FormSettings form = FormSettings.instance;
            form.BringToFront();
            form.Show();
            form.Focus();
        }

        // Shows current connection information on all labels on form
        private void refreshLabels()
        {
            try
            {   // Refresh Labels on the GUI , show current IP
                labelIPAddress.Text = FormMain.midTier.currentIP;
                // show connected alias
                labelConnectedAlias.Text = "Connected alias: " + FormMain.midTier.currentAlias;
                // show last time IP was refreshed
                labelLastRefresh.Text = "Last refresh: " + FormMain.midTier.lastRefresh;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        // Method that is responsible for maximize and show form
        private void maximizeShowForm()
        {
            // show in task bar
            ShowInTaskbar = true;
            // remove icon from tray
            notifyIcon.Visible = false;
            // show form
            this.Show();
            // show form by setting state to normal
            WindowState = FormWindowState.Normal;
        }

        // Show/ instantiate the About Box Form
        private void ShowAboutBox()
        {
            // instantiate the form and bring to focus and to front.
            AboutBox form = AboutBox.instance;
            form.BringToFront();
            form.Show();
            form.Focus();
        }







        // --------------------- END METHOD -------------------------
    }
}
