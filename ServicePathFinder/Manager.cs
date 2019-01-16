using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ServiceHelper
{
    public static class Manager
    {

        public static void RestartService(string folderPath,string serviceName)
        {
            var p = Process.Start("sc", $"stop {serviceName}");
            p.WaitForExit();
            var p2 = Process.Start("sc", $"start {serviceName}");
            p2.WaitForExit();
        }
    }
}
