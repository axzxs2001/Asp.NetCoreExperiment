using System;
using WpfApp1;
using Xunit;

namespace XUnitTestProject1
{
    public class AAAA
    {
        [Fact]
        public void ABCD()
        {
            System.Windows.Application.Current.Dispatcher.Invoke((Action)(() =>
            {
                var main = new MainWindow();
                main.button_Click(null, null);
                Assert.Equal(DateTime.Parse(main.text01.Text).Date, DateTime.Now.Date);
            }));
  
        

        }
    }
}
