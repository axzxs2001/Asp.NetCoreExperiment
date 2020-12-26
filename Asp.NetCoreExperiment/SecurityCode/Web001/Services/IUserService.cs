using System.Collections.Generic;
using System.Threading.Tasks;
using Web001.Models;

namespace Web001.Services
{
    public interface IUserService
    {

        Task<List<UserModel>> GetUsersAsync(string name);

    }
}
