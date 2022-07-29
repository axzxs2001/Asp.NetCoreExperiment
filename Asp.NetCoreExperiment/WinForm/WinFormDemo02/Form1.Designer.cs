namespace WinFormDemo02
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
            this.switch1 = new WinFormDemo02.Switch();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // switch1
            // 
            this.switch1.Checked = false;
            this.switch1.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.switch1.Location = new System.Drawing.Point(74, 57);
            this.switch1.Name = "switch1";
            this.switch1.Size = new System.Drawing.Size(52, 28);
            this.switch1.TabIndex = 1;
            this.switch1.Text = "switch1";
            this.switch1.CheckedChanged += new System.EventHandler(this.switch1_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(150, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 24);
            this.label1.TabIndex = 2;
            this.label1.Text = "没选中";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 245);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.switch1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Switch switch1;
        private Label label1;
    }
}