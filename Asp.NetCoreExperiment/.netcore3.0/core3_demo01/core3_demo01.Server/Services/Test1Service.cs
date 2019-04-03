using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test;
using Grpc.Core;

namespace core3_demo01
{
    public class Test1Service : Test1.Test1Base
    {
        
        public override Task<Outt> Send(Inn inn, ServerCallContext context)
        {          
            return Task.FromResult(new Outt
            {
                Message = "Hello " + inn.Name
            });
        }
    }
}
