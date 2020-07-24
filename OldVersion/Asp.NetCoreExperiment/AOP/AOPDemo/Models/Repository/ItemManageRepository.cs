using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AOPDemo.Models.Repository
{
    public class ItemManageRepository:IItemManageRepository
    {
       
        public string  AddItem(Item item,string token="")
        {
            return token;
        }
    }
}
