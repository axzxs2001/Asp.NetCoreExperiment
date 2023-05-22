using System.ComponentModel;

namespace WinForm_GPTDemo
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private IContainer components = null;


        //参照name_display_Lab和name_Lab，生成person各属性，并且要有Label的具本定义，位置横向排列




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
            psn_no_display_Lab = new Label();
            psn_no_Lab = new Label();
            SuspendLayout();
            // 
            // psn_no_display_Lab
            // 
            psn_no_display_Lab.AutoSize = true;
            psn_no_display_Lab.Location = new Point(39, 48);
            psn_no_display_Lab.Name = "psn_no_display_Lab";
            psn_no_display_Lab.Size = new Size(68, 17);
            psn_no_display_Lab.TabIndex = 0;
            psn_no_display_Lab.Text = "人员编号：";
            // 
            // psn_no_Lab
            // 
            psn_no_Lab.AutoSize = true;
            psn_no_Lab.Location = new Point(114, 47);
            psn_no_Lab.Name = "psn_no_Lab";
            psn_no_Lab.Size = new Size(49, 17);
            psn_no_Lab.TabIndex = 1;
            psn_no_Lab.Text = "psn_no";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(psn_no_Lab);
            Controls.Add(psn_no_display_Lab);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();

        }


        #endregion


        private Label psn_no_display_Lab;
        private Label psn_no_Lab;

    }


    //-----------------------------------------------------



}