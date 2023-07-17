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
            button1.Text = "��ǰ�汾�ţ�" + Application.ProductVersion;
        }



        private void AutoUpdaterStarter()
        {
            //XML�ļ����������ص�ַ
            AutoUpdater.Start("http://127.0.0.1:5000/AutoUpdaterStarter.xml");

            // AutoUpdater.UpdateMode = Mode.ForcedDownload;


            //ͨ����������InstalledVersion�ֶ����ṩ�Լ��İ汾
            //AutoUpdater.InstalledVersion = new Version("1.2");

            //�鿴���İ汾
            //Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.CreateSpecificCulture("zh");

            //��ʾ�Զ����Ӧ�ó������
            AutoUpdater.AppTitle = "�ҵĳ�����������";

            //����ʾ���Ժ����ѡ���ť
            //AutoUpdater.ShowRemindLaterButton = false;

            //ǿ��ѡ������Ժ����ѣ������͹رհ�ť�ı�׼���¶Ի���
            //AutoUpdater.Mandatory = true;
            //AutoUpdater.UpdateMode = Mode.Forced;

            //ΪXML�������ļ��͸�����־�ṩ���������֤
            //BasicAuthentication basicAuthentication = new BasicAuthentication("myUserName", "myPassword");
            //AutoUpdater.BasicAuthXML = AutoUpdater.BasicAuthDownload = AutoUpdater.BasicAuthChangeLog = basicAuthentication;

            //Ϊhttp web��������User-Agent
            //AutoUpdater.HttpUserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/89.0.4389.90 Safari/537.36";

            //���ô���
            //AutoUpdater.Proxy = new WebProxy("", 1);
            //var proxy = new WebProxy("ProxyIP:ProxyPort",1)
            //{
            //    Credentials = new NetworkCredential("ProxyUserName", "ProxyPassword")
            //};
            //AutoUpdater.Proxy = proxy;



            //���ô��󱨸�
            AutoUpdater.ReportErrors = true;

            //��Ӧ�ó����趨����Ҫ����ԱȨ�����滻�ɰ汾
            //AutoUpdater.RunUpdateAsAdmin = false;

            //������ҳ�棬��ֱ���������°汾*****
            AutoUpdater.OpenDownloadPage = true;

            //����ΪҪ���ظ����ļ����ļ���·�������û���ṩ����Ĭ��Ϊ��ʱ�ļ��С�
            //AutoUpdater.DownloadPath = Environment.CurrentDirectory;

            //����zip��ѹ·��
            AutoUpdater.InstallationPath = Environment.CurrentDirectory;

            //����Ӧ�ó�����������ɺ�����˳�
            AutoUpdater.ApplicationExitEvent += AutoUpdater_ApplicationExitEvent;

            //�Զ��崦������߼��¼�
            AutoUpdater.CheckForUpdateEvent += AutoUpdaterOnCheckForUpdateEvent;

           
        }


        private void AutoUpdater_ApplicationExitEvent()
        {
            Text = @"�˳�Ӧ����...";
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
                                $@"�����µİ汾 {args.CurrentVersion} ���ã���ǰʹ�õ���{args.InstalledVersion} �汾�����Ǳ���ĸ��¡�",
                                @"����",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                    }
                    else
                    {
                        dialogResult =
                            MessageBox.Show(
                                $@"�����µİ汾 {args.CurrentVersion} ���ã���ǰʹ�õ���{args.InstalledVersion} �汾��������Ҫ����Ӧ�ó�����", @"����",
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
                        @"���·����������⣬��˶��������ӣ�Ȼ�����ԡ�",
                        @"����ʧ��", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

