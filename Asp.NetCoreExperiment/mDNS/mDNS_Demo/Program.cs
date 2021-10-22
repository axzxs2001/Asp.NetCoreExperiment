using Makaretu.Dns;
using System.Net.Sockets;


var ip = "172.";
var mdns = new MulticastService();
var sd = new ServiceDiscovery(mdns);

sd.ServiceInstanceDiscovered += (s, e) =>
{
    foreach (var item in e.Message.Answers)
    {
        var content = System.Text.Json.JsonSerializer.Serialize(item);
        if (content.Contains(ip))
        {
            Console.WriteLine($"{item.Name}");
        }
    }
    if (e.Message.Answers.All(w => !w.Name.ToString().Contains("ipfs1")))
        return;


    //  Console.WriteLine($"service instance '{e.ServiceInstanceName}'");

    // Ask for the service instance details.
    mdns.SendQuery(e.ServiceInstanceName, type: DnsType.SRV);
};

mdns.AnswerReceived += (s, e) =>
{
    foreach (var item in e.Message.Answers)
    {
        var content = System.Text.Json.JsonSerializer.Serialize(item);
        if (content.Contains(ip))
        {
            Console.WriteLine(content);
        }
    }

    //if (e.Message.Answers.All(w => !w.Name.ToString().Contains("ipfs1"))) 
       // return;
    // Is this an answer to a service instance details?
    var servers = e.Message.Answers.OfType<SRVRecord>();
    foreach (var server in servers)
    {
        if (server.Name.ToString().Contains(ip))
        {
            Console.WriteLine($"host '{server.Target}' for '{server.Name}'");

            // Ask for the host IP addresses.
            mdns.SendQuery(server.Target, type: DnsType.A);
            //mdns.SendQuery(server.Target, type: DnsType.AAAA);
        }
    }

    // Is this an answer to host addresses?
    //var addresses = e.Message.Answers.OfType<AddressRecord>();
    //foreach (var address in addresses)
    //{
    //    if (address.Address.AddressFamily == AddressFamily.InterNetwork)
    //        Console.WriteLine($"host '{address.Name}' at {address.Address}");
    //}
    // Get connectionstring from DNS TXT record.
    //var txts = e.Message.Answers.OfType<TXTRecord>();
    //foreach (var txt in txts)
    //{
    //    // “connstr = Server”，获得对应connstr值
    //    Console.WriteLine($"{txt.Strings.Single(w => w.Contains("connstr")).Split('=')[1]}");
    //}
};

try
{
    mdns.Start();
    sd.QueryServiceInstances("_ipfs-discovery._udp");
    Console.ReadKey();
}
finally
{
    sd.Dispose();
    mdns.Stop();
}


static void Publish()
{
    var sd = new ServiceDiscovery();
    //发布一个服务，服务名称是有讲究的，一般都是_开头的，可以找一下相关资料
    var p = new ServiceProfile("ipfs1", "_ipfs-discovery._udp", 5010);
    p.AddProperty("connstr", "Server");
    //必须要设置这一项，否则不解析TXT记录
    sd.AnswersContainsAdditionalRecords = true;
    sd.Advertise(p);
    //sd.Announce(p);
    Console.ReadKey();
    sd.Unadvertise();
}