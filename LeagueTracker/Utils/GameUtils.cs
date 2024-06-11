using System.Diagnostics;
using LeagueTracker.Handlers;

namespace LeagueTracker.Utils
{
    public class GameUtils
    {
        public static void CloseLeagueGame()
        {
            Process[] processes = Process.GetProcessesByName("League of Legends");
            if (processes.Length != 0)
            {
                Logger.Log("Closing League Game Instance.");
                foreach (var process in processes)
                {
                    ProcessUtils.ForceKill(process);
                }
            }
        }
        
        public static bool IsLeagueGameActive()
        {
            Process[] processes = Process.GetProcessesByName("League of Legends");
            if (processes.Length != 0)
            {
                return true;
            }

            return false;
        }

        
    }
}