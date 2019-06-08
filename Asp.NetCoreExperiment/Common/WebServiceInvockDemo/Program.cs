using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServiceInvockDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var webServiceAgent = new WebServiceAgent(@"http://211.88.20.132:8040/services/syncServiceStation");
            var requestString = new StringBuilder();
            requestString.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            requestString.AppendLine("<serviceStation>");
            requestString.AppendLine("<userId>1409270001</userId>");
            requestString.AppendLine("<rptDate>2019-06-08</rptDate>");
            requestString.AppendLine("</serviceStation>");
            var responseString = webServiceAgent.Invoke("syncServiceStationOperation", requestString.ToString());
            Console.WriteLine(responseString);
        }
    }
}
