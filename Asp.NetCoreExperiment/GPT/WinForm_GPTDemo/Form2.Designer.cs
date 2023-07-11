using System;

namespace WinForm_GPTDemo
{
    partial class Form2
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
            lbl_title_name = new Label();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            lbl_title_age = new Label();
            SuspendLayout();
            // 
            // lbl_title_name
            // 
            lbl_title_name.Location = new Point(20, 20);
            lbl_title_name.Name = "lbl_title_name";
            lbl_title_name.Size = new Size(100, 23);
            lbl_title_name.TabIndex = 0;
            lbl_title_name.Text = "姓名";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(150, 20); // Changed the location
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(100, 23);
            textBox1.TabIndex = 4;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(150, 60); // Changed the location
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(100, 23);
            textBox2.TabIndex = 5;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(150, 100); // Changed the location
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(100, 23);
            textBox3.TabIndex = 5;
            // 
            // lbl_title_age
            // 
            lbl_title_age.Location = new Point(20, 60); // Changed the location
            lbl_title_age.Name = "lbl_title_age";
            lbl_title_age.Size = new Size(100, 23);
            lbl_title_age.TabIndex = 2;
            lbl_title_age.Text = "年龄";
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(701, 212);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(lbl_title_name);
            Controls.Add(lbl_title_age);
            Name = "Form2";
            Text = "Form2";
            Load += Form2_Load;
            ResumeLayout(false);
            PerformLayout();
        }


        private Label lbl_title_name;
        private Label lbl_title_age;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        #endregion


    }

}