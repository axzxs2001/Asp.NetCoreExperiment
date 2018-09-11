using Orleans;
using System;
using System.Threading.Tasks;

namespace Demo003
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Hello World!");
        }



    }

    public interface IGameGrain
    {
        Guid GetPrimaryKey();
    }
    public class GameGrain : IGameGrain
    {
        public Guid GetPrimaryKey()
        {
            throw new NotImplementedException();
        }
        //public Task JoinGame(IGameGrain gameGrain)
        //{
        //    IPlayerGrain player=null;
        //    //Invoking a grain method asynchronously
        //    Task joinGameTask = player.JoinGame(this);
        //    //The await keyword effectively makes the remainder of the method execute asynchronously at a later point (upon completion of the Task being awaited) without blocking the thread.
        //    await joinGameTask;
        //    //The next line will execute later, after joinGameTask is completed.
        //    players.Add(playerId);
        //}
    }
    public interface IPlayerGrain : IGrainWithGuidKey
    {
        Task<IGameGrain> GetCurrentGame();
        Task JoinGame(IGameGrain game);
        Task LeaveGame(IGameGrain game);
    }

    public class PlayerGrain : Grain, IPlayerGrain
    {
        private IGameGrain currentGame;

        public IPlayerGrain GetPlayerGrain()
        {
            //定时器
            var dis = RegisterTimer((obj) => Task.CompletedTask, new Program(), TimeSpan.FromSeconds(10), TimeSpan.FromMinutes(1));
            //提醒
            var rem = RegisterOrUpdateReminder(new Guid().ToString(), TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(30)).Result;
            UnregisterReminder(rem);

            IPlayerGrain player = GrainFactory.GetGrain<IPlayerGrain>(new Guid());
            return player;
        }

        // Game the player is currently in. May be null.
        public Task<IGameGrain> GetCurrentGame()
        {

            return Task.FromResult(currentGame);
        }

        // Game grain calls this method to notify that the player has joined the game.
        public Task JoinGame(IGameGrain game)
        {
            currentGame = game;
            Console.WriteLine(
                "Player {0} joined game {1}",
                this.GetPrimaryKey(),
                game.GetPrimaryKey());

            return Task.CompletedTask;
        }

        // Game grain calls this method to notify that the player has left the game.
        public Task LeaveGame(IGameGrain game)
        {
            currentGame = null;
            Console.WriteLine(
                "Player {0} left game {1}",
                this.GetPrimaryKey(),
                game.GetPrimaryKey());

            return Task.CompletedTask;
        }
    }
}
