using System;
using System.Runtime.InteropServices;

namespace LeagueTracker.Libs
{
    public class Win32
    {
        [DllImport("User32.Dll")]
        public static extern long SetCursorPos(int x, int y);

        [DllImport("User32.Dll")]
        public static extern bool ClientToScreen(IntPtr hWnd, ref Point point);
    }
    
    [StructLayout(LayoutKind.Sequential)]
    public struct Point
    {
        public int x, y;
        
        public Point(int X, int Y)
        {
            x = X;
            y = Y;
        }
        
    }
}