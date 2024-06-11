using System.Collections.Generic;
using LeagueTracker.Handlers;
using LeagueTracker.Memory;
using LeagueTracker.Models;
using LeagueTracker.Settings;

namespace LeagueTracker.Utils
{
    public class BotUtils
    {
        public static void SendHook(List<Player> players)
        { 
            using (dWebHook dcWeb = new dWebHook())
            {
                MemoryReader reader = MemoryReader.GetInstance();
                float gameTime = reader.GetGameTime();
                int rawMinute = (int) gameTime / 60;
                int rawSecond = (int) gameTime % 60;
                string minute = rawMinute < 10 ? "0" + rawMinute : rawMinute + "";
                string second = rawSecond < 10 ? "0" + rawSecond : rawSecond + "";
                
                string time = "[Game Time: **" + minute + ":" + second + "**]";
                dcWeb.ProfilePicture = "UPDATE ME";
                dcWeb.UserName = "Game Info";
                dcWeb.WebHook = Constants.WEBHOOK_URL;
                string message = ".\n";
                message = message + time + "\n";
                message = message + "**Team 1**" + "\n";
                foreach (Player player in players)
                {
                    if (player.Team == 100)
                    {
                        message = message + player + "\n";
                    }
                }
                message = message + "\n";
                message = message + "**Team 2**" + "\n";
                foreach (Player player in players)
                {
                    if (player.Team != 100)
                    {
                        message = message + player + "\n";
                    }
                }
                dcWeb.SendMessage(message);
            }
        }
        
    }
}