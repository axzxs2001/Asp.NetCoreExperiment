using Orleans;
using System;
using System.Threading.Tasks;

namespace Observer_Lib
{
    public interface IChat : IGrainObserver
    {
        void ReceiveMessage(string message);
    }
    public class Chat : IChat
    {
        public  void ReceiveMessage(string message)
        {
            Console.WriteLine($"Chat中的ReceiveMessage:{message}");          
        }
    }
}
