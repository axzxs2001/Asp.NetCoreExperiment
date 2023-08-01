using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
namespace WinFormHotKey
{
    public partial class Form1 : Form
    {

        private void Form1_Load(object sender, EventArgs e)
        {
        }
        public Form1()
        {
            InitializeComponent();
            RegisterHotKey(Handle, 100, 0, Keys.F8);
        }
        protected override void WndProc(ref Message m)
        {
            const int WM_HOTKEY = 0x0312;
            switch (m.Msg)
            {
                case WM_HOTKEY:

                    break;
            }
            base.WndProc(ref m);
        }
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, Keys vk);
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

    }





}