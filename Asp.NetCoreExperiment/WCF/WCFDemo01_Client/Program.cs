using ToDoServiceReference;

var client = new ToDoServiceClient(ToDoServiceClient.EndpointConfiguration.WSHttpBinding_IToDoService, "https://localhost:5001/ToDoService/WSHttps");

while (true)
{
    Console.WriteLine("1、Add ToDo  2、Get ToDoList 3、Remove ToDo");
    switch (Console.ReadLine())
    {
        case "1":
            var addResult = await client.AddAsync(new AddRequest
            {
                item = new Item { Title = "title_" + DateTime.Now.ToString("yyyyMMddHHmmss"), Description = "description_" + DateTime.Now.ToString("yyyyMMddHHmmssffffff") }
            });
            Console.WriteLine(addResult.AddResult);
            break;
        case "2":
            var listResult = await client.GetListAsync(new GetListRequest { });
            foreach (var item in listResult.GetListResult)
            {
                Console.WriteLine(item);
            }
            break;
        case "3":
            var delResult = await client.RemoveAsync(new RemoveRequest
            {
                title = Console.ReadLine()
            });
            Console.WriteLine(delResult.RemoveResult);
            break;
        default:
            Console.WriteLine("input error");
            break;
    }
}


