using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace TTT.ABC
{
    public class MyRequest : IRequest<MyResponse>
    {
        public string Message
        { get; set; }
    }
}
