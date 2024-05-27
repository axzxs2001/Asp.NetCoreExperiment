using FlaUI.UIA3;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using System.Threading;
using AutoIt;

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
            AutoItX.Run("calc.exe", "", AutoItX.SW_SHOW);

            AutoItX.WinWaitActive("Calculator");
            var allControls = AutoItX.ControlListView("Calculator", "", "[CLASS:SysListView32]", "", "","");


            foreach (var control in allControls)
            {
                Console.WriteLine($"Control: {control}");
            }

            AutoItX.WinClose("Calculator");
        }

        static void F1()
        {
            using (var automation = new UIA3Automation())
            {
                var list = Process.GetProcesses();
                var app = FlaUI.Core.Application.Attach("CalculatorApp");

                var mainWindow = app.GetMainWindow(automation);
                Console.WriteLine("Calculator window found: " + mainWindow.Title);

                var allControls = GetAllChildElements(mainWindow);
                foreach (var control in allControls)
                {
                    try
                    {
                        Console.WriteLine($"Control Type: {control.ControlType}, AutomationId: {control.AutomationId}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
        static List<FlaUI.Core.AutomationElements.AutomationElement> GetAllChildElements(FlaUI.Core.AutomationElements.AutomationElement parent)
        {
            var allChildElements = new List<FlaUI.Core.AutomationElements.AutomationElement>();
            var children = parent.FindAllChildren();

            foreach (var child in children)
            {
                allChildElements.Add(child);
                allChildElements.AddRange(GetAllChildElements(child)); // Recursively get all children
            }

            return allChildElements;
        }
    }
}
