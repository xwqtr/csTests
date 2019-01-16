namespace ServiceHelper
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Collections.Generic;
    using System.IO;

    public class Finder
    {
        public static string GetServicePath(string serviceName)
        {
            var p = new Process()
            {
                StartInfo = new ProcessStartInfo() {
                    FileName = "sc",
                    Arguments = $"qc {serviceName}",
                    RedirectStandardOutput = true
                }
            };
            p.Start();

            var output = p.StandardOutput.ReadToEnd().Split("\r\n").Where(z=>!string.IsNullOrEmpty(z));
            var result = output.Single(z => z.Contains("BINARY_PATH_NAME")).Split(":", 2)[1].Trim();
            return result;
        }
    }
}
