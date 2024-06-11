using System;
using LeagueTracker.Handlers;
using LeagueTracker.Libs;
using LeagueTracker.Memory;

namespace LeagueTracker.Models
{
    public class Spell
    {
        private int level;
        private float cooldown;
        private int id;
        private string name;

        public Spell(int id)
        {
            this.id = id;
        }

        public void Update(MemoryReader memory, IntPtr spellBook)
        {
            var gameTime = memory.ModuleClient.Read<float>(Offsets.GameTime) - 3;
            var spell = memory.Process.Read<IntPtr>(spellBook + 0x8 * id);
            cooldown = memory.Process.Read<float>(spell + Offsets.SpellTime) - gameTime;
            level = memory.Process.Read<int>(spell + Offsets.SpellLevel);
        }
        
        public int Level
        {
            get => level;
            set => level = value;
        }

        public float Cooldown
        {
            get => cooldown;
            set => cooldown = value;
        }

        public int ID
        {
            get => id;
        }
    }
}