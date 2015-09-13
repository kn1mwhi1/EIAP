namespace EIAP
{
    partial class FormSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSettings));
            this.buttonSMTP = new System.Windows.Forms.Button();
            this.buttonEmailNotification = new System.Windows.Forms.Button();
            this.buttonSource = new System.Windows.Forms.Button();
            this.buttonStartUp = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonSMTP
            // 
            this.buttonSMTP.Location = new System.Drawing.Point(9, 9);
            this.buttonSMTP.Name = "buttonSMTP";
            this.buttonSMTP.Size = new System.Drawing.Size(161, 33);
            this.buttonSMTP.TabIndex = 0;
            this.buttonSMTP.Text = "SMTP";
            this.buttonSMTP.UseVisualStyleBackColor = true;
            this.buttonSMTP.Click += new System.EventHandler(this.buttonSMTP_Click);
            // 
            // buttonEmailNotification
            // 
            this.buttonEmailNotification.Location = new System.Drawing.Point(9, 47);
            this.buttonEmailNotification.Name = "buttonEmailNotification";
            this.buttonEmailNotification.Size = new System.Drawing.Size(161, 33);
            this.buttonEmailNotification.TabIndex = 1;
            this.buttonEmailNotification.Text = "Email Notification";
            this.buttonEmailNotification.UseVisualStyleBackColor = true;
            this.buttonEmailNotification.Click += new System.EventHandler(this.buttonEmailNotification_Click);
            // 
            // buttonSource
            // 
            this.buttonSource.Location = new System.Drawing.Point(9, 85);
            this.buttonSource.Name = "buttonSource";
            this.buttonSource.Size = new System.Drawing.Size(161, 33);
            this.buttonSource.TabIndex = 2;
            this.buttonSource.Text = "Source";
            this.buttonSource.UseVisualStyleBackColor = true;
            this.buttonSource.Click += new System.EventHandler(this.buttonSource_Click);
            // 
            // buttonStartUp
            // 
            this.buttonStartUp.AutoSize = true;
            this.buttonStartUp.Location = new System.Drawing.Point(9, 125);
            this.buttonStartUp.Name = "buttonStartUp";
            this.buttonStartUp.Size = new System.Drawing.Size(161, 33);
            this.buttonStartUp.TabIndex = 3;
            this.buttonStartUp.Text = "Start Up";
            this.buttonStartUp.UseVisualStyleBackColor = true;
            this.buttonStartUp.Click += new System.EventHandler(this.buttonStartUp_Click);
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(182, 165);
            this.Controls.Add(this.buttonStartUp);
            this.Controls.Add(this.buttonSource);
            this.Controls.Add(this.buttonEmailNotification);
            this.Controls.Add(this.buttonSMTP);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSettings";
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonSMTP;
        private System.Windows.Forms.Button buttonEmailNotification;
        private System.Windows.Forms.Button buttonSource;
        private System.Windows.Forms.Button buttonStartUp;
    }
}