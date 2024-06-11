using System;
using System.Collections.Generic;
using LeagueTracker.Libs;
using LeagueTracker.Memory;

namespace LeagueTracker.Models
{
    public class Player : MemoryObject
    {

        public List<Spell> MemSpells = new List<Spell>();
        public int pIndex;
        public bool isValid = true;
        public float Mana;
        public float MaxMana;
        public String Name;
        
        public Player(IntPtr addressBase) : base(addressBase)
        {
            Address = addressBase;
        }
         
        public new void Update()
        {
            base.Update();
            UpdateSpells();
            MemoryReader memory = MemoryReader.GetInstance();
            AssignName();
            pIndex = memory.Process.Read<int>(Address + Offsets.Index);
            Mana = memory.Process.Read<float>(Address + Offsets.Mana);
            MaxMana = memory.Process.Read<float>(Address + Offsets.MaxMana);
        }

        public void UpdateSpells()
        {
            MemoryReader memory = MemoryReader.GetInstance();
            var book = (Address + Offsets.SpellBook);
            if (MemSpells.Count == 0)
            {
                for (int i = 0; i < 6; i++)
                {
                    Spell spell = new Spell(i);
                    MemSpells.Insert(i, spell);
                }
            }

            for (int i = 0; i < 6; i++)
            {
                Spell spell = MemSpells[i];
                spell.Update(memory, book);
            }
            
        }

        public void AssignName()
        {
            MemoryReader memory = MemoryReader.GetInstance();
            Name = "";
            int i = 0;
            while (true)
            {
                char nameChar = memory.Process.Read<char>(Address + Offsets.Name + i);
                Name = Name + nameChar;
                i++;
                if (nameChar == '\0')
                {
                    break;
                }
            }

            Name = Name.Replace(" ", "");
        }
        
        public override string ToString()
        {
            string str = "**Name: " + Name 
                                  + "** Health: " + (int) Health + "/" + (int) MaxHealth + "(%" + (int) ((Health / MaxHealth)  * 100) + ")" 
                                  + " **Mana: " + (int) Mana + "/" + (int) MaxMana + "(%" + (int) ((Mana / MaxMana) * 100) + ")" 
                                  + " **Ulti: " + (MemSpells[3].Cooldown < 0 ? "Ready" : (int) MemSpells[3].Cooldown + "")
                                  + " **Sum 1: " + (MemSpells[4].Cooldown < 0 ? "Ready" : (int) MemSpells[4].Cooldown + "")
                                  + " **Sum 2: " + (MemSpells[5].Cooldown < 0 ? "Ready" : (int) MemSpells[5].Cooldown + "");
            return str;
        }

        public List<Spell> MemSpells1
        {
            get => MemSpells;
            set => MemSpells = value;
        }
    }
}