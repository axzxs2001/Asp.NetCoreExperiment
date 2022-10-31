namespace WinFormsDemo14
{
    partial class LogForm
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
            this.LogRichTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // LogRichTextBox
            // 
            this.LogRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogRichTextBox.Location = new System.Drawing.Point(0, 0);
            this.LogRichTextBox.Name = "LogRichTextBox";
            this.LogRichTextBox.Size = new System.Drawing.Size(979, 1069);
            this.LogRichTextBox.TabIndex = 2;
            this.LogRichTextBox.Text = "";
            // 
            // LogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(979, 1069);
            this.Controls.Add(this.LogRichTextBox);
            this.Name = "LogForm";
            this.Text = "LogForm";
            this.Load += new System.EventHandler(this.LogForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private RichTextBox LogRichTextBox;
    }
}