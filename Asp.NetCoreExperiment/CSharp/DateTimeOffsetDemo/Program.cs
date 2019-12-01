using System;
using System.Collections.Generic;
namespace DateTimeOffsetDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var dir = new Dictionary<string, object>();
            dir.Add("ID", "1");
            // dir.Add("CreateTime", DateTimeOffset.Now);
            dir.Add("ModifyTime", DateTime.Now.ToString());
            dir.Add("CreateTime", DateTimeOffset.Now.ToString("o"));//System.InvalidCastException:“Invalid cast from 'System.String' to 'System.DateTimeOffset'.”
            var classobj = Activator.CreateInstance(typeof(ABC));
            foreach (var pro in typeof(ABC).GetProperties())
            {
                var value = dir[pro.Name];
                var proobj = Convert.ChangeType(value, pro.PropertyType);
                pro.SetValue(classobj, proobj);
            }
        }
    }

    public class ABC
    {
        public int ID { get; set; }
        public DateTimeOffset CreateTime { get; set; }
        public DateTime ModifyTime { get; set; }
    }
}
