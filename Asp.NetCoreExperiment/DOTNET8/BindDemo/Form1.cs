using Prism.Commands;
using Prism.Mvvm;
using System.ComponentModel;

namespace BindDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //var person = new Person() { Name = "张三", Birthday = DateTime.Parse("2017-5-3") };

            //textBox1.DataBindings.Add(new Binding("Text", person, "Name"));

            //dateTimePicker1.DataBindings.Add(new Binding("Value", person, "Birthday"));
            //var student = new Student();
            //student.StuName = "张三";
            //this.DataContext = student;

            //textBox1.DataBindings.Add(new Binding(nameof(textBox1.Text), DataContext, nameof(student.StuName), true, DataSourceUpdateMode.OnPropertyChanged));


        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            #region ViewModelInitialization
            var viewModel = new MainFormViewModel();
            DataContext = viewModel;
            #endregion

            #region DataBindings
            button_Add.DataBindings.Add(new Binding(nameof(button_Add.Command), DataContext, nameof(viewModel.OnAddCommand), true));
            button_Delete.DataBindings.Add(new Binding(nameof(button_Delete.Command), DataContext, nameof(viewModel.OnDeleteCommand), true));
            listBox_Tasks.DataBindings.Add(new Binding(nameof(listBox_Tasks.DataSource), DataContext, nameof(viewModel.TodoList), true));
            textBox_Task.DataBindings.Add(new Binding(nameof(textBox_Task.Text), DataContext, nameof(viewModel.Task), true, DataSourceUpdateMode.OnPropertyChanged));
            listBox_Tasks.DataBindings.Add(new Binding(nameof(listBox_Tasks.SelectedValue), DataContext, nameof(viewModel.SelectedTask), true, DataSourceUpdateMode.OnPropertyChanged));
            #endregion
        }
    }


    public class MainFormViewModel : BindableBase
    {
        #region Properties
        private BindingList<string> todoList = new BindingList<string>();
        public BindingList<string> TodoList
        {
            get { return todoList; }
        }

        private string task;
        public string Task
        {
            get { return task; }
            set { SetProperty(ref task, value); }
        }

        private string selectedTask;
        public string SelectedTask
        {
            get { return selectedTask; }
            set { SetProperty(ref selectedTask, value); }
        }
        #endregion

        #region Commands
        public DelegateCommand OnAddCommand { get; set; }
        public DelegateCommand OnDeleteCommand { get; set; }
        #endregion

        #region Constructor       
        public MainFormViewModel()
        {
            #region Commands
            OnAddCommand = new DelegateCommand(() =>
            {
                TodoList.Add(Task);

                Task = string.Empty;

                OnAddCommand?.RaiseCanExecuteChanged();
                OnDeleteCommand?.RaiseCanExecuteChanged();
            }, () => Task?.Length > 0);
            OnDeleteCommand = new DelegateCommand(() =>
            {
                TodoList.Remove(SelectedTask);

                OnAddCommand?.RaiseCanExecuteChanged();
                OnDeleteCommand.RaiseCanExecuteChanged();
            }, () => TodoList?.Count > 0 && !string.IsNullOrEmpty(SelectedTask));
            #endregion

            PropertyChanged += (_, e) =>
            {
                if (e.PropertyName == nameof(SelectedTask))
                {
                    OnDeleteCommand?.RaiseCanExecuteChanged();
                }
                else if (e.PropertyName == nameof(Task))
                {
                    OnAddCommand?.RaiseCanExecuteChanged();
                }
            };
        }
        #endregion
    }
}
