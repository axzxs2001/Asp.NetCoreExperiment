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

        [HttpGet("adduser")]
        public IActionResult AddUser()
        {
            return View();
        }
        [HttpPost("adduser")]
        public IActionResult AddUser(string userName,string password)
        {
          var result=  _permissionResitory.AddUser(new User { UserName = userName, Password = password });
            return View();
        }

        [HttpGet("users")]
        public IActionResult Users()
        {
            var users = _permissionResitory.GetUsers();
            return View(users);
        }
    }
}
