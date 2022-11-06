using System.Windows.Forms;

namespace WinFormsDemo16
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Person person = new Person
        {
            Name = "桂素伟",
            Sex = Sex.女,
            Tel = new Tel { Name = "手机", No = "13333333333" },
            Birthday = DateTime.Parse("1979-06-22"),
            Goodses = new List<Goods>
            {
                new Goods{ID=1, Name="商品A", Price=1.1m,Quantity=10},
                new Goods{ID=2, Name="商品B", Price=2.1m,Quantity=20},
            }
        };
        private void button1_Click(object sender, EventArgs e)
        {
            var list = new List<Tel>
            {
                new Tel{Name="手机",No="13333333333" },
                new Tel{Name="电话",No="88888888" },
            };

            listBox1.DataSource = list;
            listBox1.DisplayMember = "Name";
            listBox1.ValueMember = "No";

            comboBox1.DataSource = Enum.GetValues(typeof(Sex));

            dataGridView1.DataSource = person.Goodses;

            textBox1.DataBindings.Add(new Binding("Text", person, "Name"));
            dateTimePicker1.DataBindings.Add(new Binding("Text", person, "Birthday"));
            comboBox1.DataBindings.Add(new Binding("SelectedItem", person, "Sex"));
            listBox1.DataBindings.Add(new Binding("SelectedItem", person, "Tel"));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(person.ToString());
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
    record Tel
    {
        public string Name { get; set; }
        public string No { get; set; }
    }
    record Person
    {
        public string Name { get; set; }
        public Sex Sex { get; set; }
        public DateTime Birthday { get; set; }
        public Tel Tel { get; set; }
        public List<Goods> Goodses { get; set; }
    }
    enum Sex
    {
        男,
        女
    }
    record Goods
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Quantity { get; set; }
        public decimal Price { get; set; }
    }
}