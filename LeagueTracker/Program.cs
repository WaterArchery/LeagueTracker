using System.Threading;
using System.Threading.Tasks;
using LeagueTracker.Handlers;

namespace LeagueTracker
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Logger.Log("Starting League Tracker");
            GameHandler gameHandler = GameHandler.GetInstance();
            Task.Run(() =>
            {
                gameHandler.HandleFirstStart();
            });
            
            SpinWait.SpinUntil(() => false);
        }
    }
}