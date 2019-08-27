using System;
using System.Collections.Generic;

namespace GeneratePgSql.Models
{
    public partial class Ts
    {
        public long Id { get; set; }
        public int? Tradeid { get; set; }
        public string Email { get; set; }
        public int? Num { get; set; }
        public DateTime? Modified { get; set; }
    }
}
