namespace SmartWinForms
{
    partial class AIChat
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            splitContainer1 = new SplitContainer();
            historyTB = new RichTextBox();
            chatTB = new RichTextBox();
            bottomPan = new Panel();
            sendBut = new Button();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            bottomPan.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(historyTB);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(chatTB);
            splitContainer1.Panel2.Controls.Add(bottomPan);
            splitContainer1.Size = new Size(1180, 870);
            splitContainer1.SplitterDistance = 510;
            splitContainer1.TabIndex = 0;
            // 
            // historyTB
            // 
            historyTB.Dock = DockStyle.Fill;
            historyTB.Location = new Point(0, 0);
            historyTB.Name = "historyTB";
            historyTB.ReadOnly = true;
            historyTB.Size = new Size(1180, 510);
            historyTB.TabIndex = 0;
            historyTB.Text = "";
            // 
            // chatTB
            // 
            chatTB.Dock = DockStyle.Fill;
            chatTB.Location = new Point(0, 0);
            chatTB.Name = "chatTB";
            chatTB.Size = new Size(1180, 281);
            chatTB.TabIndex = 1;
            chatTB.Text = "";
            chatTB.KeyPress += chatTB_KeyPress;
            // 
            // bottomPan
            // 
            bottomPan.Controls.Add(sendBut);
            bottomPan.Dock = DockStyle.Bottom;
            bottomPan.Location = new Point(0, 281);
            bottomPan.Name = "bottomPan";
            bottomPan.Padding = new Padding(5);
            bottomPan.Size = new Size(1180, 75);
            bottomPan.TabIndex = 0;
            // 
            // sendBut
            // 
            sendBut.Dock = DockStyle.Right;
            sendBut.Location = new Point(1011, 5);
            sendBut.Name = "sendBut";
            sendBut.Size = new Size(164, 65);
            sendBut.TabIndex = 0;
            sendBut.Text = "发送";
            sendBut.UseVisualStyleBackColor = true;
            sendBut.Click += sendBut_Click;
            // 
            // AIChat
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(splitContainer1);
            Name = "AIChat";
            Size = new Size(1180, 870);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            bottomPan.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer1;
        private RichTextBox historyTB;
        private RichTextBox chatTB;
        private Panel bottomPan;
        private Button sendBut;
    }
}
