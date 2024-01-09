namespace DapperAOTAPITest.Service
{
    public interface ITodoService
    {
        IEnumerable<T> Query<T>();
    }
}
