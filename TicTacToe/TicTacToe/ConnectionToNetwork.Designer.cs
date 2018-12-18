namespace TicTacToe
{
    partial class ConnectionToNetwork
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
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtLocalIP = new System.Windows.Forms.TextBox();
            this.txtFriendsIP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbLocalPort = new System.Windows.Forms.ComboBox();
            this.cmbFriendsPort = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnConnect.Location = new System.Drawing.Point(157, 91);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(83, 46);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "CONNECT";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtLocalIP
            // 
            this.txtLocalIP.Enabled = false;
            this.txtLocalIP.Location = new System.Drawing.Point(87, 12);
            this.txtLocalIP.Name = "txtLocalIP";
            this.txtLocalIP.Size = new System.Drawing.Size(197, 20);
            this.txtLocalIP.TabIndex = 1;
            // 
            // txtFriendsIP
            // 
            this.txtFriendsIP.Location = new System.Drawing.Point(87, 45);
            this.txtFriendsIP.Name = "txtFriendsIP";
            this.txtFriendsIP.Size = new System.Drawing.Size(197, 20);
            this.txtFriendsIP.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Local IP :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Friend\'s IP :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 124);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Friend\'s Port :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Local Port :";
            // 
            // cmbLocalPort
            // 
            this.cmbLocalPort.FormattingEnabled = true;
            this.cmbLocalPort.Items.AddRange(new object[] {
            "20",
            "21"});
            this.cmbLocalPort.Location = new System.Drawing.Point(87, 88);
            this.cmbLocalPort.Name = "cmbLocalPort";
            this.cmbLocalPort.Size = new System.Drawing.Size(54, 21);
            this.cmbLocalPort.TabIndex = 9;
            // 
            // cmbFriendsPort
            // 
            this.cmbFriendsPort.FormattingEnabled = true;
            this.cmbFriendsPort.Items.AddRange(new object[] {
            "20",
            "21"});
            this.cmbFriendsPort.Location = new System.Drawing.Point(87, 121);
            this.cmbFriendsPort.Name = "cmbFriendsPort";
            this.cmbFriendsPort.Size = new System.Drawing.Size(54, 21);
            this.cmbFriendsPort.TabIndex = 10;
            // 
            // ConnectionToNetwork
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 158);
            this.Controls.Add(this.cmbFriendsPort);
            this.Controls.Add(this.cmbLocalPort);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtFriendsIP);
            this.Controls.Add(this.txtLocalIP);
            this.Controls.Add(this.btnConnect);
            this.Name = "ConnectionToNetwork";
            this.Text = "Connection";
            this.Load += new System.EventHandler(this.ConnectionToNetwork_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtLocalIP;
        private System.Windows.Forms.TextBox txtFriendsIP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbLocalPort;
        private System.Windows.Forms.ComboBox cmbFriendsPort;
    }
}