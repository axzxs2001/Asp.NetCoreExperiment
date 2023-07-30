using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaitForm
{
    public static class LoadingManage
    {
        static LoadingForm _loading;

        public static void Show()
        {
            Task.Run(() =>
            {
                _loading = new LoadingForm();
                _loading.ShowDialog();
            });

        }
        public static void Close()
        {
            Task.Run(() =>
            {
                while (_loading == null || _loading.IsDisposed)
                {
                    Thread.Sleep(10);
                }
                _loading.CloseAll();
            });


        }
        public static string Message
        {
            set
            {
                while (_loading == null||_loading.IsDisposed)
                {
                    Thread.Sleep(10);
                }
                Task.Run(() =>
                {
                    _loading.Message = value;
                });

            }
        }
    }
}
