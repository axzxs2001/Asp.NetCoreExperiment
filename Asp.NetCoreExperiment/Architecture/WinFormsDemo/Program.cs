namespace WinFormsDemo
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
          //  Application.SetDefaultFont(new Font("Microsoft YaHei UI", 9));
            Application.Run(new frmMain());
        }
    }
}


//Application.SetDefaultFont(new Font("Segoe UI", 9));
//Application.SetDefaultFont(new Font("ººÒÇ×­Êé·±", 12));