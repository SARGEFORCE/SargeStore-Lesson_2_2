using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace SargeStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args)
                .Build()
                .Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            //.ConfigureLogging(
            //    (host, log) =>
            //    {
            //        //log.ClearProviders();
            //        //log.AddConsole(opt=>opt.IncludeScopes = true);
            //        //log.AddDebug();
            //        //log.AddFilter("System", LogLevel.Error); //общий фильтр
            //        //log.AddFilter<ConsoleLoggerProvider>("Microsoft", LogLevel.Error); //фильтр для провайдера
            //        log.AddFilter<ConsoleLoggerProvider>(
            //            (NameSpace, level) => !NameSpace.StartsWith("Microsoft") || level >= LogLevel.Error);
            //    })
            //.UseUrls("http://0.0.0.0:8080")
            .UseStartup<Startup>();
    }
}
