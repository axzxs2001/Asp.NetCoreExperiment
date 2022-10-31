namespace WinFormsDemo15
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.IsServer = new System.Windows.Forms.CheckBox();
            this.ConnectionButton = new System.Windows.Forms.Button();
            this.PortTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.IPTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.YouTextBox = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.SendButton = new System.Windows.Forms.Button();
            this.MyTextBox = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.IsServer);
            this.panel1.Controls.Add(this.ConnectionButton);
            this.panel1.Controls.Add(this.PortTextBox);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.IPTextBox);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1179, 76);
            this.panel1.TabIndex = 0;
            // 
            // IsServer
            // 
            this.IsServer.AutoSize = true;
            this.IsServer.Location = new System.Drawing.Point(659, 21);
            this.IsServer.Name = "IsServer";
            this.IsServer.Size = new System.Drawing.Size(112, 35);
            this.IsServer.TabIndex = 5;
            this.IsServer.Text = "服务端";
            this.IsServer.UseVisualStyleBackColor = true;
            // 
            // ConnectionButton
            // 
            this.ConnectionButton.Location = new System.Drawing.Point(792, 18);
            this.ConnectionButton.Name = "ConnectionButton";
            this.ConnectionButton.Size = new System.Drawing.Size(129, 41);
            this.ConnectionButton.TabIndex = 4;
            this.ConnectionButton.Text = "连接";
            this.ConnectionButton.UseVisualStyleBackColor = true;
            this.ConnectionButton.Click += new System.EventHandler(this.ConnectionButton_Click);
            // 
            // PortTextBox
            // 
            this.PortTextBox.Location = new System.Drawing.Point(513, 20);
            this.PortTextBox.Name = "PortTextBox";
            this.PortTextBox.Size = new System.Drawing.Size(101, 38);
            this.PortTextBox.TabIndex = 3;
            this.PortTextBox.Text = " 8989";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(441, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 31);
            this.label2.TabIndex = 2;
            this.label2.Text = "Port：";
            // 
            // IPTextBox
            // 
            this.IPTextBox.Location = new System.Drawing.Point(67, 20);
            this.IPTextBox.Name = "IPTextBox";
            this.IPTextBox.Size = new System.Drawing.Size(326, 38);
            this.IPTextBox.TabIndex = 1;
            this.IPTextBox.Text = "127.0.0.1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP：";
            // 
            // YouTextBox
            // 
            this.YouTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.YouTextBox.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.YouTextBox.HideSelection = false;
            this.YouTextBox.Location = new System.Drawing.Point(0, 0);
            this.YouTextBox.Multiline = true;
            this.YouTextBox.Name = "YouTextBox";
            this.YouTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.YouTextBox.Size = new System.Drawing.Size(1179, 630);
            this.YouTextBox.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.SendButton);
            this.panel3.Controls.Add(this.MyTextBox);
            this.panel3.Controls.Add(this.YouTextBox);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.panel3.Location = new System.Drawing.Point(0, 76);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1179, 897);
            this.panel3.TabIndex = 2;
            // 
            // SendButton
            // 
            this.SendButton.Location = new System.Drawing.Point(1002, 830);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(155, 55);
            this.SendButton.TabIndex = 3;
            this.SendButton.Text = "发送";
            this.SendButton.UseVisualStyleBackColor = true;
            this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // MyTextBox
            // 
            this.MyTextBox.Location = new System.Drawing.Point(0, 636);
            this.MyTextBox.Multiline = true;
            this.MyTextBox.Name = "MyTextBox";
            this.MyTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.MyTextBox.Size = new System.Drawing.Size(1179, 188);
            this.MyTextBox.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1179, 973);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "聊天";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private TextBox PortTextBox;
        private Label label2;
        private TextBox IPTextBox;
        private Label label1;
        private TextBox YouTextBox;
        private Panel panel3;
        private Button SendButton;
        private TextBox MyTextBox;
        private CheckBox IsServer;
        private Button ConnectionButton;
    }
}