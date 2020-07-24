using System;
using System.Collections.Generic;
using System.Text;

namespace RRDemo_Entity
{
    public interface IRequestEntity
    {
        int ID { get; set; }
        string Name { get; set; }
    }
    public class RequestEntity : IRequestEntity
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public interface IResponseEntity
    {
        int ID { get; set; }
        string Name { get; set; }

        int RequestID { get; set; }
    }
    public class ResponseEntity : IResponseEntity
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public int RequestID { get; set; }
    }
}
