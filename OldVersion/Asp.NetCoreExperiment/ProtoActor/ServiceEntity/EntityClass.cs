using System;

namespace ServiceEntity
{
    public class HelloRequest
    {
        public int Amount
        { get; set; }
        public string Message
        {
            get; set;
        }
    }

    public class HelloResponse
    {
        public string Message
        { get; set; }
    }
}
