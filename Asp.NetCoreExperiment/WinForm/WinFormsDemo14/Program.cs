using NLog;
using NLog.Fluent;

namespace WinFormsDemo14
{
    internal static class Program
    {      
        [STAThread]
        static void Main()
        {    
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }  
}