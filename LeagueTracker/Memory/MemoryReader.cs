using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using LeagueTracker.Handlers;
using LeagueTracker.Libs;
using LeagueTracker.Models;

namespace LeagueTracker.Memory
{
    public class MemoryReader
    {
        private const string NAME_PROCESS = "League of Legends";
        private static MemoryReader _instance;
        
        public static MemoryReader GetInstance()
        {
            if (_instance == null)
                _instance = new MemoryReader();
            return _instance;
        }

        public void Dispose()
        {
            Process = null;
            ModuleClient = null;
            _instance = null;
            Logger.Log("Memory reader disposed.");
        }

        private MemoryReader()
        {
            if (EnsureProcessAndModules())
            {
                Logger.Log("Created Memory Reader instance");
            }
            else
            {
                Logger.Log("Creating memory reader instance failed.");
                Dispose();
            }
        }

        private List<Player> latestPlayers = new List<Player>();
        public List<Player> GetPlayers()
        {
            List<Player> players = new List<Player>();
            foreach (Player player in latestPlayers)
            {
                if (player.isValid)
                {
                    player.Update();
                    players.Add(player);
                }
            }

            return players;
        }

        /*
         * skipped players isimli arrayi sıfırlama hiçbir zaman.
         * kontrole girdiğinde o array içinde looplanan oyuncu var mı diye kontrol et
         * varsa can değerlerini kontrol et ki ölü olup olmadığını anla.
         * Ölüyse yani canları aynıysa listede kalsın ve skiplesin.
         * Can değeri skipped arrayindekinden farklıysa listeden çıkar.
         */
        public void UpdatePlayers()
        {
            IntPtr heroList = ModuleClient.Read<IntPtr>(Offsets.HeroList);
            var pList = Process.Read<IntPtr>(heroList + 0x8);
            var pSize = Process.Read<int>(heroList + 0x10);
            for (int i = 0; i < pSize; ++i) {
                var champObject = Process.Read<IntPtr>(pList + (0x8 * i));
                Player player = new Player(champObject);
                player.Update();
                bool found = false;
                foreach (Player latestPlayer in latestPlayers)
                {
                    if (latestPlayer.pIndex == player.pIndex)
                    {
                        found = true;
                        if (Math.Abs(latestPlayer.Health - player.Health) > 0.01 || player.MaxHealth - player.Health < 20)
                        {
                            latestPlayer.isValid = true;
                        }
                        else
                        {
                            latestPlayer.isValid = false;
                        }
                        break;
                    }
                }

                if (!found)
                {
                    latestPlayers.Add(player);
                }
            }
            
        }
        
        public float GetGameTime()
        {
            return ModuleClient.Read<float>(Offsets.GameTime);
        }
        
        public bool EnsureProcessAndModules()
        {
            if (Process is null)
            {
                ModuleClient = null;
                Process = Process.GetProcessesByName(NAME_PROCESS).FirstOrDefault();
            }
            
            if (Process is null || !Process.IsRunning() || !Process.Responding)
                return false;
            
            if (ModuleClient is null)
                ModuleClient = Process.GetModule();
            
            if (ModuleClient is null)
                return false;

            return true;
        }
        
        private void InvalidateModules()
        {
            ModuleClient?.Dispose();
            ModuleClient = default;

            Process?.Dispose();
            Process = default;
        }
        
        
        public Process Process { get; private set; }

        /// <summary>
        /// Client module.
        /// </summary>
        public Module ModuleClient { get; private set; }
        
        
        /// <summary>
        /// Is game process valid?
        /// </summary>
        public bool IsValid =>  !(Process is null) && !(ModuleClient is null);
        
    }
}