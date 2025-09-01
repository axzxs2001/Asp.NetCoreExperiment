using A2A;


Console.WriteLine("回车开始");
Console.ReadLine();
var cardResolver = new A2ACardResolver(new Uri("http://localhost:5100/"));
var agentCard = await cardResolver.GetAgentCardAsync();
Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(agentCard));
var client = new A2AClient(new Uri(agentCard.Url));

// Send message
var response = await client.SendMessageAsync(new MessageSendParams
{
    Message = new Message
    {
        Role = MessageRole.User,
        Parts = [new TextPart { Text = "Hello!" }]
    }
});
if (response is Message)
{
    var message = response as Message;
    Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(message));
}
Console.ReadLine();