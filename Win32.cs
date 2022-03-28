using System;
using System.Runtime.InteropServices;

namespace WindowsCursorPosition
{
    public class Win32 {
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out Point lpPoint);
        [DllImport("user32.dll")]
        public static extern long SetCursorPos(int x, int y);
        [DllImport("user32.dll")]
        public static extern bool ClientToScreen(IntPtr hWnd, ref Point point);
        [StructLayout(LayoutKind.Sequential)]
        public struct Point {
            public int x;
            public int y;
            public Point(int X, int Y) {
                x = X;
                y = Y;
            }
        }
    }
}