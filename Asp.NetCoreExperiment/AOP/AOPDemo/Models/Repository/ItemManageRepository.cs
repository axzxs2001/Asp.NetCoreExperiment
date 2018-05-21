using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AOPDemo.Models.Repository
{
    public class ItemManageRepository:IItemManageRepository
    {
        //TODO 这里不同浏览器有相同的值，有But
        public bool AddItem(Item item,string token="")
        {
            return true;
        }
    }
}
