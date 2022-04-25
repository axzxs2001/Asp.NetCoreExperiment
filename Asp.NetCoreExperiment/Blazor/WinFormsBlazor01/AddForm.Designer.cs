namespace WinFormsBlazor01
{
    partial class AddForm
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
            this.addBlazorWebView = new Microsoft.AspNetCore.Components.WebView.WindowsForms.BlazorWebView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtNo = new System.Windows.Forms.TextBox();
            this.labBackMessage = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labMessage = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // addBlazorWebView
            // 
            this.addBlazorWebView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addBlazorWebView.Location = new System.Drawing.Point(0, 176);
            this.addBlazorWebView.Name = "addBlazorWebView";
            this.addBlazorWebView.Size = new System.Drawing.Size(967, 414);
            this.addBlazorWebView.TabIndex = 1;
            this.addBlazorWebView.Text = "queryBlazorWebView";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.txtNo);
            this.panel1.Controls.Add(this.labBackMessage);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.labMessage);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(967, 176);
            this.panel1.TabIndex = 2;
            // 
            // txtNo
            // 
            this.txtNo.Location = new System.Drawing.Point(423, 39);
            this.txtNo.Name = "txtNo";
            this.txtNo.Size = new System.Drawing.Size(362, 23);
            this.txtNo.TabIndex = 6;
            // 
            // labBackMessage
            // 
            this.labBackMessage.AutoSize = true;
            this.labBackMessage.Location = new System.Drawing.Point(423, 111);
            this.labBackMessage.Name = "labBackMessage";
            this.labBackMessage.Size = new System.Drawing.Size(73, 17);
            this.labBackMessage.TabIndex = 5;
            this.labBackMessage.Text = "-------------";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(198, 30);
            this.label1.TabIndex = 4;
            this.label1.Text = "我是WinForm窗体";
            // 
            // labMessage
            // 
            this.labMessage.AutoSize = true;
            this.labMessage.ForeColor = System.Drawing.Color.Red;
            this.labMessage.Location = new System.Drawing.Point(38, 91);
            this.labMessage.Name = "labMessage";
            this.labMessage.Size = new System.Drawing.Size(116, 17);
            this.labMessage.TabIndex = 3;
            this.labMessage.Text = "============";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(817, 82);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(121, 34);
            this.button1.TabIndex = 2;
            this.button1.Text = "触发Html事件";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(829, 30);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(109, 32);
            this.button2.TabIndex = 7;
            this.button2.Text = "Test";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // AddForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(967, 590);
            this.Controls.Add(this.addBlazorWebView);
            this.Controls.Add(this.panel1);
            this.Name = "AddForm";
            this.Text = "AddForm";
            this.Load += new System.EventHandler(this.AddForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.AspNetCore.Components.WebView.WindowsForms.BlazorWebView addBlazorWebView;
        private Panel panel1;
        private Button button1;
        private Label labMessage;
        private Label label1;
        private Label labBackMessage;
        private TextBox txtNo;
        private Button button2;
    }
}