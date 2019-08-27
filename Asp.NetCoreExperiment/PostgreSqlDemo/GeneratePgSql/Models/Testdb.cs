using System;
using System.Collections.Generic;

namespace GeneratePgSql.Models
{
    public partial class Testdb
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Pid { get; set; }
    }
}
