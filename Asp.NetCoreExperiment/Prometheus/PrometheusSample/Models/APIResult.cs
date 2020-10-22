using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrometheusSample.Models
{
    public class APIResult
    {
        public bool Result { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }
    }
}
