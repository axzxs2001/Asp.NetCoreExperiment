using Dapper;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Collections;

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
            var rootNode = treeView2.Nodes.Add("0", "中国");
            LoadProvince(rootNode, 1);
            this.treeView2.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView2_BeforeExpand);


        }

        private void button1_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            var rootNode = treeView1.Nodes.Add("0", "中国");

            LoadProvince(rootNode);

            //var tn = new TreeNode();
            //tn.Text = "中国";
            //tn.Name = "0";

            //LoadProvinceA(tn);
            //treeView1.Nodes.Add(tn);



            //var rootPath = textBox1.Text;
            //var rootNode = treeView1.Nodes.Add(rootPath, Path.GetFileName(rootPath));
            //LoadFile(rootNode);
        }
        void LoadProvinceA(TreeNode node)
        {
            Task.Run(() =>
            {
                foreach (var item in list.Where(s => s.pid == node.Name).OrderBy(s => s.sid))
                {
                    var childNode = node.Nodes.Add(item.sid, item.name);

                    //foreach (TreeNode childNode in node.Nodes)
                    //{
                    //    LoadProvince(childNode);
                    //}
                }
                //Task.Run(() =>
                //{
                //    foreach (TreeNode childNode in node.Nodes)
                //    {
                //        LoadProvince(childNode);
                //    }
                //});
            });
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
                        node.Nodes.Add(item.sid, item.name);
                        if (node.Level == 0)
                        {
                            node.Expand();
                        }
                    });
                }
                foreach (TreeNode childNode in node.Nodes)
                {
                    LoadProvince(childNode);
                }
            });
        }

        //void LoadProvince(TreeNode node)
        //{

        //    foreach (var item in list.Where(s => s.pid == node.Name))
        //    {
        //        var childNode = node.Nodes.Add(item.sid, item.name);
        //        LoadProvince(childNode);
        //    }
        //}

        void LoadProvince(TreeNode node, int i)
        {
            i++;
            foreach (var item in list.Where(s => s.pid == node.Name))
            {
                var childNode = node.Nodes.Add(item.sid, item.name);
                if (node.Level == 0)
                {
                    node.Expand();
                }
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

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var fs = new FileStream(Directory.GetCurrentDirectory() + "/DataFile.dat", FileMode.Create);
            // Construct a BinaryFormatter and use it to serialize the data to the stream.
            try
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(fs, treeView2);
            }
            catch (SerializationException ee)
            {
                MessageBox.Show("Failed to serialize. Reason: " + ee.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            var fs = new FileStream(Directory.GetCurrentDirectory() + "/DataFile.dat", FileMode.Open);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();

                // Deserialize the hashtable from the file and
                // assign the reference to the local variable.
                treeView1.Nodes.Add((TreeNode)formatter.Deserialize(fs));
            }
            catch (SerializationException ee)
            {
                MessageBox.Show("Failed to serialize. Reason: " + ee.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }

        }



        private void treeView2_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node != null && e.Node.Nodes != null && e.Node.Nodes.Count > 0)
            {
                e.Node.Nodes.Clear();
                LoadProvince(e.Node, 1);
            }
        }
    }
    class Province
    {
        public string sid { get; set; }
        public string pid { get; set; }
        public string name { get; set; }

    }

    [Serializable]    
    public class MyTreeView : TreeView
    {        

    }

}