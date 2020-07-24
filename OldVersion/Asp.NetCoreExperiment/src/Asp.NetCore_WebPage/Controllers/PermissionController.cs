using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Asp.NetCore_WebPage.Model.Repository;
using Asp.NetCore_WebPage.Model;


namespace Asp.NetCore_WebPage.Controllers
{
    /// <summary>
    /// 权限Controller
    /// </summary>
    public class PermissionController : Controller
    {
        IPermissionResitory _permissionResitory;
        public PermissionController(IPermissionResitory permissionResitory)
        {
            _permissionResitory = permissionResitory;
        }
        /// <summary>
        /// 添加用户get
        /// </summary>
        /// <returns></returns>
        [HttpGet("adduser")]
        public IActionResult AddUser()
        {
            return View();
        }
        /// <summary>
        /// 添加用户post
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost("adduser")]
        public IActionResult AddUser(string userName, string password)
        {
            var result = _permissionResitory.AddUser(new User { UserName = userName, Password = password });
            return View();
        }
        /// <summary>
        /// 用户列表get
        /// </summary>
        /// <returns></returns>
        [HttpGet("users")]
        public IActionResult Users()
        {
            var users = _permissionResitory.GetUsers();
            return View(users);
        }
    }
}
