using EasyHook;
using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting;
using System.Threading;

namespace EasyHookTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string channelName = null;
            var dllInterface = new DLLInterface();

            string processName = "notepad";

            var process = Process.GetProcessesByName(processName).FirstOrDefault();
            if(process == null)
            {
                ProcessStartInfo info = new ProcessStartInfo(processName);
                Process.Start(info);

                Thread.Sleep(3000);

                process = Process.GetProcessesByName(processName).First();
            }

            RemoteHooking.IpcCreateServer(ref channelName, WellKnownObjectMode.SingleCall, dllInterface);
            RemoteHooking.Inject(process.Id, "InjectA.dll", "InjectA.dll", channelName);
            Console.WriteLine("InjectedA");

            Console.ReadLine();
        }
    }
}
