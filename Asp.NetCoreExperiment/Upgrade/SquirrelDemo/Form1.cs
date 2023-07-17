using AutoUpdaterDotNET;
using System.Net;

namespace SquirrelDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AutoUpdaterStarter();
            button1.Text = "当前版本号：" + Application.ProductVersion;
        }



        private void AutoUpdaterStarter()
        {
            //XML文件服务器下载地址
            AutoUpdater.Start("http://127.0.0.1:5000/AutoUpdaterStarter.xml");

            // AutoUpdater.UpdateMode = Mode.ForcedDownload;


            //通过将其分配给InstalledVersion字段来提供自己的版本
            //AutoUpdater.InstalledVersion = new Version("1.2");

            //查看中文版本
            //Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.CreateSpecificCulture("zh");

            //显示自定义的应用程序标题
            AutoUpdater.AppTitle = "我的程序升级更新";

            //不显示“稍后提醒”按钮
            //AutoUpdater.ShowRemindLaterButton = false;

            //强制选项将隐藏稍后提醒，跳过和关闭按钮的标准更新对话框。
            //AutoUpdater.Mandatory = true;
            //AutoUpdater.UpdateMode = Mode.Forced;

            //为XML、更新文件和更改日志提供基本身份验证
            //BasicAuthentication basicAuthentication = new BasicAuthentication("myUserName", "myPassword");
            //AutoUpdater.BasicAuthXML = AutoUpdater.BasicAuthDownload = AutoUpdater.BasicAuthChangeLog = basicAuthentication;

            //为http web请求设置User-Agent
            //AutoUpdater.HttpUserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/89.0.4389.90 Safari/537.36";

            //设置代理
            //AutoUpdater.Proxy = new WebProxy("", 1);
            //var proxy = new WebProxy("ProxyIP:ProxyPort",1)
            //{
            //    Credentials = new NetworkCredential("ProxyUserName", "ProxyPassword")
            //};
            //AutoUpdater.Proxy = proxy;



            //启用错误报告
            AutoUpdater.ReportErrors = true;

            //将应用程序设定不需要管理员权限来替换旧版本
            //AutoUpdater.RunUpdateAsAdmin = false;

            //打开下载页面，不直接下载最新版本*****
            AutoUpdater.OpenDownloadPage = true;

            //设置为要下载更新文件的文件夹路径。如果没有提供，则默认为临时文件夹。
            //AutoUpdater.DownloadPath = Environment.CurrentDirectory;

            //设置zip解压路径
            AutoUpdater.InstallationPath = Environment.CurrentDirectory;

            //处理应用程序在下载完成后如何退出
            AutoUpdater.ApplicationExitEvent += AutoUpdater_ApplicationExitEvent;

            //自定义处理更新逻辑事件
            AutoUpdater.CheckForUpdateEvent += AutoUpdaterOnCheckForUpdateEvent;

           
        }


        private void AutoUpdater_ApplicationExitEvent()
        {
            Text = @"退出应用中...";
            Thread.Sleep(5000);
            Application.Exit();
        }

        private void AutoUpdaterOnCheckForUpdateEvent(UpdateInfoEventArgs args)
        {
            if (args.Error == null)
            {
                if (args.IsUpdateAvailable)
                {
                    DialogResult dialogResult;
                    if (args.Mandatory.Value)
                    {
                        dialogResult =
                            MessageBox.Show(
                                $@"您有新的版本 {args.CurrentVersion} 可用，当前使用的是{args.InstalledVersion} 版本，这是必需的更新。",
                                @"更新",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                    }
                    else
                    {
                        dialogResult =
                            MessageBox.Show(
                                $@"您有新的版本 {args.CurrentVersion} 可用，当前使用的是{args.InstalledVersion} 版本，您现在要更新应用程序吗？", @"更新",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Information);
                    }

                    if (dialogResult.Equals(DialogResult.Yes) || dialogResult.Equals(DialogResult.OK))
                    {
                        try
                        {
                            //You can use Download Update dialog used by AutoUpdater.NET to download the update.

                            if (AutoUpdater.DownloadUpdate(args))
                            {
                                Application.Exit();
                            }                            
                        }
                        catch (Exception exception)
                        {
                            MessageBox.Show(exception.Message, exception.GetType().ToString(), MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                if (args.Error is System.Net.WebException)
                {
                    MessageBox.Show(
                        @"更新服务器有问题，请核对网络连接，然后再试。",
                        @"更新失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(args.Error.Message,
                        args.Error.GetType().ToString(), MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }
    }

}

