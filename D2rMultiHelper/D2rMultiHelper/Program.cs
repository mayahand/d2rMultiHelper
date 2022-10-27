using System;
using D2rMultiHelper.modules;
using System.Runtime.InteropServices;
using static D2rMultiHelper.modules.ProcessManager;
using System.Threading;

using System.Diagnostics;
using System.Security.Principal;

namespace D2rMultiHelper
{
    class Program
    {
        [DllImport("user32.dll")]
        public static extern bool SetWindowText(IntPtr hWnd, string text);

        public static string newTitle = "D2R MultiLoaded by YMD2RML";

        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("================================================");
            Console.WriteLine("Welcome to D2R Multi Instance Helper");
            Console.WriteLine("Developer : Ymson (Nickname : 코더657)");
            Console.WriteLine("email : ymson1984@gmail.com");
            Console.WriteLine("Copyright(c) 2022 Ymson all rights reserved");
            Console.WriteLine("================================================");
            Console.WriteLine("");

            if (!IsAdministrator())
            {
                try
                {
                    ProcessStartInfo procInfo = new ProcessStartInfo()
                    {
                        UseShellExecute = true,
                        FileName = System.Reflection.Assembly.GetEntryAssembly().Location,
                        WorkingDirectory = Environment.CurrentDirectory,
                        Verb = "runas"
                    };

                    Process.Start(procInfo);
                }
                catch(Exception e)
                {
                    Console.WriteLine("이 프로그램은 관리자 권한으로 실행되어야 합니다.");
                    Console.WriteLine(e.ToString());
                    Console.Read();
                    Environment.Exit(0);
                }
            }

            Console.WriteLine("관리자 권한 실행 완료.");

            while (true)
            {
                Process[] processList = Process.GetProcessesByName("D2R");

                foreach (Process p in processList)
                {

                    //if (newTitle.Equals(p.MainWindowTitle) || String.IsNullOrEmpty(p.MainWindowTitle)) continue;
                    if (String.IsNullOrEmpty(p.MainWindowTitle)) continue;

                    //IntPtr pSysHandles = ProcessManager.GetAllHandles();

                    if (ProcessManager.KillHandle(p, "DiabloII Check For Other Instances"))
                    {
                        Console.WriteLine("PID("+p.Id+") : DiabloII Check For Other Instances Handle Killed");
                    }

                    //SetWindowText(p.MainWindowHandle, newTitle);
                }

                GC.Collect();
                Thread.Sleep(1000);
            }
        }

        public static bool IsAdministrator()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();

            if (null != identity)
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
#pragma warning disable CA1416 // 플랫폼 호환성 유효성 검사
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
#pragma warning restore CA1416 // 플랫폼 호환성 유효성 검사
            }

            return false;
        }
    }
}
