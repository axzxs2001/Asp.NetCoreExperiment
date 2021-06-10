
namespace AsyncStreamForm
{
    partial class MainForm
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
            this.syncBut = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.asyncBut = new System.Windows.Forms.Button();
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // syncBut
            // 
            this.syncBut.Location = new System.Drawing.Point(21, 12);
            this.syncBut.Name = "syncBut";
            this.syncBut.Size = new System.Drawing.Size(75, 23);
            this.syncBut.TabIndex = 0;
            this.syncBut.Text = "加载";
            this.syncBut.UseVisualStyleBackColor = true;
            this.syncBut.Click += new  System.EventHandler(this.syncBut_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.asyncBut);
            this.panel1.Controls.Add(this.syncBut);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 45);
            this.panel1.TabIndex = 1;
            // 
            // asyncBut
            // 
            this.asyncBut.Location = new System.Drawing.Point(120, 12);
            this.asyncBut.Name = "asyncBut";
            this.asyncBut.Size = new System.Drawing.Size(190, 23);
            this.asyncBut.TabIndex = 1;
            this.asyncBut.Text = "异步流加载";
            this.asyncBut.UseVisualStyleBackColor = true;
            this.asyncBut.Click += new System.EventHandler(this.asyncBut_Click);
            // 
            // dataGrid
            // 
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid.Location = new System.Drawing.Point(0, 45);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.RowTemplate.Height = 25;
            this.dataGrid.Size = new System.Drawing.Size(800, 405);
            this.dataGrid.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataGrid);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button syncBut;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button asyncBut;
        private System.Windows.Forms.DataGridView dataGrid;
    }
}

