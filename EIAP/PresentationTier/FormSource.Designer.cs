namespace EIAP
{
    partial class FormSource
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSource));
            this.labelAlias = new System.Windows.Forms.Label();
            this.textBoxAlias = new System.Windows.Forms.TextBox();
            this.buttonAddNew = new System.Windows.Forms.Button();
            this.checkBoxRequiresUsername = new System.Windows.Forms.CheckBox();
            this.dataGridViewSource = new System.Windows.Forms.DataGridView();
            this.labelHttpAddress = new System.Windows.Forms.Label();
            this.textBoxHttpAddress = new System.Windows.Forms.TextBox();
            this.textBoxPriority = new System.Windows.Forms.TextBox();
            this.labelPriority = new System.Windows.Forms.Label();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.labelPassword = new System.Windows.Forms.Label();
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.labelUsername = new System.Windows.Forms.Label();
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSource)).BeginInit();
            this.SuspendLayout();
            // 
            // labelAlias
            // 
            this.labelAlias.AutoSize = true;
            this.labelAlias.Location = new System.Drawing.Point(21, 9);
            this.labelAlias.Name = "labelAlias";
            this.labelAlias.Size = new System.Drawing.Size(32, 13);
            this.labelAlias.TabIndex = 0;
            this.labelAlias.Text = "Alias:";
            // 
            // textBoxAlias
            // 
            this.textBoxAlias.Location = new System.Drawing.Point(97, 6);
            this.textBoxAlias.Name = "textBoxAlias";
            this.textBoxAlias.Size = new System.Drawing.Size(279, 20);
            this.textBoxAlias.TabIndex = 1;
            // 
            // buttonAddNew
            // 
            this.buttonAddNew.Location = new System.Drawing.Point(24, 373);
            this.buttonAddNew.Name = "buttonAddNew";
            this.buttonAddNew.Size = new System.Drawing.Size(75, 23);
            this.buttonAddNew.TabIndex = 7;
            this.buttonAddNew.Text = "Add";
            this.buttonAddNew.UseVisualStyleBackColor = true;
            this.buttonAddNew.Click += new System.EventHandler(this.buttonAddNew_Click);
            // 
            // checkBoxRequiresUsername
            // 
            this.checkBoxRequiresUsername.AutoSize = true;
            this.checkBoxRequiresUsername.Location = new System.Drawing.Point(21, 73);
            this.checkBoxRequiresUsername.Name = "checkBoxRequiresUsername";
            this.checkBoxRequiresUsername.Size = new System.Drawing.Size(189, 17);
            this.checkBoxRequiresUsername.TabIndex = 4;
            this.checkBoxRequiresUsername.Text = "Requires Username and Password";
            this.checkBoxRequiresUsername.UseVisualStyleBackColor = true;
            this.checkBoxRequiresUsername.CheckedChanged += new System.EventHandler(this.checkBoxRequiresUsername_CheckedChanged);
            // 
            // dataGridViewSource
            // 
            this.dataGridViewSource.AllowUserToAddRows = false;
            this.dataGridViewSource.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewSource.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewSource.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridViewSource.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSource.Location = new System.Drawing.Point(20, 178);
            this.dataGridViewSource.MultiSelect = false;
            this.dataGridViewSource.Name = "dataGridViewSource";
            this.dataGridViewSource.ReadOnly = true;
            this.dataGridViewSource.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewSource.Size = new System.Drawing.Size(489, 184);
            this.dataGridViewSource.TabIndex = 4;
            this.dataGridViewSource.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dataGridViewSource_MouseClick);
            // 
            // labelHttpAddress
            // 
            this.labelHttpAddress.AutoSize = true;
            this.labelHttpAddress.Location = new System.Drawing.Point(21, 41);
            this.labelHttpAddress.Name = "labelHttpAddress";
            this.labelHttpAddress.Size = new System.Drawing.Size(71, 13);
            this.labelHttpAddress.TabIndex = 5;
            this.labelHttpAddress.Text = "Http Address:";
            // 
            // textBoxHttpAddress
            // 
            this.textBoxHttpAddress.Location = new System.Drawing.Point(97, 38);
            this.textBoxHttpAddress.Name = "textBoxHttpAddress";
            this.textBoxHttpAddress.Size = new System.Drawing.Size(408, 20);
            this.textBoxHttpAddress.TabIndex = 3;
            // 
            // textBoxPriority
            // 
            this.textBoxPriority.Location = new System.Drawing.Point(430, 6);
            this.textBoxPriority.MaxLength = 3;
            this.textBoxPriority.Name = "textBoxPriority";
            this.textBoxPriority.Size = new System.Drawing.Size(75, 20);
            this.textBoxPriority.TabIndex = 2;
            // 
            // labelPriority
            // 
            this.labelPriority.AutoSize = true;
            this.labelPriority.Location = new System.Drawing.Point(384, 9);
            this.labelPriority.Name = "labelPriority";
            this.labelPriority.Size = new System.Drawing.Size(41, 13);
            this.labelPriority.TabIndex = 7;
            this.labelPriority.Text = "Priority:";
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Enabled = false;
            this.textBoxPassword.Location = new System.Drawing.Point(97, 136);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(408, 20);
            this.textBoxPassword.TabIndex = 6;
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(21, 139);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(56, 13);
            this.labelPassword.TabIndex = 11;
            this.labelPassword.Text = "Password:";
            // 
            // textBoxUsername
            // 
            this.textBoxUsername.Enabled = false;
            this.textBoxUsername.Location = new System.Drawing.Point(97, 104);
            this.textBoxUsername.Name = "textBoxUsername";
            this.textBoxUsername.Size = new System.Drawing.Size(408, 20);
            this.textBoxUsername.TabIndex = 5;
            // 
            // labelUsername
            // 
            this.labelUsername.AutoSize = true;
            this.labelUsername.Location = new System.Drawing.Point(21, 107);
            this.labelUsername.Name = "labelUsername";
            this.labelUsername.Size = new System.Drawing.Size(58, 13);
            this.labelUsername.TabIndex = 9;
            this.labelUsername.Text = "Username:";
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(214, 373);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(75, 23);
            this.buttonClear.TabIndex = 8;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // buttonRemove
            // 
            this.buttonRemove.Location = new System.Drawing.Point(309, 373);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(75, 23);
            this.buttonRemove.TabIndex = 9;
            this.buttonRemove.Text = "Remove";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(404, 373);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 10;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // FormSource
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(529, 406);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonRemove);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.labelPassword);
            this.Controls.Add(this.textBoxUsername);
            this.Controls.Add(this.labelUsername);
            this.Controls.Add(this.textBoxPriority);
            this.Controls.Add(this.labelPriority);
            this.Controls.Add(this.textBoxHttpAddress);
            this.Controls.Add(this.labelHttpAddress);
            this.Controls.Add(this.dataGridViewSource);
            this.Controls.Add(this.checkBoxRequiresUsername);
            this.Controls.Add(this.buttonAddNew);
            this.Controls.Add(this.textBoxAlias);
            this.Controls.Add(this.labelAlias);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSource";
            this.Text = "Source Settings";
            this.Load += new System.EventHandler(this.FormSource_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelAlias;
        private System.Windows.Forms.Button buttonAddNew;
        private System.Windows.Forms.CheckBox checkBoxRequiresUsername;
        private System.Windows.Forms.DataGridView dataGridViewSource;
        private System.Windows.Forms.Label labelHttpAddress;
        private System.Windows.Forms.TextBox textBoxHttpAddress;
        private System.Windows.Forms.TextBox textBoxPriority;
        private System.Windows.Forms.Label labelPriority;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.Label labelUsername;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonClose;
        internal System.Windows.Forms.TextBox textBoxAlias;
    }
}