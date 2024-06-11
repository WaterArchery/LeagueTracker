namespace LeagueTracker.Handlers
{
    public class RestHandler
    {
        private static RestHandler _instance;
        
        public static RestHandler GetInstance()
        {
            if (_instance == null)
                _instance = new RestHandler();
            return _instance;
        }

        public void SendJson(dynamic json)
        {
            
        }
        
    }
}