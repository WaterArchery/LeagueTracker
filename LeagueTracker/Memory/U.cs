using System;
using System.Runtime.InteropServices;
using LeagueTracker.Handlers;
using LeagueTracker.Libs;
using LeagueTracker.Utils;

namespace LeagueTracker.Memory
{
    public static class U
    {

        /// <summary>
        /// Get module by name.
        /// </summary>
        public static Module GetModule(this System.Diagnostics.Process process)
        {
            var processModule = process.GetProcessModule();
            return processModule is null || processModule.BaseAddress == IntPtr.Zero
                ? default
                : new Module(process, processModule);
        }

        /// <summary>
        /// Get process module by name.
        /// </summary>
        public static System.Diagnostics.ProcessModule GetProcessModule(this System.Diagnostics.Process process)
        {
            return process.MainModule;
        }

        /// <summary>
        /// Get if process is running.
        /// </summary>
        public static bool IsRunning(this System.Diagnostics.Process process)
        {
            try
            {
                System.Diagnostics.Process.GetProcessById(process.Id);
            }
            catch (InvalidOperationException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Read process memory.
        /// </summary>
        public static T Read<T>(this System.Diagnostics.Process process, IntPtr lpBaseAddress)
            where T : unmanaged
        {
            return Read<T>(process.Handle, lpBaseAddress);
        }

        /// <summary>
        /// Read process memory from module.
        /// </summary>
        private static int failCount;
        public static T Read<T>(this Module module, int offset)
            where T : unmanaged
        {
            if (module != null && module.Process != null && !module.Process.HasExited && module.Process.Handle != IntPtr.Zero)
            {
                failCount = 0;
                return Read<T>(module.Process.Handle, module.ProcessModule.BaseAddress + offset);
            }
            else
            {
                failCount++;
                if (module != null && module.Process != null)
                {
                    GameUtils.CloseLeagueGame();
                }
                Logger.Log($"Fail count {failCount} / 5");
                if (failCount > 5)
                {
                    Logger.Log("Closing everything");
                    StartStopHandler handler = StartStopHandler.GetInstance();
                    failCount = 0;
                    handler.HandleStop();
                }
            }

            Logger.Log("Tried to read on wrong moduleeee.");
            return default;
        }

        /// <summary>
        /// Read process memory.
        /// </summary>
        public static T Read<T>(IntPtr hProcess, IntPtr lpBaseAddress)
            where T : unmanaged
        {
            var size = Marshal.SizeOf<T>();
            var buffer = (object)default(T);
            try
            {
                User32.ReadProcessMemory(hProcess, lpBaseAddress, buffer, size, out var lpNumberOfBytesRead);
                return lpNumberOfBytesRead == size ? (T)buffer : default;
            }
            catch (Exception e)
            {
                Logger.Log("Error on read with hProcess");
                Logger.Log(e.Message);
                Logger.Log(e.StackTrace);
                return default;
            }
        }
        
    }
}