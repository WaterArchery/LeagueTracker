using System;
using System.Diagnostics;
using System.Linq;
using LeagueTracker.Handlers;

namespace LeagueTracker.Utils
{
    public class ProcessUtils
    {
        public static void ForceKill(Process proc)
        {
            if (proc == null)
            {
                proc = Process.GetProcessesByName("League of Legends").First();
            }

            if (proc == null)
            {
                Logger.Log("Tried to force kill but process is null.");
                return;
            }

            string processName = string.Empty;
            try
            {
                processName = proc.ProcessName;
            }
            catch (Exception ex) { Logger.Debug(ex.Message); }

            // ProcessId can be accessed after the process has been killed but we'll do this safely anyways.
            int pId = 0;
            try
            {
                pId = proc.Id;
            }
            catch (Exception ex) { Logger.Debug(ex.Message); }

            // Will only work if started by this instance of the dll.
            try
            {
                proc.Kill();
            }
            catch (Exception ex)
            { Logger.Debug(ex.Message); }

            // Fallback to task kill
            if (pId > 0)
            {
                var taskKilPsi = new ProcessStartInfo("taskkill");
                taskKilPsi.Arguments = $"/pid {proc.Id} /T /F";
                taskKilPsi.WindowStyle = ProcessWindowStyle.Hidden;
                taskKilPsi.UseShellExecute = false;
                taskKilPsi.RedirectStandardOutput = true;
                taskKilPsi.RedirectStandardError = true;
                taskKilPsi.CreateNoWindow = true;
                var taskKillProc = Process.Start(taskKilPsi);
                taskKillProc.WaitForExit();
                String taskKillOutput = taskKillProc.StandardOutput.ReadToEnd(); // Contains success
                String taskKillErrorOutput = taskKillProc.StandardError.ReadToEnd();
            }

            // Fallback to wmic delete process.
            if (!string.IsNullOrEmpty(processName))
            {
                // https://stackoverflow.com/a/38757852/591285
                var wmicPsi = new ProcessStartInfo("wmic");
                wmicPsi.Arguments = $@"process where ""name='{processName}.exe'"" delete";
                wmicPsi.WindowStyle = ProcessWindowStyle.Hidden;
                wmicPsi.UseShellExecute = false;
                wmicPsi.RedirectStandardOutput = true;
                wmicPsi.RedirectStandardError = true;
                wmicPsi.CreateNoWindow = true;
                var wmicProc = Process.Start(wmicPsi);
                if (wmicProc != null)
                {
                    wmicProc.WaitForExit();
                    String wmicOutput = wmicProc.StandardOutput.ReadToEnd(); // Contains success
                    String wmicErrorOutput = wmicProc.StandardError.ReadToEnd();
                    Logger.Log("vmic Output: " + wmicOutput);
                }
            }
        }
    }
}