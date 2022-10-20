using D2rMultiHelper.modules;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using static D2rMultiHelper.modules.ProcessManager;
using System;
using System.Threading;

namespace D2rMultiHelper
{
    class Program
    {
        [DllImport("user32.dll")]
        public static extern bool SetWindowText(IntPtr hWnd, string text);

        public static string newTitle = "D2R MultiLoaded by YMD2RML";

        static void Main(string[] args)
        {

            Console.WriteLine("================================================");
            Console.WriteLine("Welcome to D2R Multi Instance Helper");
            Console.WriteLine("Developer : Ymson (Nickname : 코더657)");
            Console.WriteLine("email : ymson1984@gmail.com");
            Console.WriteLine("Copyright(c) 2022 Ymson all rights reserved");
            Console.WriteLine("================================================");
            Console.WriteLine("");

            while (true)
            {
                Process[] processList = Process.GetProcessesByName("D2R");

                foreach (Process p in processList)
                {

                    if (newTitle.Equals(p.MainWindowTitle)) continue;

                    IntPtr pSysHandles = ProcessManager.GetAllHandles();

                    if (ProcessManager.KillHandle(p, "Instances") > 0)
                    {
                        Console.WriteLine("DiabloII Check For Other Instances Handle Killed");
                    }
                    else
                    {
                        Console.WriteLine("Already DiabloII Check For Other Instances Handle Killed");
                    }

                    SetWindowText(p.MainWindowHandle, newTitle);
                }

                Thread.Sleep(1000);
            }
        }
    }
}
