namespace Buttons
{
    partial class GetIPForm
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
            this.ownServer = new System.Windows.Forms.RadioButton();
            this.ipField = new System.Windows.Forms.TextBox();
            this.connectServer = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ownServer
            // 
            this.ownServer.AutoSize = true;
            this.ownServer.Location = new System.Drawing.Point(13, 13);
            this.ownServer.Name = "ownServer";
            this.ownServer.Size = new System.Drawing.Size(14, 13);
            this.ownServer.TabIndex = 0;
            this.ownServer.TabStop = true;
            this.ownServer.UseVisualStyleBackColor = true;
            this.ownServer.CheckedChanged += new System.EventHandler(this.ownServer_CheckedChanged);
            // 
            // ipField
            // 
            this.ipField.Location = new System.Drawing.Point(53, 57);
            this.ipField.Name = "ipField";
            this.ipField.Size = new System.Drawing.Size(211, 20);
            this.ipField.TabIndex = 1;
            // 
            // connectServer
            // 
            this.connectServer.AutoSize = true;
            this.connectServer.Location = new System.Drawing.Point(13, 34);
            this.connectServer.Name = "connectServer";
            this.connectServer.Size = new System.Drawing.Size(85, 17);
            this.connectServer.TabIndex = 2;
            this.connectServer.TabStop = true;
            this.connectServer.Text = "radioButton2";
            this.connectServer.UseVisualStyleBackColor = true;
            this.connectServer.CheckedChanged += new System.EventHandler(this.connectServer_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 83);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(251, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Ok";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // GetIPForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(276, 188);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.connectServer);
            this.Controls.Add(this.ipField);
            this.Controls.Add(this.ownServer);
            this.Name = "GetIPForm";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton ownServer;
        private System.Windows.Forms.TextBox ipField;
        private System.Windows.Forms.RadioButton connectServer;
        private System.Windows.Forms.Button button1;
    }
}