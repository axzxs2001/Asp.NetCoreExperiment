using System;
using System.Collections.Generic;
using System.IO;
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

            foreach(var meth in webServiceAgent.Methods)
            {
                Console.WriteLine(meth.Name);
            }

            Console.WriteLine("----------------");
            var requestString = new StringBuilder();
            requestString.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            requestString.AppendLine("<serviceStation>");
            requestString.AppendLine("<userId>1409270111</userId>");
            requestString.AppendLine("<rptDate>2019-06-08</rptDate>");
            requestString.AppendLine("</serviceStation>");
            var par = File.ReadAllText(Directory.GetCurrentDirectory() + "/par.txt");
            var responseString1 = webServiceAgent.Invoke("syncServiceStationOperation", par);
            Console.WriteLine(responseString1);

            Console.WriteLine("----------------");
            var responseString2 = webServiceAgent.Invoke("syncServiceStationOperation", requestString.ToString());
            Console.WriteLine(responseString2);
        }
    }
}
