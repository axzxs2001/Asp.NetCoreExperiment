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
            this.dbCombox1 = new ClientDemo01.DBCombox();
            this.SuspendLayout();
            // 
            // dbCombox1
            // 
          
            this.dbCombox1.Location = new System.Drawing.Point(392, 236);
            this.dbCombox1.Name = "dbCombox1";
            this.dbCombox1.Size = new System.Drawing.Size(148, 25);
            this.dbCombox1.TabIndex = 1;     
            this.dbCombox1.Url = "http://localhost:5235/kv/";
            this.dbCombox1.TableName = "type";
            this.dbCombox1.DisplayMember = "Name";
            this.dbCombox1.ValueMember = "ID";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dbCombox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private DBCombox dbCombox1;
    }
}