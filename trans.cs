using System;
using System.Runtime.InteropServices;
using System.Diagnostics;
namespace App
{
    class Program
    {
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, int bAlpha, uint dwFlags);
        [DllImport("user32.dll", SetLastError = true)]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);
        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        static void Main(string[] args)
        {
            int trans = 192;
            if(args.Length > 0){
                trans = Convert.ToInt32(args[0]);
                if(trans < 0 || trans > 255){
                    Console.WriteLine("Transparency setting between 0-255");
                    return;
                }
            }
            Program program = new Program();
            Process[] ps = Process.GetProcessesByName("gvim");
            if(ps.Length < 1){
                Console.WriteLine("Not find the process with gvim");
                return;
            } 
            bool isTrans = false;
            foreach(Process p in ps){
                isTrans = program.MakeWindowTransparent(p.MainWindowHandle, trans);
                if(isTrans){
                    Console.WriteLine("Transparency changed to " + trans +" successfully");
                    break;
                }
            }
        }
        bool MakeWindowTransparent(IntPtr hWnd, int factor)
        {
            const int GWL_EXSTYLE = (-20);
            const uint WS_EX_LAYERED = 0x00080000;
            int Cur_STYLE = GetWindowLong(hWnd, GWL_EXSTYLE);
            SetWindowLong(hWnd, GWL_EXSTYLE, (uint)(Cur_STYLE | WS_EX_LAYERED));
            const uint LWA_COLORKEY = 1;
            const uint LWA_ALPHA = 2;
            const uint WHITE = 0xffffff;
            return SetLayeredWindowAttributes(hWnd, WHITE, factor, LWA_COLORKEY | LWA_ALPHA);
        }
    }
}
