using DapperAOTAPITest.Respository;

namespace DapperAOTAPITest.Service
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRespository _respositor;
        public TodoService(ITodoRespository respository)
        {
            _respositor = respository;
        }
        public IEnumerable<T> Query<T>()
        {
            return _respositor.Query<T>();
        }
    }
}
