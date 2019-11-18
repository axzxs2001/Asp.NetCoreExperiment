using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace prometheus_demo03.Monitor
{
    public interface IMonitoringService
    {
        bool Monitor(string httpMethod, PathString path);
    }
}
