using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestIpChangeNotify
{
    class Program
    {
        static void Main(string[] args)
        {
            #if DEBUG
            if (Environment.UserInteractive)
            {
                WindowsIpChangeNotifyService.IpChangeNotifyService service1 = new WindowsIpChangeNotifyService.IpChangeNotifyService();
                service1.TestStartupAndStop(args);
            }
            else
            {
                // Put the body of your old Main method here.
            }
            #endif
        }
    }
}