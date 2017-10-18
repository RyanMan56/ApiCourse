using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CityInfo.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                // For core 1.0 (automagically included in core 2.0):
                //.UseKestrel() // Linux hosting
                //.UseContentRoot(Directory.GetCurrentDirectory()) // Uses the current directory (not same as web root)
                //.UseIISIntegration() // Windows hosting
                .UseStartup<Startup>()
                .Build(); // Builds the web app
    }
}
