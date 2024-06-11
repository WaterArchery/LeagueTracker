using System;
using System.Numerics;
using LeagueTracker.Libs;
using LeagueTracker.Memory;

namespace LeagueTracker.Models
{
    public class MemoryObject
    {
        private Vector3 pos;
        private bool targetable;
        private bool visibility;
        private int team;
        private float health;
        private float maxHealth;
        private IntPtr address;
        
        public MemoryObject(IntPtr addressBase)
        {
            this.address = addressBase;
        }
        
        public void Update()
        {
            MemoryReader memory = MemoryReader.GetInstance();
            if (memory != null)
            {
                Team = memory.Process.Read<int>(address + Offsets.Team);
                Targetable = memory.Process.Read<bool>(address + Offsets.Targetable);
                Visibility = memory.Process.Read<bool>(address + Offsets.Visibility);
                Health = memory.Process.Read<float>(address + Offsets.Health);
                MaxHealth = memory.Process.Read<float>(address + Offsets.MaxHealth);
                Pos = memory.Process.Read<Vector3>(address + Offsets.Position);
            }
        }

        public override string ToString()
        {
            return "Object Health: " + Health + " Object Max Health: " + MaxHealth + " Position: " + Pos + " Team: " + team.ToString() + " Targetable: " + targetable.ToString();
        }
        
        public Vector3 Pos
        {
            get => pos;
            set => pos = value;
        }

        public float Health
        {
            get => health;
            set => health = value;
        }
        
        public bool Targetable
        {
            get => targetable;
            set => targetable = value;
        }

        public float MaxHealth
        {
            get => maxHealth;
            set => maxHealth = value;
        }

        public IntPtr Address
        {
            get => address;
            set => address = value;
        }

        public bool Visibility
        {
            get => visibility;
            set => visibility = value;
        }

        public int Team
        {
            get => team;
            set => team = value;
        }
    }
}