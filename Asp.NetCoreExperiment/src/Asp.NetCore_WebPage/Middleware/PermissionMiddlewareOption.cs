using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.NetCore_WebPage.Middleware
{
    /// <summary>
    /// 权限中间件选项
    /// </summary>
    public class PermissionMiddlewareOption
    {
        /// <summary>
        /// 登录action
        /// </summary>
        public string LoginAction
        { get; set; }
        /// <summary>
        /// 无权限导航action
        /// </summary>
        public string NoPermissionAction
        { get; set; }
    }
}
