using System.ComponentModel;

namespace WinFormsDemo06
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        bool mark = true;
        private void startButton_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                foreach (var item in list)
                {
                    if (mark == false)
                    {
                        break;
                    }
                    var dotString = "";
                    for (var i = 0; i < item.Time; i++)
                    {
                        if (mark == false)
                        {
                            break;
                        }
                        if (i % 6 == 0)
                        {
                            dotString = ".";
                        }
                        else
                        {
                            dotString += ".";
                        }
                        this.Invoke(() =>
                        {
                            messageLabel.Text = $"{item.Name}{dotString}";
                        });
                        SpinWait.SpinUntil(() => false, 500);
                    }
                }
                this.Invoke(() =>
                {
                    this.Close();
                });
            });
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (mark)
            //{
            //    e.Cancel = true;
            //    mark = false;

            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                foreach (var item in list)
                {
                    var dotString = "";
                    for (var i = 0; i < item.Time; i++)
                    {
                        if (i % 6 == 0)
                        {
                            dotString = ".";
                        }
                        else
                        {
                            dotString += ".";
                        }
                        this.Invoke(() =>
                        {
                            messageLabel.Text = $"{item.Name}{dotString}";
                        });
                        SpinWait.SpinUntil(() => false, 300);
                    }
                }
            });
        }
        static List<Item> list;
        private void Form1_Load(object sender, EventArgs e)
        {
            list = new List<Item>
            {
                new Item{ Name="正在上传诊疗项目",Time=8 },
                new Item{ Name="正在上传器材",Time=12 },
                new Item{ Name="正在上传药品",Time=20 },
                new Item{ Name="正在核对",Time=24 },
            };
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                foreach (var item in list)
                {
                    var dotString = "";
                    for (var i = 0; i < item.Time; i++)
                    {
                        if (i % 6 == 0)
                        {
                            dotString = ".";
                        }
                        else
                        {
                            dotString += ".";
                        }
                        messageLabel.Text = $"{item.Name}{dotString}";
                        Thread.Sleep(300);
                    }
                }
            });
        }
    }
    class Item
    {
        public string Name { get; set; }
        public int Time { get; set; }
    }
}