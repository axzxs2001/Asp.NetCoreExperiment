using System;
using System.Collections.Generic;

#nullable disable

namespace GraphQLDemo02
{
    public partial class DatabaseLog
    {
        public int DatabaseLogId { get; set; }
        public DateTime PostTime { get; set; }
        public string DatabaseUser { get; set; }
        public string Event { get; set; }
        public string Schema { get; set; }
        public string Object { get; set; }
        public string Tsql { get; set; }
        public string XmlEvent { get; set; }
    }
}
