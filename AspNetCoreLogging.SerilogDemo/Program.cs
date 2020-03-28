using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using Serilog;

namespace AspNetCoreLogging.SerilogDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            //Serilog.AspNetCore
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration) //Serilog.Settings.Configuration
                //.WriteTo.Console() //These two lines are configuring logger provider programatically. But we will be doing this from config
                //.WriteTo.File()
                .CreateLogger();

            try
            {
                Log.Information(
                    "Application starting app"); // Here its not using ILogger instead its Serilog Logger. Once applicaiton starts up Serilog will get integrated into ILogger
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Fail to start");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
