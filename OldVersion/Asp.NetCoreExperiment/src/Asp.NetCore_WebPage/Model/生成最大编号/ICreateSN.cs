using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.NetCore_WebPage.Model.生成最大编号
{
    public interface ICreateSN
    {
        string GetSN(string typeName);
    }
}
