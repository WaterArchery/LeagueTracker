using System;

namespace LeagueTracker.Handlers
{
    public class StartStopHandler
    {
        private static StartStopHandler instance;
        
        public static StartStopHandler GetInstance()
        {
            if (instance == null)
                instance = new StartStopHandler();
            return instance;
        }
        
        public void HandleStop()
        {
            Environment.Exit(0);
        }
    }
}