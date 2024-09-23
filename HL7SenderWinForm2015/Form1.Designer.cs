namespace HL7SenderWinForm2015
{
    partial class Form1
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
            this.txtHL7 = new System.Windows.Forms.RichTextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.lblHostname = new System.Windows.Forms.Label();
            this.txtHostname = new System.Windows.Forms.TextBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.rdbtnOneWaySSL = new System.Windows.Forms.RadioButton();
            this.rdbtnTwoWaySSL = new System.Windows.Forms.RadioButton();
            this.grpBxSSL = new System.Windows.Forms.GroupBox();
            this.chkBxSSL = new System.Windows.Forms.CheckBox();
            this.ddlstCertDir = new System.Windows.Forms.ComboBox();
            this.lblCertDir = new System.Windows.Forms.Label();
            this.grpBxLogPath = new System.Windows.Forms.GroupBox();
            this.lblLogPath = new System.Windows.Forms.Label();
            this.txtLogPath = new System.Windows.Forms.TextBox();
            this.rdbtnDefLogPath = new System.Windows.Forms.RadioButton();
            this.rdbtnCustLogPath = new System.Windows.Forms.RadioButton();
            this.lblThumbprint = new System.Windows.Forms.Label();
            this.txtThumbprint = new System.Windows.Forms.TextBox();
            this.lblSRC = new System.Windows.Forms.Label();
            this.txtSRC = new System.Windows.Forms.TextBox();
            this.lblIPIndex = new System.Windows.Forms.Label();
            this.txtIPIndex = new System.Windows.Forms.TextBox();
            this.grpBxCertDetails = new System.Windows.Forms.GroupBox();
            this.grpBxSSL.SuspendLayout();
            this.grpBxLogPath.SuspendLayout();
            this.grpBxCertDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtHL7
            // 
            this.txtHL7.Location = new System.Drawing.Point(0, 0);
            this.txtHL7.Margin = new System.Windows.Forms.Padding(2);
            this.txtHL7.Name = "txtHL7";
            this.txtHL7.Size = new System.Drawing.Size(896, 691);
            this.txtHL7.TabIndex = 0;
            this.txtHL7.Text = "";
            // 
            // btnSend
            // 
            this.btnSend.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnSend.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSend.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnSend.Location = new System.Drawing.Point(929, 23);
            this.btnSend.Margin = new System.Windows.Forms.Padding(2);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(91, 27);
            this.btnSend.TabIndex = 1;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = false;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // lblHostname
            // 
            this.lblHostname.AutoSize = true;
            this.lblHostname.Location = new System.Drawing.Point(926, 66);
            this.lblHostname.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblHostname.Name = "lblHostname";
            this.lblHostname.Size = new System.Drawing.Size(138, 13);
            this.lblHostname.TabIndex = 2;
            this.lblHostname.Text = "Host Name [Provide FQDN]";
            // 
            // txtHostname
            // 
            this.txtHostname.Location = new System.Drawing.Point(928, 82);
            this.txtHostname.Margin = new System.Windows.Forms.Padding(2);
            this.txtHostname.Name = "txtHostname";
            this.txtHostname.Size = new System.Drawing.Size(209, 20);
            this.txtHostname.TabIndex = 3;
            this.txtHostname.Text = "G07SGXNFAP45200.g07.fujitsu.local";
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(926, 126);
            this.lblPort.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(96, 13);
            this.lblPort.TabIndex = 4;
            this.lblPort.Text = "Port [Sending Port]";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(928, 142);
            this.txtPort.Margin = new System.Windows.Forms.Padding(2);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(209, 20);
            this.txtPort.TabIndex = 5;
            this.txtPort.Text = "57207";
            this.txtPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPort_KeyPress);
            // 
            // rdbtnOneWaySSL
            // 
            this.rdbtnOneWaySSL.AutoSize = true;
            this.rdbtnOneWaySSL.Location = new System.Drawing.Point(5, 17);
            this.rdbtnOneWaySSL.Margin = new System.Windows.Forms.Padding(2);
            this.rdbtnOneWaySSL.Name = "rdbtnOneWaySSL";
            this.rdbtnOneWaySSL.Size = new System.Drawing.Size(93, 17);
            this.rdbtnOneWaySSL.TabIndex = 6;
            this.rdbtnOneWaySSL.Text = "One Way SSL";
            this.rdbtnOneWaySSL.UseVisualStyleBackColor = true;
            this.rdbtnOneWaySSL.CheckedChanged += new System.EventHandler(this.rdbtnSSL_CheckedChanged);
            // 
            // rdbtnTwoWaySSL
            // 
            this.rdbtnTwoWaySSL.AutoSize = true;
            this.rdbtnTwoWaySSL.Checked = true;
            this.rdbtnTwoWaySSL.Location = new System.Drawing.Point(5, 38);
            this.rdbtnTwoWaySSL.Margin = new System.Windows.Forms.Padding(2);
            this.rdbtnTwoWaySSL.Name = "rdbtnTwoWaySSL";
            this.rdbtnTwoWaySSL.Size = new System.Drawing.Size(94, 17);
            this.rdbtnTwoWaySSL.TabIndex = 7;
            this.rdbtnTwoWaySSL.TabStop = true;
            this.rdbtnTwoWaySSL.Text = "Two Way SSL";
            this.rdbtnTwoWaySSL.UseVisualStyleBackColor = true;
            // 
            // grpBxSSL
            // 
            this.grpBxSSL.Controls.Add(this.rdbtnOneWaySSL);
            this.grpBxSSL.Controls.Add(this.rdbtnTwoWaySSL);
            this.grpBxSSL.Location = new System.Drawing.Point(928, 470);
            this.grpBxSSL.Margin = new System.Windows.Forms.Padding(2);
            this.grpBxSSL.Name = "grpBxSSL";
            this.grpBxSSL.Padding = new System.Windows.Forms.Padding(2);
            this.grpBxSSL.Size = new System.Drawing.Size(208, 81);
            this.grpBxSSL.TabIndex = 8;
            this.grpBxSSL.TabStop = false;
            this.grpBxSSL.Text = "SSL Type";
            // 
            // chkBxSSL
            // 
            this.chkBxSSL.AutoSize = true;
            this.chkBxSSL.Checked = true;
            this.chkBxSSL.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBxSSL.Location = new System.Drawing.Point(930, 439);
            this.chkBxSSL.Margin = new System.Windows.Forms.Padding(2);
            this.chkBxSSL.Name = "chkBxSSL";
            this.chkBxSSL.Size = new System.Drawing.Size(46, 17);
            this.chkBxSSL.TabIndex = 9;
            this.chkBxSSL.Text = "SSL";
            this.chkBxSSL.UseVisualStyleBackColor = true;
            this.chkBxSSL.CheckedChanged += new System.EventHandler(this.chkBxSSL_CheckedChanged);
            // 
            // ddlstCertDir
            // 
            this.ddlstCertDir.DisplayMember = "(none)";
            this.ddlstCertDir.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlstCertDir.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ddlstCertDir.FormattingEnabled = true;
            this.ddlstCertDir.Items.AddRange(new object[] {
            "Publisher",
            "Personal",
            "Root"});
            this.ddlstCertDir.Location = new System.Drawing.Point(3, 31);
            this.ddlstCertDir.Margin = new System.Windows.Forms.Padding(2);
            this.ddlstCertDir.Name = "ddlstCertDir";
            this.ddlstCertDir.Size = new System.Drawing.Size(204, 21);
            this.ddlstCertDir.TabIndex = 10;
            // 
            // lblCertDir
            // 
            this.lblCertDir.AutoSize = true;
            this.lblCertDir.Location = new System.Drawing.Point(0, 16);
            this.lblCertDir.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCertDir.Name = "lblCertDir";
            this.lblCertDir.Size = new System.Drawing.Size(49, 13);
            this.lblCertDir.TabIndex = 11;
            this.lblCertDir.Text = "Directory";
            // 
            // grpBxLogPath
            // 
            this.grpBxLogPath.Controls.Add(this.lblLogPath);
            this.grpBxLogPath.Controls.Add(this.txtLogPath);
            this.grpBxLogPath.Controls.Add(this.rdbtnDefLogPath);
            this.grpBxLogPath.Controls.Add(this.rdbtnCustLogPath);
            this.grpBxLogPath.Location = new System.Drawing.Point(928, 302);
            this.grpBxLogPath.Margin = new System.Windows.Forms.Padding(2);
            this.grpBxLogPath.Name = "grpBxLogPath";
            this.grpBxLogPath.Padding = new System.Windows.Forms.Padding(2);
            this.grpBxLogPath.Size = new System.Drawing.Size(208, 128);
            this.grpBxLogPath.TabIndex = 9;
            this.grpBxLogPath.TabStop = false;
            this.grpBxLogPath.Text = "Log Path";
            // 
            // lblLogPath
            // 
            this.lblLogPath.AutoSize = true;
            this.lblLogPath.Location = new System.Drawing.Point(1, 70);
            this.lblLogPath.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLogPath.Name = "lblLogPath";
            this.lblLogPath.Size = new System.Drawing.Size(88, 13);
            this.lblLogPath.TabIndex = 12;
            this.lblLogPath.Text = "Custom Log Path";
            // 
            // txtLogPath
            // 
            this.txtLogPath.Location = new System.Drawing.Point(0, 85);
            this.txtLogPath.Margin = new System.Windows.Forms.Padding(2);
            this.txtLogPath.Name = "txtLogPath";
            this.txtLogPath.Size = new System.Drawing.Size(208, 20);
            this.txtLogPath.TabIndex = 12;
            this.txtLogPath.Text = "..\\Path";
            // 
            // rdbtnDefLogPath
            // 
            this.rdbtnDefLogPath.AutoSize = true;
            this.rdbtnDefLogPath.Checked = true;
            this.rdbtnDefLogPath.Location = new System.Drawing.Point(5, 17);
            this.rdbtnDefLogPath.Margin = new System.Windows.Forms.Padding(2);
            this.rdbtnDefLogPath.Name = "rdbtnDefLogPath";
            this.rdbtnDefLogPath.Size = new System.Drawing.Size(84, 17);
            this.rdbtnDefLogPath.TabIndex = 6;
            this.rdbtnDefLogPath.TabStop = true;
            this.rdbtnDefLogPath.Text = "Default Path";
            this.rdbtnDefLogPath.UseVisualStyleBackColor = true;
            this.rdbtnDefLogPath.CheckedChanged += new System.EventHandler(this.rdbtnLog_CheckedChanged);
            // 
            // rdbtnCustLogPath
            // 
            this.rdbtnCustLogPath.AutoSize = true;
            this.rdbtnCustLogPath.Location = new System.Drawing.Point(5, 38);
            this.rdbtnCustLogPath.Margin = new System.Windows.Forms.Padding(2);
            this.rdbtnCustLogPath.Name = "rdbtnCustLogPath";
            this.rdbtnCustLogPath.Size = new System.Drawing.Size(85, 17);
            this.rdbtnCustLogPath.TabIndex = 7;
            this.rdbtnCustLogPath.TabStop = true;
            this.rdbtnCustLogPath.Text = "Custom Path";
            this.rdbtnCustLogPath.UseVisualStyleBackColor = true;
            this.rdbtnCustLogPath.CheckedChanged += new System.EventHandler(this.rdbtnLog_CheckedChanged);
            // 
            // lblThumbprint
            // 
            this.lblThumbprint.AutoSize = true;
            this.lblThumbprint.Location = new System.Drawing.Point(1, 68);
            this.lblThumbprint.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblThumbprint.Name = "lblThumbprint";
            this.lblThumbprint.Size = new System.Drawing.Size(64, 13);
            this.lblThumbprint.TabIndex = 12;
            this.lblThumbprint.Text = "Thumb Print";
            // 
            // txtThumbprint
            // 
            this.txtThumbprint.Location = new System.Drawing.Point(3, 84);
            this.txtThumbprint.Margin = new System.Windows.Forms.Padding(2);
            this.txtThumbprint.Name = "txtThumbprint";
            this.txtThumbprint.Size = new System.Drawing.Size(204, 20);
            this.txtThumbprint.TabIndex = 13;
            this.txtThumbprint.Text = "ae744a9aad1d0b58c2b01ce00557281bd2f33938";
            // 
            // lblSRC
            // 
            this.lblSRC.AutoSize = true;
            this.lblSRC.Location = new System.Drawing.Point(926, 190);
            this.lblSRC.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSRC.Name = "lblSRC";
            this.lblSRC.Size = new System.Drawing.Size(168, 13);
            this.lblSRC.TabIndex = 14;
            this.lblSRC.Text = "IP Address [UAT: tcp://ipaddress]";
            // 
            // txtSRC
            // 
            this.txtSRC.Location = new System.Drawing.Point(928, 205);
            this.txtSRC.Margin = new System.Windows.Forms.Padding(2);
            this.txtSRC.Name = "txtSRC";
            this.txtSRC.Size = new System.Drawing.Size(209, 20);
            this.txtSRC.TabIndex = 15;
            this.txtSRC.Text = "tcp://localhost";
            // 
            // lblIPIndex
            // 
            this.lblIPIndex.AutoSize = true;
            this.lblIPIndex.Location = new System.Drawing.Point(926, 247);
            this.lblIPIndex.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblIPIndex.Name = "lblIPIndex";
            this.lblIPIndex.Size = new System.Drawing.Size(133, 13);
            this.lblIPIndex.TabIndex = 16;
            this.lblIPIndex.Text = "IP Address Index [UAT : 0]";
            // 
            // txtIPIndex
            // 
            this.txtIPIndex.Location = new System.Drawing.Point(929, 262);
            this.txtIPIndex.Margin = new System.Windows.Forms.Padding(2);
            this.txtIPIndex.Name = "txtIPIndex";
            this.txtIPIndex.Size = new System.Drawing.Size(209, 20);
            this.txtIPIndex.TabIndex = 17;
            this.txtIPIndex.Text = "1";
            this.txtIPIndex.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPort_KeyPress);
            // 
            // grpBxCertDetails
            // 
            this.grpBxCertDetails.Controls.Add(this.lblCertDir);
            this.grpBxCertDetails.Controls.Add(this.ddlstCertDir);
            this.grpBxCertDetails.Controls.Add(this.lblThumbprint);
            this.grpBxCertDetails.Controls.Add(this.txtThumbprint);
            this.grpBxCertDetails.Location = new System.Drawing.Point(929, 569);
            this.grpBxCertDetails.Name = "grpBxCertDetails";
            this.grpBxCertDetails.Size = new System.Drawing.Size(208, 122);
            this.grpBxCertDetails.TabIndex = 18;
            this.grpBxCertDetails.TabStop = false;
            this.grpBxCertDetails.Text = "Certificate Details";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1145, 718);
            this.Controls.Add(this.grpBxCertDetails);
            this.Controls.Add(this.txtIPIndex);
            this.Controls.Add(this.lblIPIndex);
            this.Controls.Add(this.txtSRC);
            this.Controls.Add(this.lblSRC);
            this.Controls.Add(this.grpBxLogPath);
            this.Controls.Add(this.chkBxSSL);
            this.Controls.Add(this.grpBxSSL);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.lblPort);
            this.Controls.Add(this.txtHostname);
            this.Controls.Add(this.lblHostname);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtHL7);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.grpBxSSL.ResumeLayout(false);
            this.grpBxSSL.PerformLayout();
            this.grpBxLogPath.ResumeLayout(false);
            this.grpBxLogPath.PerformLayout();
            this.grpBxCertDetails.ResumeLayout(false);
            this.grpBxCertDetails.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtHL7;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Label lblHostname;
        private System.Windows.Forms.TextBox txtHostname;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.RadioButton rdbtnOneWaySSL;
        private System.Windows.Forms.RadioButton rdbtnTwoWaySSL;
        private System.Windows.Forms.GroupBox grpBxSSL;
        private System.Windows.Forms.CheckBox chkBxSSL;
        private System.Windows.Forms.ComboBox ddlstCertDir;
        private System.Windows.Forms.Label lblCertDir;
        private System.Windows.Forms.GroupBox grpBxLogPath;
        private System.Windows.Forms.RadioButton rdbtnDefLogPath;
        private System.Windows.Forms.RadioButton rdbtnCustLogPath;
        private System.Windows.Forms.Label lblLogPath;
        private System.Windows.Forms.TextBox txtLogPath;
        private System.Windows.Forms.Label lblThumbprint;
        private System.Windows.Forms.TextBox txtThumbprint;
        private System.Windows.Forms.Label lblSRC;
        private System.Windows.Forms.TextBox txtSRC;
        private System.Windows.Forms.Label lblIPIndex;
        private System.Windows.Forms.TextBox txtIPIndex;
        private System.Windows.Forms.GroupBox grpBxCertDetails;
    }
}

