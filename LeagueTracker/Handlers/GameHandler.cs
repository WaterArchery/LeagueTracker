using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LeagueTracker.Memory;
using LeagueTracker.Models;
using LeagueTracker.Utils;

namespace LeagueTracker.Handlers
{
    public class GameHandler
    {
        private static GameHandler _instance;
        
        public static GameHandler GetInstance()
        {
            if (_instance == null)
                _instance = new GameHandler();
            return _instance;
        }

        public void HandleFirstStart()
        {
            Logger.Log("First start detected.");
            Logger.Log("Checking if League of Legends is started.");
            while (!GameUtils.IsLeagueGameActive())
            {
                Logger.Log("League game not detected. Waiting 5 seconds to try again.");
                Thread.Sleep(5000);
            }
            Logger.Log("");
            Logger.Log("League game is successfully detected.");
            Loop();
        }

        private void Loop()
        {
            MemoryReader memoryReader = MemoryReader.GetInstance();
            memoryReader.UpdatePlayers();
            List<Player> players = memoryReader.GetPlayers();
            Print(players);
            BotUtils.SendHook(players);
            Thread.Sleep(5000);
            Task.Run(Loop);
        }

        private void Print(List<Player> players)
        {
            Console.Clear();
            Logger.Log("Team 1:");
            foreach (Player player in players)
            {
                if (player.Team == 100)
                {
                    Logger.Log(player.ToString());
                }
            }
            Logger.Log("");
            Logger.Log("Team 2:");
            foreach (Player player in players)
            {
                if (player.Team != 100)
                {
                    Logger.Log(player.ToString());
                }
            }
        }
    }
    
}