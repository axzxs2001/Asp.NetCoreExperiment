using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AOPDemo.Models.Repository
{

    [RepositoryInterceptor]
    public interface IItemManageRepository
    {
        string AddItem(Item item, string token = "");
    }
}
