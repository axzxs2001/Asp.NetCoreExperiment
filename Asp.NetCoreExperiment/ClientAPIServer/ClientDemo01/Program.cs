using System.Reflection;
using System.Xml.Linq;

namespace ClientDemo01
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            var s= typeof(GSWControls.DBComBox).Assembly.GetManifestResourceStream(typeof(GSWControls.DBComBox), "gcom.png");
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}