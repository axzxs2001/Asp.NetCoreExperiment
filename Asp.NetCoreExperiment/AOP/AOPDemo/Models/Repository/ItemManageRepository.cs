using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AOPDemo.Models.Repository
{
    public class ItemManageRepository:IItemManageRepository
    {
        public bool AddItem(Item item)
        {
            return true;
        }
    }
}
