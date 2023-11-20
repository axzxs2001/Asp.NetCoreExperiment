namespace BindDemo
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button_Add = new Button();
            listBox_Tasks = new ListBox();
            textBox_Task = new TextBox();
            tableLayoutPanel2 = new TableLayoutPanel();
            button_Delete = new Button();
            groupBox_Tasks = new GroupBox();
            label_ToDoList = new Label();
            tableLayoutPanel2.SuspendLayout();
            groupBox_Tasks.SuspendLayout();
            SuspendLayout();
            // 
            // button_Add
            // 
            button_Add.Dock = DockStyle.Fill;
            button_Add.FlatStyle = FlatStyle.Flat;
            button_Add.Font = new Font("Segoe UI", 9F);
            button_Add.Location = new Point(443, 1159);
            button_Add.Margin = new Padding(5);
            button_Add.Name = "button_Add";
            button_Add.Size = new Size(226, 46);
            button_Add.TabIndex = 1;
            button_Add.Text = "Add";
            button_Add.UseVisualStyleBackColor = true;
            // 
            // listBox_Tasks
            // 
            listBox_Tasks.Dock = DockStyle.Fill;
            listBox_Tasks.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            listBox_Tasks.FormattingEnabled = true;
            listBox_Tasks.ItemHeight = 32;
            listBox_Tasks.Location = new Point(5, 43);
            listBox_Tasks.Margin = new Padding(16);
            listBox_Tasks.Name = "listBox_Tasks";
            listBox_Tasks.Size = new Size(882, 1016);
            listBox_Tasks.TabIndex = 2;
            // 
            // textBox_Task
            // 
            textBox_Task.BorderStyle = BorderStyle.FixedSingle;
            textBox_Task.Dock = DockStyle.Fill;
            textBox_Task.Font = new Font("Segoe UI", 12F);
            textBox_Task.Location = new Point(13, 1159);
            textBox_Task.Margin = new Padding(5);
            textBox_Task.Name = "textBox_Task";
            textBox_Task.PlaceholderText = "Task";
            textBox_Task.Size = new Size(420, 39);
            textBox_Task.TabIndex = 3;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 3;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 236F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 236F));
            tableLayoutPanel2.Controls.Add(button_Delete, 2, 2);
            tableLayoutPanel2.Controls.Add(button_Add, 1, 2);
            tableLayoutPanel2.Controls.Add(textBox_Task, 0, 2);
            tableLayoutPanel2.Controls.Add(groupBox_Tasks, 0, 1);
            tableLayoutPanel2.Controls.Add(label_ToDoList, 0, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            tableLayoutPanel2.Location = new Point(0, 0);
            tableLayoutPanel2.Margin = new Padding(5);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.Padding = new Padding(8);
            tableLayoutPanel2.RowCount = 3;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 72F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
            tableLayoutPanel2.Size = new Size(918, 1218);
            tableLayoutPanel2.TabIndex = 5;
            // 
            // button_Delete
            // 
            button_Delete.Dock = DockStyle.Fill;
            button_Delete.FlatStyle = FlatStyle.Flat;
            button_Delete.Font = new Font("Segoe UI", 9F);
            button_Delete.Location = new Point(679, 1159);
            button_Delete.Margin = new Padding(5);
            button_Delete.Name = "button_Delete";
            button_Delete.Size = new Size(226, 46);
            button_Delete.TabIndex = 1;
            button_Delete.Text = "Delete";
            button_Delete.UseVisualStyleBackColor = true;
            // 
            // groupBox_Tasks
            // 
            tableLayoutPanel2.SetColumnSpan(groupBox_Tasks, 3);
            groupBox_Tasks.Controls.Add(listBox_Tasks);
            groupBox_Tasks.Dock = DockStyle.Fill;
            groupBox_Tasks.Font = new Font("Segoe UI", 14.25F);
            groupBox_Tasks.Location = new Point(13, 85);
            groupBox_Tasks.Margin = new Padding(5);
            groupBox_Tasks.Name = "groupBox_Tasks";
            groupBox_Tasks.Padding = new Padding(5);
            groupBox_Tasks.Size = new Size(892, 1064);
            groupBox_Tasks.TabIndex = 4;
            groupBox_Tasks.TabStop = false;
            groupBox_Tasks.Text = "Tasks";
            // 
            // label_ToDoList
            // 
            label_ToDoList.AutoSize = true;
            tableLayoutPanel2.SetColumnSpan(label_ToDoList, 3);
            label_ToDoList.Dock = DockStyle.Fill;
            label_ToDoList.Font = new Font("Segoe UI", 21.75F, FontStyle.Bold);
            label_ToDoList.Location = new Point(13, 8);
            label_ToDoList.Margin = new Padding(5, 0, 5, 0);
            label_ToDoList.Name = "label_ToDoList";
            label_ToDoList.Size = new Size(892, 72);
            label_ToDoList.TabIndex = 5;
            label_ToDoList.Text = "Todo List";
            label_ToDoList.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(918, 1218);
            Controls.Add(tableLayoutPanel2);
            Margin = new Padding(5);
            MinimumSize = new Size(930, 1246);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "WinForms MVVM Todo";
            Load += Form1_Load_1;
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            groupBox_Tasks.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Button button_Add;
        private ListBox listBox_Tasks;
        private TextBox textBox_Task;
        private TableLayoutPanel tableLayoutPanel2;
        private Button button_Delete;
        private GroupBox groupBox_Tasks;
        private Label label_ToDoList;
    }
}
