using System;
using System.Collections.Generic;
using System.Text;

namespace Do.Domain.Respository.Model
{
    public class CaseModel
    {
        public string ID { get; set; }
        public string UserID { get; set; }

        public DateTimeOffset CreateTime { get; set; }
        /// <summary>
        /// 一个json属性
        /// </summary>
        public string Company { get; set; }
    }
}
