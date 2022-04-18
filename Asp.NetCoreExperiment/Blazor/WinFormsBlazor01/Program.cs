namespace WinFormsBlazor01
{
    internal static class Program
    {       
        [STAThread]
        static void Main()
        {           
            ApplicationConfiguration.Initialize();
            Application.Run(new AddForm());
        }
    }
}