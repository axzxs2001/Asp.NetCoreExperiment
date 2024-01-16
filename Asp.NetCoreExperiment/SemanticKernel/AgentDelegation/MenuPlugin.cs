using Microsoft.SemanticKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentDelegation
{
    public sealed class MenuPlugin
    {
        [KernelFunction, Description("提供菜单中的特色菜列表。")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1024:Use properties where appropriate", Justification = "Too smart")]
        public string GetSpecials()
        {
            return @"
特色汤：蛤蜊浓汤
特色沙拉：科布沙拉
特色饮品：印度奶茶
";
        }

        [KernelFunction, Description("提供所请求菜单项的价格。")]
        public string GetItemPrice(
            [Description("菜单项的名称。")]
            string menuItem)
        {
            return "$9.99";
        }
    }
}
