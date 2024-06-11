namespace LeagueTracker.Libs
{
    public class Offsets
    {
        public static int LocalPlayer = 0x23524a8;						//48 8B 05 ? ? ? ? 4C 8B D2 4C 8B C1
        public static int GameTime = 0x233e7e8;							//F3 0F 5C 35 ? ? ? ? 0F 28 F8
        public static int HeroList = 0x232fac0;							//48 8B 05 ? ? ? ? 45 33 E4 0F 57 C0
        public static int MinionList = 0x2334EE0;						//48 8B 0D ? ? ? ? E8 ? ? ? ? E8 ? ? ? ? E8 ? ? ? ? 48 8B C8
        public static int TurretList = 0x2338490;						//48 8B 1D ? ? ? ? 48 8B 5B 28
        public static int InhibList = 0x2307610;							//48 8B 05 ? ? ? ? 48 89 7C 24 ? 48 8B 58 08
        
        // object
        public static int Index = 0x10;
        public static int Team = 0x3C;
        public static int Health = 0x11E0;
        public static int Mana = 0x378;
        public static int MaxHealth = 0x1208;
        public static int MaxMana = 0x3A0;
        public static int Position = 0x220;
        public static int Visibility = 0x348;
        public static int Targetable = 0xEE0;
        public static int SpellBook = 0x3C58;
        public static int Inventory = 0x4070;
        public static int Name = 0x43D8;
        
        // Inventory
        public static int Gold = 0x10;
        
        // Spell
        public static int SpellBookSpellSlot = 0x6D0;
        public static int SpellInfo = 0x130;
        public static int SpellData = 0x60;
        public static int SpellLevel = 0x28;
        public static int SpellTime = 0x30;
        public static int SpellName = 0x28;
    }
}