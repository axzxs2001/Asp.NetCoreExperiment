namespace DapperAOTAPITest.Respository
{
    public interface ITodoRespository
    {
        IEnumerable<T> Query<T>();
    }
}
