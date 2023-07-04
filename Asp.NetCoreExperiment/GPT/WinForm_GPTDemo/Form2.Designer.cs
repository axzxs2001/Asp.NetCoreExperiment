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
            SuspendLayout();
            // 标题标签 "姓名"
            lbl_title_name = new Label();
            lbl_title_name.Name = "lbl_title_name";
            lbl_title_name.Text = "姓名";
            lbl_title_name.Location = new Point(20, 20); // 这里
            this.Controls.Add(lbl_title_name); // 设定标签的位置，可以根据需要进行
                                               // 内容标签 "姓名"
            lbl_name = new Label();
            lbl_name.Name = "lbl_name";
            lbl_name.Text = ""; // 这里暂时设为空，内容将在其他地方设置
            lbl_name.Location = new Point(80, 20); // 这里设定标签的位置，可以根据需要进行调整

            // 标题标签 "年龄"
            lbl_title_age = new Label();
            lbl_title_age.Name = "lbl_title_age";
            lbl_title_age.Text = "年龄";
            lbl_title_age.Location = new Point(20, 50); // 这里设定标签的位置，可以根据需要进行调整

            // 内容标签 "年龄"
            lbl_age = new Label();
            lbl_age.Name = "lbl_age";
            lbl_age.Text = ""; // 这里暂时设为空，内容将在其他地方设置
            lbl_age.Location = new Point(80, 50); // 这里设定标签的位置，可以根据需要进行调整

            // 将标签添加到表单中

            this.Controls.Add(lbl_name);
            this.Controls.Add(lbl_title_age);
            this.Controls.Add(lbl_age);
            // 调整
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(701, 634);
            Name = "Form2";
            Text = "Form2";
            ResumeLayout(false);
        }

        private void CreateLabels()
        {



        }


        private Label lbl_title_name;
        private Label lbl_name;
        private Label lbl_title_age;
        private Label lbl_age;






        #endregion
    }
}