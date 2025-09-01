using A2A;
using A2A.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

var taskManager = new TaskManager();
var agent = new EchoAgent();
agent.Attach(taskManager);

app.MapA2A(taskManager, "/echo");

app.Run();

public class EchoAgent
{
    public void Attach(ITaskManager taskManager)
    {
        taskManager.OnMessageReceived = ProcessMessageAsync;
        taskManager.OnAgentCardQuery = GetAgentCardAsync;
    }

    private Task<Message> ProcessMessageAsync(MessageSendParams messageSendParams, CancellationToken cancellationToken)
    {
        var text = messageSendParams.Message.Parts.OfType<TextPart>().First().Text;
        return Task.FromResult(new Message
        {
            Role = MessageRole.Agent,
            MessageId = Guid.NewGuid().ToString(),
            ContextId = messageSendParams.Message.ContextId,
            Parts = [new TextPart { Text = $"Echo: {text}" }],             
        });
    }

    private Task<AgentCard> GetAgentCardAsync(string agentUrl, CancellationToken cancellationToken)
    {
        return Task.FromResult(new AgentCard
        {
            Name = "Echo Agent",
            Description = "Echoes messages back to the user",
            Url = agentUrl,
            Version = "1.0.0",
            DefaultInputModes = ["text"],
            DefaultOutputModes = ["text"],
            Capabilities = new AgentCapabilities { Streaming = true }
        });
    }
}