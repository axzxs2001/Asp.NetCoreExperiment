using System.Diagnostics;
using System.Runtime.InteropServices;
using FlaUI.Core;
using FlaUI.UIA3;
static void F2()
{
    using (var automation = new UIA3Automation())
    {
        var app = FlaUI.Core.Application.Launch("calc.exe");
        var mainWindow = app.GetMainWindow(automation);
        Console.WriteLine("Calculator window found: " + mainWindow.Title);

        var buttonOne = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("num1Button"));
        var buttonPlus = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("plusButton"));
        var buttonTwo = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("num2Button"));
        var buttonEquals = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("equalButton"));

        buttonOne?.AsButton().Invoke();
        buttonPlus?.AsButton().Invoke();
        buttonTwo?.AsButton().Invoke();
        buttonEquals?.AsButton().Invoke();

        Console.WriteLine("Performed 1 + 2 = operation.");
    }
}










static void F1()
{

    const int WM_SETTEXT = 0x000C;

    string windowTitle = "新建 文本文档.txt - Notepad";

    // 找到目标窗口句柄
    IntPtr hwnd = FindWindow(null, windowTitle);
    if (hwnd == IntPtr.Zero)
    {
        Console.WriteLine("未找到目标窗口");
        return;
    }


    // 替换为目标文本框的类名或标题，如果不确定，可以使用Spy++等工具查找
    IntPtr hwndChild1 = FindWindowEx(hwnd, IntPtr.Zero, "RichEditD2DPT", null);

    IntPtr hwndChild = FindWindowEx(hwnd, IntPtr.Zero, null, "RichEditD2DPT");


    if (hwndChild == IntPtr.Zero)
    {
        Console.WriteLine("未找到目标文本框");
        return;
    }

    //将窗口置于前台（可选）
    SetForegroundWindow(hwnd);

    // 发送文本到文本框
    SendMessage(hwndChild, WM_SETTEXT, IntPtr.Zero, "99");

    Console.WriteLine("文本已发送");
}

[DllImport("user32.dll", SetLastError = true)]
static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

[DllImport("user32.dll", SetLastError = true)]
static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

[DllImport("user32.dll", SetLastError = true)]
static extern bool SetForegroundWindow(IntPtr hWnd);

[DllImport("user32.dll", SetLastError = true)]
static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, string lParam);
