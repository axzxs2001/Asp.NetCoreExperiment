namespace WinForm_GPTDemo
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;


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
            name_display_Lab = new Label();
            name_Lab = new Label();
            age_display_Lab = new Label();
            height_display_Lab = new Label();
            weight_display_Lab = new Label();
            age_Lab = new Label();
            height_Lab = new Label();
            weight_Lab = new Label();
            SuspendLayout();
            // 
            // name_display_Lab
            // 
            name_display_Lab.AutoSize = true;
            name_display_Lab.Location = new Point(55, 47);
            name_display_Lab.Name = "name_display_Lab";
            name_display_Lab.Size = new Size(44, 17);
            name_display_Lab.TabIndex = 0;
            name_display_Lab.Text = "名称：";
            // 
            // name_Lab
            // 
            name_Lab.AutoSize = true;
            name_Lab.Location = new Point(112, 49);
            name_Lab.Name = "name_Lab";
            name_Lab.Size = new Size(40, 17);
            name_Lab.TabIndex = 1;
            name_Lab.Text = "name";
            // 
            // age_display_Lab
            // 
            age_display_Lab.AutoSize = true;
            age_display_Lab.Location = new Point(55, 158);
            age_display_Lab.Name = "age_display_Lab";
            age_display_Lab.Size = new Size(44, 17);
            age_display_Lab.TabIndex = 2;
            age_display_Lab.Text = "年龄：";
            // 
            // height_display_Lab
            // 
            height_display_Lab.AutoSize = true;
            height_display_Lab.Location = new Point(258, 100);
            height_display_Lab.Name = "height_display_Lab";
            height_display_Lab.Size = new Size(44, 17);
            height_display_Lab.TabIndex = 3;
            height_display_Lab.Text = "身高：";
            // 
            // weight_display_Lab
            // 
            weight_display_Lab.AutoSize = true;
            weight_display_Lab.Location = new Point(55, 100);
            weight_display_Lab.Name = "weight_display_Lab";
            weight_display_Lab.Size = new Size(44, 17);
            weight_display_Lab.TabIndex = 4;
            weight_display_Lab.Text = "体重：";
            // 
            // age_Lab
            // 
            age_Lab.AutoSize = true;
            age_Lab.Location = new Point(92, 158);
            age_Lab.Name = "age_Lab";
            age_Lab.Size = new Size(30, 17);
            age_Lab.TabIndex = 5;
            age_Lab.Text = "age";
            // 
            // height_Lab
            // 
            height_Lab.AutoSize = true;
            height_Lab.Location = new Point(308, 100);
            height_Lab.Name = "height_Lab";
            height_Lab.Size = new Size(44, 17);
            height_Lab.TabIndex = 6;
            height_Lab.Text = "height";
            // 
            // weight_Lab
            // 
            weight_Lab.AutoSize = true;
            weight_Lab.Location = new Point(106, 100);
            weight_Lab.Name = "weight_Lab";
            weight_Lab.Size = new Size(46, 17);
            weight_Lab.TabIndex = 7;
            weight_Lab.Text = "weight";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(age_display_Lab);
            Controls.Add(height_display_Lab);
            Controls.Add(weight_display_Lab);
            Controls.Add(age_Lab);
            Controls.Add(height_Lab);
            Controls.Add(weight_Lab);
            Controls.Add(name_display_Lab);
            Controls.Add(name_Lab);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label name_display_Lab;
        private Label name_Lab;

        private Label age_display_Lab;
        private Label height_display_Lab;
        private Label weight_display_Lab;

        private Label age_Lab;
        private Label height_Lab;
        private Label weight_Lab;

    }
}