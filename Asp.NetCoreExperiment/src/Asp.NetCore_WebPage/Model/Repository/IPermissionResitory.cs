using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.NetCore_WebPage.Model.Repository
{
    public interface IPermissionResitory
    {
        bool AddUser(User user);
        bool ModifyUser(User user);

        bool Remove(int id);

        List<User> GetUsers();

        User GetUser(int id);
    }
}
