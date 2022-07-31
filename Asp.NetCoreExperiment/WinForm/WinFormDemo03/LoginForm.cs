using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormDemo03.Models;

namespace WinFormDemo03
{
    public partial class LoginForm : Form
    {
        private readonly List<User> _users;
        public LoginForm()
        {
            _users = new List<User>()
            {
                new User{ ID=1,Name="桂素伟", UserName="gsw",Password="abc123" },
                new User{ ID=2,Name="张三", UserName="zs",Password="123abc" }
            };
            InitializeComponent();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            Common.CurrentUser = _users.SingleOrDefault(s => s.UserName == usernameTextBox.Text && s.Password == passwordTextBox.Text);
            if (Common.CurrentUser == null)
            {
                MessageBox.Show("用户名或密码错误，请重新输入！");
            }
            else
            {
                this.DialogResult = DialogResult.OK;
            }
        }

    }
}