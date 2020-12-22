using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web001.Models;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Logging;

namespace Web001.Services
{
    public interface IUserService
    {

        Task<List<UserModel>> GetUsersAsync(string name);

    }
}
