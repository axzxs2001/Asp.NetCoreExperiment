namespace ClientDemo01
{
    partial class Form1
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
            this.dbListBox1 = new ClientDemo01.DBListBox();
            this.dbCombox1 = new ClientDemo01.DBCombox();
            this.SuspendLayout();
            // 
            // dbListBox1
            // 
            this.dbListBox1.DisplayMember = "Name";
            this.dbListBox1.FormattingEnabled = true;
            this.dbListBox1.ItemHeight = 24;
            this.dbListBox1.Location = new System.Drawing.Point(223, 143);
            this.dbListBox1.Name = "dbListBox1";
            this.dbListBox1.Size = new System.Drawing.Size(180, 124);
            this.dbListBox1.TabIndex = 0;
            this.dbListBox1.DataSourceName = "type";
            this.dbListBox1.Url = "http://localhost:5235/kv/";
            this.dbListBox1.ValueMember = "ID";
            // 
            // dbCombox1
            // 
            this.dbCombox1.DisplayMember = "Name";
            this.dbCombox1.FormattingEnabled = true;
            this.dbCombox1.Location = new System.Drawing.Point(556, 162);
            this.dbCombox1.Name = "dbCombox1";
            this.dbCombox1.Size = new System.Drawing.Size(182, 32);
            this.dbCombox1.TabIndex = 1;
            this.dbCombox1.DataSourceName = "type";
            this.dbCombox1.Url = "http://localhost:5235/kv/";
            this.dbCombox1.ValueMember = "ID";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1257, 635);
            this.Controls.Add(this.dbCombox1);
            this.Controls.Add(this.dbListBox1);
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        private DBListBox dbListBox1;
        private DBCombox dbCombox1;

        #endregion
        //private DBCombox dbCombox1;
    }
}