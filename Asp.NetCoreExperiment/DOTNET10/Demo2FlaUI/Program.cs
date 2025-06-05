using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo2FlaUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var app = FlaUI.Core.Application.Launch("calc.exe");
            using (var automation = new UIA3Automation())
            {
                var window = app.GetMainWindow(automation);
                var button1 = window.FindFirstDescendant(cf => cf.ByText("1"))?.AsButton();
                button1?.Invoke();

                // 点击菜单栏 “文件” -> “退出”
                var fileMenu = window.FindFirstDescendant(cf => cf.ByText("文件"))?.AsMenuItem();
                fileMenu?.Invoke();

                // 根据需要关闭应用
            }
        }
    }
}
