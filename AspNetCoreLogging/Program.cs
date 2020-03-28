using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AspNetCoreLogging
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging((context, logging) =>
                {
                    logging.ClearProviders();
                    logging.AddConfiguration(context.Configuration.GetSection("Logging"));
                    logging.AddDebug();
                    logging.AddConsole(); 
                    //EventSource, EventLog, TraceSource, AzureAppServiceFile, AzureAppServiceBlob, ApplicationInsights
                    //?? Files, Database (3rd party provider that integrate with ILogger)
                    //No support for Async. Its intentional by Microsoft. Logging should be short running process and making it async will slow things down.
                    // In case we want to log to database then its better to include a queue in a way that a background process read the queue and log to persistence.

                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
