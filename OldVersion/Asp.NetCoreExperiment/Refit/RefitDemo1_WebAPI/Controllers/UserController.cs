using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RefitDemo1_WebAPI.Controllers
{
    [Authorize(Policy = "Permission")]
    public class UserController : Controller
    {

        public  static List<User> users;
        public UserController()
        {
            if (User == null)
            {
                users = new List<User>() {
                new User{ID=1,Name="张三1",Age=11 },
                new User{ID=2,Name="张三2",Age=12 },
                new User{ID=3,Name="张三3",Age=13 },
                new User{ID=4,Name="张三4",Age=14 },
                new User{ID=5,Name="张三5",Age=15 }
            };
            }
        }

        [HttpGet("/users")]
        public List<User> GetUserList()
        {
            return users;
        }

        [HttpPost("/adduser")]
        public bool AddUser([FromBody]User user)
        {
            users.Add(user);
            return true;
        }
    }

    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
