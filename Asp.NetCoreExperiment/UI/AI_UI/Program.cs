using System.Runtime.InteropServices;


const int WM_SETTEXT = 0x000C;

string windowTitle = "加入会议";

// 找到目标窗口句柄
IntPtr hwnd = FindWindow(null, windowTitle);
if (hwnd == IntPtr.Zero)
{
    Console.WriteLine("未找到目标窗口");
    return;
}


// 替换为目标文本框的类名或标题，如果不确定，可以使用Spy++等工具查找
IntPtr hwndChild = FindWindowEx(hwnd, IntPtr.Zero, "", null);

//hwndChild = IntPtr.Parse("0x0000000000080008");
hwndChild = new IntPtr(Convert.ToInt64("80008", 16));
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


[DllImport("user32.dll", SetLastError = true)]
static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

[DllImport("user32.dll", SetLastError = true)]
static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

[DllImport("user32.dll", SetLastError = true)]
static extern bool SetForegroundWindow(IntPtr hWnd);

[DllImport("user32.dll", SetLastError = true)]
static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, string lParam);
