using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace WinFormBindDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var order = new Person()
            {
                FirstName = "¹ð",
                LastName = "ËØÎ°"
            };
            DataContext = order;
            textBox1.DataBindings.Add(new Binding(nameof(textBox1.Text), DataContext, nameof(order.FirstName), true, DataSourceUpdateMode.OnPropertyChanged));
            textBox2.DataBindings.Add(new Binding(nameof(textBox2.Text), DataContext, nameof(order.FirstName), true, DataSourceUpdateMode.OnPropertyChanged));
        }
    }

    public class Person : INotifyPropertyChanged
    {
        // The event that is fired when a property changes.
        public event PropertyChangedEventHandler? PropertyChanged;

        // Backing field for the property.
        private string? _lastName;
        private string? _firstName;

        // The property that is being monitored.
        public string? LastName
        {
            get => _lastName;

            set
            {
                if (_lastName == value)
                {
                    return;
                }
                _lastName = value;
                OnPropertyChanged();
            }
        }

        public string? FirstName
        {
            get => _firstName;

            set
            {
                if (_firstName == value)
                {
                    return;
                }

                _firstName = value;
                OnPropertyChanged();
            }
        }

        public ICommand ButCommand { get; set; }
        public Person()
        {
            ButCommand = new DelegateCommand(() =>
            {
                TodoList.Add(Task);

                Task = string.Empty;

                OnAddCommand?.RaiseCanExecuteChanged();
                OnDeleteCommand?.RaiseCanExecuteChanged();
            }, () => Task?.Length > 0);
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}