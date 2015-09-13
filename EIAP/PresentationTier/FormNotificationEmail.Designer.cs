namespace EIAP
{
    partial class FormNotificationEmail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormNotificationEmail));
            this.textBoxFrom = new System.Windows.Forms.TextBox();
            this.labelFrom = new System.Windows.Forms.Label();
            this.textBoxTo = new System.Windows.Forms.TextBox();
            this.labelTo = new System.Windows.Forms.Label();
            this.textBoxSubject = new System.Windows.Forms.TextBox();
            this.labelSubject = new System.Windows.Forms.Label();
            this.textBoxBody = new System.Windows.Forms.TextBox();
            this.labelBody = new System.Windows.Forms.Label();
            this.buttonSendTestEmail = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.labelConfirm = new System.Windows.Forms.Label();
            this.labelSent = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBoxFrom
            // 
            this.textBoxFrom.Location = new System.Drawing.Point(54, 44);
            this.textBoxFrom.Name = "textBoxFrom";
            this.textBoxFrom.Size = new System.Drawing.Size(272, 20);
            this.textBoxFrom.TabIndex = 2;
            // 
            // labelFrom
            // 
            this.labelFrom.AutoSize = true;
            this.labelFrom.Location = new System.Drawing.Point(8, 47);
            this.labelFrom.Name = "labelFrom";
            this.labelFrom.Size = new System.Drawing.Size(33, 13);
            this.labelFrom.TabIndex = 15;
            this.labelFrom.Text = "From:";
            // 
            // textBoxTo
            // 
            this.textBoxTo.Location = new System.Drawing.Point(54, 12);
            this.textBoxTo.Name = "textBoxTo";
            this.textBoxTo.Size = new System.Drawing.Size(272, 20);
            this.textBoxTo.TabIndex = 1;
            // 
            // labelTo
            // 
            this.labelTo.AutoSize = true;
            this.labelTo.Location = new System.Drawing.Point(8, 15);
            this.labelTo.Name = "labelTo";
            this.labelTo.Size = new System.Drawing.Size(23, 13);
            this.labelTo.TabIndex = 13;
            this.labelTo.Text = "To:";
            // 
            // textBoxSubject
            // 
            this.textBoxSubject.Location = new System.Drawing.Point(54, 70);
            this.textBoxSubject.Name = "textBoxSubject";
            this.textBoxSubject.Size = new System.Drawing.Size(408, 20);
            this.textBoxSubject.TabIndex = 3;
            // 
            // labelSubject
            // 
            this.labelSubject.AutoSize = true;
            this.labelSubject.Location = new System.Drawing.Point(8, 73);
            this.labelSubject.Name = "labelSubject";
            this.labelSubject.Size = new System.Drawing.Size(46, 13);
            this.labelSubject.TabIndex = 17;
            this.labelSubject.Text = "Subject:";
            // 
            // textBoxBody
            // 
            this.textBoxBody.Location = new System.Drawing.Point(54, 96);
            this.textBoxBody.Multiline = true;
            this.textBoxBody.Name = "textBoxBody";
            this.textBoxBody.Size = new System.Drawing.Size(408, 127);
            this.textBoxBody.TabIndex = 4;
            // 
            // labelBody
            // 
            this.labelBody.AutoSize = true;
            this.labelBody.Location = new System.Drawing.Point(8, 99);
            this.labelBody.Name = "labelBody";
            this.labelBody.Size = new System.Drawing.Size(34, 13);
            this.labelBody.TabIndex = 19;
            this.labelBody.Text = "Body:";
            // 
            // buttonSendTestEmail
            // 
            this.buttonSendTestEmail.Location = new System.Drawing.Point(357, 23);
            this.buttonSendTestEmail.Name = "buttonSendTestEmail";
            this.buttonSendTestEmail.Size = new System.Drawing.Size(101, 23);
            this.buttonSendTestEmail.TabIndex = 8;
            this.buttonSendTestEmail.Text = "Send Test Email";
            this.buttonSendTestEmail.UseVisualStyleBackColor = true;
            this.buttonSendTestEmail.Click += new System.EventHandler(this.buttonSendTestEmail_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(54, 243);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 5;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(218, 243);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(75, 23);
            this.buttonClear.TabIndex = 6;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(382, 243);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 7;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // labelConfirm
            // 
            this.labelConfirm.AutoSize = true;
            this.labelConfirm.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelConfirm.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.labelConfirm.Location = new System.Drawing.Point(194, 230);
            this.labelConfirm.Name = "labelConfirm";
            this.labelConfirm.Size = new System.Drawing.Size(0, 13);
            this.labelConfirm.TabIndex = 25;
            // 
            // labelSent
            // 
            this.labelSent.AutoSize = true;
            this.labelSent.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.labelSent.Location = new System.Drawing.Point(364, 51);
            this.labelSent.Name = "labelSent";
            this.labelSent.Size = new System.Drawing.Size(0, 13);
            this.labelSent.TabIndex = 26;
            // 
            // FormNotificationEmail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 282);
            this.Controls.Add(this.labelSent);
            this.Controls.Add(this.labelConfirm);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonSendTestEmail);
            this.Controls.Add(this.textBoxBody);
            this.Controls.Add(this.labelBody);
            this.Controls.Add(this.textBoxSubject);
            this.Controls.Add(this.labelSubject);
            this.Controls.Add(this.textBoxFrom);
            this.Controls.Add(this.labelFrom);
            this.Controls.Add(this.textBoxTo);
            this.Controls.Add(this.labelTo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormNotificationEmail";
            this.Text = "Customize Notification Email Settings";
            this.Load += new System.EventHandler(this.FormNotificationEmail_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxFrom;
        private System.Windows.Forms.Label labelFrom;
        private System.Windows.Forms.TextBox textBoxTo;
        private System.Windows.Forms.Label labelTo;
        private System.Windows.Forms.TextBox textBoxSubject;
        private System.Windows.Forms.Label labelSubject;
        private System.Windows.Forms.TextBox textBoxBody;
        private System.Windows.Forms.Label labelBody;
        private System.Windows.Forms.Button buttonSendTestEmail;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Label labelConfirm;
        private System.Windows.Forms.Label labelSent;
    }
}