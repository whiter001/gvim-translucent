using System;
using System.Runtime.InteropServices;
namespace App
{
    class Program
    {
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);
        [DllImport("user32.dll", SetLastError = true)]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);
        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        static void Main(string[] args)
        {
            byte trans = 192;
            if(args.Length > 0){
                int[] ints = Array.ConvertAll(args, arg => Convert.ToInt32(arg));
                if(ints[0]>=0 && ints[0]<=255){
                    byte[] bytes = BitConverter.GetBytes(ints[0]);
                    trans = bytes[0];
                }else{
                    Console.WriteLine("透明度的设置区间在0~255之间");
                }
            }
            Program program = new Program();
            System.Diagnostics.Process[] ps = System.Diagnostics.Process.GetProcessesByName("gvim");
            // System.Console.WriteLine(ps.Length);
            if(ps.Length < 1) return;
            foreach(System.Diagnostics.Process p in ps){
                program.MakeWindowTransparent(p.MainWindowHandle, trans);
            }
        }
        bool MakeWindowTransparent(IntPtr hWnd, byte factor)
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
