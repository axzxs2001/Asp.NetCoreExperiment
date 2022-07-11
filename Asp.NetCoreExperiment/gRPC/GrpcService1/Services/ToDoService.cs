using Grpc.Core;
using GrpcService1;

namespace GrpcService1.Services
{
    public class ToDoService : IToDoService.IToDoServiceBase
    {
        static List<ToDo> _todos = new List<ToDo>();
        private readonly ILogger<ToDoService> _logger;
        public ToDoService(ILogger<ToDoService> logger)
        {
            _logger = logger;
        }

        public override Task<AddToDoReply> AddToDo(AddToDoRequest request, ServerCallContext context)
        {
            request.ToDo.CreateOn = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.UtcNow);
            _todos.Add(request.ToDo);
            return Task.FromResult(new AddToDoReply
            {
                Result = true
            });
        }
        public override Task<RemoveToDoReply> RemoveToDo(RemoveToDoRequest request, ServerCallContext context)
        {
            return Task.FromResult(new RemoveToDoReply
            {
                Result = _todos.Remove(_todos.FirstOrDefault(s => s.Title == request.Title))
            });
        }
        public override Task<QueryToDoReply> QueryToDo(Google.Protobuf.WellKnownTypes.Empty empty, ServerCallContext context)
        {
            var reply = new QueryToDoReply();
            reply.ToDos.AddRange(_todos);
            return Task.FromResult(reply);
        }
    }

}