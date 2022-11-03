using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;

namespace WinFormsDemo15
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 创建连接后的网络流
        /// </summary>
        NetworkStream _stream;
        private void ConnectionButton_Click(object sender, EventArgs e)
        {
            if (IsServer.Checked)
            {
                AcceptTcpClient();
            }
            else
            {
                var tcpClient = new TcpClient(IPTextBox.Text, int.Parse(PortTextBox.Text));
                _stream = tcpClient.GetStream();
                ReciveMessage(_stream);
            }
        }
        /// <summary>
        /// 在一个新线程中接收TcpClient连接
        /// </summary>
        void AcceptTcpClient()
        {
            Task.Run(() =>
            {
                var listener = new TcpListener(IPAddress.Any, int.Parse(PortTextBox.Text));
                listener.Start();
                var tcpClient = listener.AcceptTcpClient();
                this.Invoke(() =>
                {
                    YouTextBox.Text += $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} \r\n收到客户端连接。\r\n";
                });
                _stream = tcpClient.GetStream();
                ReciveMessage(_stream);
            });
        }
        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="stream">网络流</param>
        void ReciveMessage(NetworkStream stream)
        {
            Task.Run(() =>
            {
                while (true)
                {
                    var messageArr = new byte[1024];
                    stream.Read(messageArr, 0, messageArr.Length);
                    this.Invoke(() =>
                    {
                        var message = System.Text.Encoding.UTF8.GetString(messageArr).Trim('\0');
                        YouTextBox.Text += $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} 对方说：\r\n{message}\r\n";
                    });
                }
            });
        }
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SendButton_Click(object sender, EventArgs e)
        {
            var messageArr = System.Text.Encoding.UTF8.GetBytes(MyTextBox.Text);
            _stream.Write(messageArr, 0, messageArr.Length);
            _stream.Flush();
            YouTextBox.Text += $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} 我说：\r\n{MyTextBox.Text}\r\n";
            MyTextBox.Clear();
        }
    }
}