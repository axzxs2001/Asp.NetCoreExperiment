using System;
using System.Threading;
using System.Windows.Threading;
using WpfApp1;
using Xunit;

namespace XUnitTestProject1
{
    public class AAAA
    {
        [Fact]
        public void ABCD()
        {
            var  thread = new Thread(F);
            thread.SetApartmentState(ApartmentState.STA);
            thread.IsBackground = true;
            var t = new T();
            thread.Start(t);
            thread.Join();
            Assert.Equal(t.V, DateTime.Now.Date);      
        }

        static void F(object obj)
        {
            Thread.CurrentThread.SetApartmentState(ApartmentState.STA);
            var main = new MainWindow();
            main.button_Click(null, null);
            var t = obj as T;
            t.V = DateTime.Parse(main.text01.Text).Date;
        }
    }
    class T
    {
        public object V
        { get; set; }
    }
}
