namespace DapperAOTAPITest.Model
{
    public class Todo
    {
        public int ID { get; set; }
    }

    public class RequestResult
    {
        public IEnumerable<Todo> Data { get; set; }
        public string Message { get; set; }
    }
}
