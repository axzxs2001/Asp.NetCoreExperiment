using Dapper;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinFormsDemo09
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            using var con = new Microsoft.Data.SqlClient.SqlConnection("server=.;database=exam;uid=gsw;pwd=gsw;TrustServerCertificate=true");
            list = con.Query<Province>("select sid,pid,name from province").ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            var rootNode = treeView1.Nodes.Add("0", "ÖÐ¹ú");

            LoadProvince(rootNode);
       
            //var rootPath = textBox1.Text;
            //var rootNode = treeView1.Nodes.Add(rootPath, Path.GetFileName(rootPath));
            //LoadFile(rootNode);
        }
        static List<Province> list = new List<Province>();
        void LoadProvince(TreeNode node)
        {
            Task.Run(() =>
            {
                foreach (var item in list.Where(s => s.pid == node.Name).OrderBy(s => s.sid))
                {
                    this.Invoke(() =>
                    {
                    
                        // TreeNode childNode = new TreeNode();
                        var childNode = node.Nodes.Add(item.sid, item.name);                           
                        if(node.Level==0)
                        {
                            node.Expand();
                        }
                    });
                }
                Task.Run(() =>
                {
                    foreach (TreeNode childNode in node.Nodes)
                    {
                        LoadProvince(childNode);                 
                    }
                });
            });
        }
        void LoadProvince(TreeNode node, int i)
        {
            i++;
            using var con = new Microsoft.Data.SqlClient.SqlConnection("server=.;database=exam;uid=gsw;pwd=gsw;TrustServerCertificate=true");
            var list = con.Query<Province>("select sid,pid,name from province where pid=@pid", new { pid = node.Name });
            foreach (var item in list)
            {
                var childNode = node.Nodes.Add(item.sid, item.name);
                if (i < 3)
                {
                    LoadProvince(childNode, i);
                }
            }
        }

        void LoadFile(TreeNode node)
        {
            foreach (var file in Directory.GetFiles(node.Name))
            {
                node.Nodes.Add(file, Path.GetFileName(file));
            }
            foreach (var dir in Directory.GetDirectories(node.Name))
            {
                var childNode = node.Nodes.Add(dir, Path.GetFileName(dir));
                LoadFile(childNode);
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //LoadProvince(e.Node, 1);
        }
    }
    class Province
    {
        public string sid { get; set; }
        public string pid { get; set; }
        public string name { get; set; }

    }
}