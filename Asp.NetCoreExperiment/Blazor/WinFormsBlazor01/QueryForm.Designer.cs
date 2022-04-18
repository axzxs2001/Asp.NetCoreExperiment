namespace WinFormsBlazor01
{
    partial class QueryForm
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
            this.queryBlazorWebView = new Microsoft.AspNetCore.Components.WebView.WindowsForms.BlazorWebView();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // queryBlazorWebView
            // 
            this.queryBlazorWebView.Dock = System.Windows.Forms.DockStyle.Top;
            this.queryBlazorWebView.Location = new System.Drawing.Point(0, 0);
            this.queryBlazorWebView.Name = "queryBlazorWebView";
            this.queryBlazorWebView.Size = new System.Drawing.Size(800, 391);
            this.queryBlazorWebView.TabIndex = 0;
            this.queryBlazorWebView.Text = "queryBlazorWebView";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(639, 404);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(149, 34);
            this.button1.TabIndex = 1;
            this.button1.Text = "AddForm";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.queryBlazorWebView);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.AspNetCore.Components.WebView.WindowsForms.BlazorWebView queryBlazorWebView;
        private Button button1;
    }
}