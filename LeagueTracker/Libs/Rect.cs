using System.Runtime.InteropServices;

namespace LeagueTracker.Libs
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Rect
    {
        public int Left, Top, Right, Bottom;
    }
}