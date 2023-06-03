using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPT_Demo02_Ext
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class YBDateTimeAttribute : Attribute
    {
        public YBDateTimeAttribute(string format)
        {
            this.Format = format;
        }
        public string Format { get; private set; }
    }
}

