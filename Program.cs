using System;
using System.IO;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using System.Runtime.InteropServices;

namespace WindowsCursorPosition
{
    public class Program {

        private static int c;

        static void Main(string[] args) {
            Console.WriteLine("WindowsCursorPosition started at {0:HH:mm:ss.fff}\n", DateTime.Now);
            Console.WriteLine("Press Enter key to exit the application...\n");

            c = 0;
            
            System.Timers.Timer timer = new System.Timers.Timer(1000);
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;

            Win32.ForceSystemAwake();

            Console.ReadLine();

            timer.Stop();
            timer.Dispose();

            Win32.ResetThreadExecutionState();

            Console.WriteLine("Terminating application...");
            //Thread.Sleep(2000);
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e) {
            Console.WriteLine(e.SignalTime);
            
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) return;
            //Win32.Point point = new Win32.Point(xPos, yPos);
            Win32.Point point = default;
            Win32.GetCursorPos(out point);

            if (c % 2 == 0) {
                Console.WriteLine(c);
                point.x += 18;
                c++;
            } else {
                Console.WriteLine(c);
                point.x -= 18;
                c = 0;
            }
            
            //Win32.ClientToScreen(this.handle, ref point);
            Win32.SetCursorPos(point.x, point.y);
        }
    }
}
