using Proto;

namespace Messages
{
    public class Ping
    {
    }

    public class Pong
    {
    }

    public class Start
    {
    }

    public class StartRemote
    {
        public PID Sender { get; set; }
    }
}