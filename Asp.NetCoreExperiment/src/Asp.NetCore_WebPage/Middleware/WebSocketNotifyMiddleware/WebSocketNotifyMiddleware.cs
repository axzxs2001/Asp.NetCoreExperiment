using Microsoft.AspNetCore.Http;
using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace Asp.NetCore_WebPage.Middleware
{
    /// <summary>
    /// websocket中间件
    /// </summary>
    public class WebSocketNotifyMiddleware
    {
        /// <summary>
        /// 管道代理对象
        /// </summary>
        private readonly RequestDelegate _next;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        public WebSocketNotifyMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        /// <summary>
        /// 中间件调用
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task Invoke(HttpContext context)
        {
            //判断是否为websocket请求
            if (context.WebSockets.IsWebSocketRequest)
            {
                //接收客户端
                var webSocket = context.WebSockets.AcceptWebSocketAsync().Result;
                //启用一个线程处理接收客户端数据
                new Thread(Accept).Start(webSocket);
                while (webSocket.State == WebSocketState.Open)
                {
                    webSocket.SendAsync(new ArraySegment<byte>(System.Text.Encoding.UTF8.GetBytes($"{DateTime.Now}")), System.Net.WebSockets.WebSocketMessageType.Text, true, CancellationToken.None);
                    Thread.Sleep(1000);
                }
            }
            return this._next(context);

        }
        /// <summary>
        /// 接收客户端数据方法
        /// </summary>
        /// <param name="obj"></param>
        void Accept(object obj)
        {
            var webSocket = obj as WebSocket;
            while (true)
            {
                var acceptArr = new byte[1024];

                var result = webSocket.ReceiveAsync(new ArraySegment<byte>(acceptArr), CancellationToken.None).Result;

                var acceptStr = System.Text.Encoding.UTF8.GetString(acceptArr).Trim(char.MinValue);
                Console.WriteLine("收到信息：" + acceptStr);
            }

        }
    }
}
