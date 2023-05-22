namespace WinForm_GPTDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

    }
    //生成姓名，年龄，身高，体重实体类
    public class Person
    {
        public string name { get; set; }
        public int age { get; set; }
        public int height { get; set; }
        public int weight { get; set; }
    }
}