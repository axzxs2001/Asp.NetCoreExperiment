using System.Runtime.InteropServices;
using System.Runtime.Loader;

namespace WinFormsSignal
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {

            AssemblyLoadContext.Default.Unloading += unloadTask;
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());   


        }

       
        private static void unloadTask(AssemblyLoadContext obj)
        {
            Console.WriteLine("Unloading");
        }

    }
}