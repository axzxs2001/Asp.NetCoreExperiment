using Orleans;
using System;
using System.Threading.Tasks;

namespace Demo003
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.ReadLine();
        }


    }

    public interface IPlayerGrain : IGrainWithGuidKey
    {
        Task<IGameGrain> GetCurrentGame();
        Task JoinGame(IGameGrain game);
        Task LeaveGame(IGameGrain game);
    }

    public interface IGameGrain
    {
        Guid GetPrimaryKey();
    }
    public class PlayerGrain : Grain, IPlayerGrain
    {
        private IGameGrain currentGame;

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

