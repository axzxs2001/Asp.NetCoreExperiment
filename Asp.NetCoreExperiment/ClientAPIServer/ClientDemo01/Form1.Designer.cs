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
            DBControl.DBCondition dbCondition1 = new DBControl.DBCondition();
            DBControl.DBCondition dbCondition2 = new DBControl.DBCondition();
            DBControl.DBCondition dbCondition3 = new DBControl.DBCondition();
            DBControl.DBCondition dbCondition4 = new DBControl.DBCondition();
            DBControl.DBCondition dbCondition5 = new DBControl.DBCondition();
            DBControl.DBCondition dbCondition6 = new DBControl.DBCondition();
            this.dbListBox1 = new DBControl.DBListBox();
            this.dbCombox1 = new DBControl.DBComBox();
            this.dataGridView1 = new DBControl.DBDataGridView();
            this.DGTBC_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DGTB_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DGTBC_Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DGTBC_Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dbListBox1
            // 
            dbCondition1.Name = "type";
            dbCondition1.Symbol = "=";
            dbCondition1.Value = "101";
            this.dbListBox1.Conditions.Add(dbCondition1);
            this.dbListBox1.DataSourceName = "city";
            this.dbListBox1.DisplayMember = "Value";
            this.dbListBox1.FormattingEnabled = true;
            this.dbListBox1.ItemHeight = 17;
            this.dbListBox1.Location = new System.Drawing.Point(135, 51);
            this.dbListBox1.Margin = new System.Windows.Forms.Padding(2);
            this.dbListBox1.Name = "dbListBox1";
            this.dbListBox1.Size = new System.Drawing.Size(116, 89);
            this.dbListBox1.TabIndex = 0;
            this.dbListBox1.Url = "http://localhost:5235/parame/";
            this.dbListBox1.ValueMember = "Key";
            // 
            // dbCombox1
            // 
            dbCondition2.Name = "type";
            dbCondition2.Symbol = "=";
            dbCondition2.Value = "2";
            this.dbCombox1.Conditions.Add(dbCondition2);
            this.dbCombox1.DataSourceName = "type";
            this.dbCombox1.DisplayMember = "Name";
            this.dbCombox1.FormattingEnabled = true;
            this.dbCombox1.Location = new System.Drawing.Point(135, 22);
            this.dbCombox1.Margin = new System.Windows.Forms.Padding(2);
            this.dbCombox1.Name = "dbCombox1";
            this.dbCombox1.Size = new System.Drawing.Size(117, 25);
            this.dbCombox1.TabIndex = 1;
            this.dbCombox1.Url = "http://localhost:5235/parame/";
            this.dbCombox1.ValueMember = "ID";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DGTBC_ID,
            this.DGTB_Name,
            this.DGTBC_Price,
            this.DGTBC_Quantity});
            dbCondition3.Name = "name";
            dbCondition3.Symbol = "like";
            dbCondition3.Value = "%abc%";
            dbCondition4.Name = "age";
            dbCondition4.Symbol = ">=";
            dbCondition4.Value = "10";
            dbCondition5.Name = "type";
            dbCondition5.Symbol = "like";
            dbCondition5.Value = "%大%";
            dbCondition6.Name = "ppt";
            dbCondition6.Symbol = "like";
            dbCondition6.Value = "%123%";
            this.dataGridView1.Conditions.Add(dbCondition3);
            this.dataGridView1.Conditions.Add(dbCondition4);
            this.dataGridView1.Conditions.Add(dbCondition5);
            this.dataGridView1.Conditions.Add(dbCondition6);
            this.dataGridView1.DataSourceName = "order";
            this.dataGridView1.Location = new System.Drawing.Point(305, 22);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.Size = new System.Drawing.Size(479, 253);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.Url = "http://localhost:5235/parame/";
            // 
            // DGTBC_ID
            // 
            this.DGTBC_ID.DataPropertyName = "ID";
            this.DGTBC_ID.HeaderText = "编号";
            this.DGTBC_ID.Name = "DGTBC_ID";
            // 
            // DGTB_Name
            // 
            this.DGTB_Name.DataPropertyName = "Name";
            this.DGTB_Name.HeaderText = "名称";
            this.DGTB_Name.Name = "DGTB_Name";
            // 
            // DGTBC_Price
            // 
            this.DGTBC_Price.DataPropertyName = "Price";
            this.DGTBC_Price.HeaderText = "价格";
            this.DGTBC_Price.Name = "DGTBC_Price";
            // 
            // DGTBC_Quantity
            // 
            this.DGTBC_Quantity.DataPropertyName = "Quantity";
            this.DGTBC_Quantity.HeaderText = "数量";
            this.DGTBC_Quantity.Name = "DGTBC_Quantity";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(822, 366);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.dbCombox1);
            this.Controls.Add(this.dbListBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        private DBControl.DBListBox dbListBox1;
        private DBControl.DBComBox dbCombox1;
        private DBControl.DBDataGridView dataGridView1;
        private DataGridViewTextBoxColumn DGTBC_ID;
        private DataGridViewTextBoxColumn DGTBC_Name;
        private DataGridViewTextBoxColumn DGTBC_Price;
        private DataGridViewTextBoxColumn DGTBC_Quantity;
        private DataGridViewTextBoxColumn DGTB_Name;

        #endregion
        //private DBCombox dbCombox1;
    }
}