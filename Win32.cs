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
            public Point(int x, int y) {
                this.x = x;
                this.y = y;
            }
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);

        [FlagsAttribute]
        public enum EXECUTION_STATE : uint {
            ES_AWAYMODE_REQUIRED = 0x00000040,
            ES_CONTINUOUS = 0x80000000,
            ES_DISPLAY_REQUIRED = 0x00000002,
            ES_SYSTEM_REQUIRED = 0x00000001
            // Legacy flag, should not be used.
            // ES_USER_PRESENT = 0x00000004
        }

        public static void ForceSystemAwake() {
            Win32.SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS |
                EXECUTION_STATE.ES_DISPLAY_REQUIRED |
                EXECUTION_STATE.ES_SYSTEM_REQUIRED |
                EXECUTION_STATE.ES_AWAYMODE_REQUIRED);
        }

        public static void ResetThreadExecutionState() {
            Win32.SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS);
        }
    }
}
