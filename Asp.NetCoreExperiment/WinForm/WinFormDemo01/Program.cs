namespace WinFormDemo01
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
      
            ApplicationConfiguration.Initialize();

            Application.Run(new Form1());

        }
        static bool MessageLoopCallback()
        {
            return true;
        }

    }
    
}