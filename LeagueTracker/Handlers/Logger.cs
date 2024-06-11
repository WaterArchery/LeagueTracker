using System;
using System.IO;

namespace LeagueTracker.Handlers
{
    public class Logger
    {
        private static string _fileName;
        
        public static void Log(string log)
        {
            DateTime now = DateTime.Now;
            string day = now.Day.ToString().Length == 1 ? "0" + now.Day : now.Day.ToString();
            string month = now.Month.ToString().Length == 1 ? "0" + now.Month : now.Month.ToString();
            string hour = now.Hour.ToString().Length == 1 ? "0" + now.Hour : now.Hour.ToString();
            string minute = now.Minute.ToString().Length == 1 ? "0" + now.Minute : now.Minute.ToString();
            string second = now.Second.ToString().Length == 1 ? "0" + now.Second : now.Second.ToString();
            string date = $"[{day}/{month} {hour}:{minute}:{second}]";
            log = date + " " + log;
            WriteToTextFile(log); 
            Console.WriteLine(log);
        }
        
        public static void Debug(string log)
        {
            //Log(log);
        }
        
        public static void Error(string log)
        {
            Console.WriteLine(log);
            WriteToTextFile(log);
        }

        public static void WriteToTextFile(string log)
        {
            try
            {
                if (_fileName == null)
                {
                    CreateLogFolder();
                    AssignTextFile();
                    WriteToTextFile(log);
                    return;
                }

                using (StreamWriter outputFile = new StreamWriter(_fileName, true))
                {
                    outputFile.WriteLine(log);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void CreateLogFolder()
        {
            bool exists = Directory.Exists(@".\logs");

            if(!exists)
                Directory.CreateDirectory(@".\logs");
        }

        public static void AssignTextFile()
        {
            long date = DateTime.Now.Ticks;
            _fileName = @".\logs\" + date + ".txt";
        }
    }
}