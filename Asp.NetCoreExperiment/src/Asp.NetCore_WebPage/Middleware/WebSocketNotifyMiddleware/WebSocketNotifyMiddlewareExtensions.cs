using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace Asp.NetCore_WebPage.Middleware
{
    /// <summary>
    /// websocket通知中间件扩展
    /// </summary>
    public static class WebSocketNotifyMiddlewareExtensions
    {
        /// <summary>
        /// 使用websocket通知
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseWebSocketNotify(
          this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<WebSocketNotifyMiddleware>();
        }
    }
}
