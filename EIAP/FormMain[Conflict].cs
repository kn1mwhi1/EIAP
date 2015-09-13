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
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Resize(object sender, EventArgs e)
        {
            minimizeToTray();
        }

        private void minimizeToTray()
        {
            
            
            if (FormWindowState.Minimized == this.WindowState)
            {
                notifyIcon.Visible = true;
                notifyIcon.ShowBalloonTip(500);
                this.Hide();
            }

            else if (FormWindowState.Normal == this.WindowState)
            {
                notifyIcon.Visible = false;
            }

        }



        private void FormMain_Load(object sender, EventArgs e)
        {
            if (Program.midTier.getMinimize())
            {
                this.WindowState = FormWindowState.Minimized;
                minimizeToTray();
            }


            
            if (Program.midTier.getAutoStart())
            {
                timerUpdateInterval.Interval = ( Convert.ToInt32( Program.midTier.getUpdateInterval()) * 60000);
                buttonStart_Click(sender, e);
            }

            // Timer that looks each second for a change is started.
            timerCheckForChange.Start();
        }

        private void buttonSettings_Click(object sender, EventArgs e)
        {
            ShowFormSettings();
        }

        private void ShowFormSettings()
        {
            // instantiate the form and bring to focus and to front.
            FormSettings form = FormSettings.instance;
            form.BringToFront();
            form.Show();
            form.Focus();
        }

        private void buttonQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            try
            {
                Program.midTier.getIPAddressForUI(ref richTextBoxDisplay);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void timerUpdateInterval_Tick(object sender, EventArgs e)
        {

            richTextBoxDisplay.AppendText("IT Works");
            buttonStart_Click(sender, e);
            timerUpdateInterval.Stop();
            timerUpdateInterval.Start();
        }

        private void timerCheckForChange_Tick(object sender, EventArgs e)
        {
            
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            timerUpdateInterval.Stop();
            richTextBoxDisplay.AppendText("The Program has stopped");
        }
       

    }
}
