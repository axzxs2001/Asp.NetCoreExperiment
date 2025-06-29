namespace SmartWinForms
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
            aiChat1 = new AIChat();
            SuspendLayout();
            // 
            // aiChat1
            // 
            aiChat1.DefaultModelId = "phi4-mini";
            aiChat1.Dock = DockStyle.Fill;
            aiChat1.Location = new Point(0, 0);
            aiChat1.Name = "aiChat1";
            aiChat1.Size = new Size(1217, 890);
            aiChat1.SystemPrompt = "你是一个AI助理，用简练的语言回答问题";
            aiChat1.TabIndex = 0;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1217, 890);
            Controls.Add(aiChat1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
        }

        #endregion

        private AIChat aiChat1;
    }
}
