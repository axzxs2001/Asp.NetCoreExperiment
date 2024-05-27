using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;

namespace TestUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            F2();
        }


        static void F2()
        {

            foreach (var pro in Process.GetProcesses())
            {
                Console.WriteLine(pro.ProcessName);
            }
            var process = Process.GetProcessesByName("voovmeetingapp")[0];

            string windowTitle = "加入会议";

            // 找到目标窗口句柄
            IntPtr hwnd = FindWindow(null, windowTitle);
            // 获取目标窗体的自动化元素
            var rootElement = AutomationElement.FromHandle(process.MainWindowHandle);

            // 查找目标控件（例如文本框）
            var textBox = rootElement.FindFirst(TreeScope.Descendants,
                new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Edit));

            if (textBox != null)
            {
                // 设置控件的值
                var valuePattern = textBox.GetCurrentPattern(ValuePattern.Pattern) as ValuePattern;
                if (valuePattern != null)
                {
                    valuePattern.SetValue("新的值");
                }
            }
            else
            {
                Console.WriteLine("未找到目标控件");
            }
        }
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
    }
}
