namespace WinFormDemo01
{
    internal static class Program
    {
        [STAThread]
        static int Main(string[] args)
        {           

            MessageBox.Show($"Main�������յ��Ĳ�����{string.Join(',', args)}");
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
            return 101;
        }      
    }
}