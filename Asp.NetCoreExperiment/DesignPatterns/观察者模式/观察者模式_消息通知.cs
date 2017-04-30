using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.观察者模式
{
    public class 观察者模式_消息通知
    {
        public static void Start()
        {
            //var notice = new Notice() { ID = 1, Content = "2018-1-1服务暂停12个小时升级，敬请凉解！", Title = "重要通知" };

            //var noticeObserver = new PCNoticeObserver(notice);
            //notice.SendMessage += noticeObserver.SendMessage;
            //var iosObserver = new IOSNoticeObserver(notice);
            //notice.SendMessage += iosObserver.SendMessage;
            //var androidObserver = new AndroidNoticeObserver(notice);
            //notice.SendMessage += androidObserver.SendMessage;
            //var webObserver = new WebNoticeObserver(notice);
            //notice.SendMessage += webObserver.SendMessage;


            //notice.Notify();


            var download = new DownLoad() { CreateTime=DateTime .Now , Url="http://23.45.42.223/upload",Version="1.1" };
            var noticeObserver1 = new PCNoticeObserver(download);
            download.SendMessage += noticeObserver1.SendMessage;
            var iosObserver1 = new IOSNoticeObserver(download);
            download.SendMessage += iosObserver1.SendMessage;
            var androidObserver1 = new AndroidNoticeObserver(download);
            download.SendMessage += androidObserver1.SendMessage;
            var webObserver1 = new WebNoticeObserver(download);
            download.SendMessage += webObserver1.SendMessage;
            download.Notify();
        }
    }
    #region 各种观察者
    /// <summary>
    /// 观察者类
    /// </summary>
    public abstract class Observer
    {
        protected MessageObject _messagbeObject;
        public abstract void SendMessage();
    }
    /// <summary>
    /// PC通知观察者
    /// </summary>
    public class PCNoticeObserver : Observer
    {
        public PCNoticeObserver(MessageObject messagbeObject)
        {
            _messagbeObject = messagbeObject;
        }

        public override void SendMessage()
        {
            Console.WriteLine($"PC用TCPIP发送：{_messagbeObject}");
        }
    }
    /// <summary>
    /// IOS通知观察者
    /// </summary>
    public class IOSNoticeObserver : Observer
    {
        public IOSNoticeObserver(MessageObject messagbeObject)
        {
            _messagbeObject = messagbeObject;
        }

        public override void SendMessage()
        {
            Console.WriteLine($"IOS通知中心发送：{_messagbeObject}");
        }
    }
    /// <summary>
    /// Android通知观察者
    /// </summary>
    public class AndroidNoticeObserver : Observer
    {
        public AndroidNoticeObserver(MessageObject messagbeObject)
        {
            _messagbeObject = messagbeObject;
        }

        public override void SendMessage()
        {
            Console.WriteLine($"Android通知中心发送：{_messagbeObject}");
        }
    }
    /// <summary>
    /// Web通知观察者
    /// </summary>
    public class WebNoticeObserver : Observer
    {
        public WebNoticeObserver(MessageObject messagbeObject)
        {
            _messagbeObject = messagbeObject;
        }

        public override void SendMessage()
        {
            Console.WriteLine($"WebSocket通知中心发送：{_messagbeObject}");
        }
    }
    #endregion

    public delegate void SendMessageHandle();
    /// <summary>
    /// 观察者主体对象
    /// </summary>
    public abstract class MessageObject
    {
        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public event SendMessageHandle SendMessage;
        public void Notify()
        {
            SendMessage();
        }
    }
    public class Notice : MessageObject
    {
        public int ID
        { get; set; }
        public string Title
        { get; set; }
        public string Content
        {
            get; set;
        }
    }
    public class DownLoad : MessageObject
    {
        public string Url
        { get; set; }
        public DateTime CreateTime
        {
            get; set;
        }
        public string Version
        { get; set; }
    }
}
