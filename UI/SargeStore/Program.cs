﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;
using System;

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
            .UseStartup<Startup>()
            .UseSerilog((host, log) =>
            {
                log.ReadFrom.Configuration(host.Configuration)
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
                .Enrich.FromLogContext()
                .WriteTo.Console(
                    outputTemplate: "[{Timestamp:HH:mm:ss.fff}{Level:u3}]{SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}")
                .WriteTo.RollingFile($".\\Logs\\SargeStore[{DateTime.Now:yyyy-MM-ddTHH-mm-ss}].log")
                .WriteTo.File(new JsonFormatter(",", true), $".\\Logs\\SargeStore[{DateTime.Now:yyyy-MM-ddTHH-mm-ss}].log.json")
                .WriteTo.Seq("http://localhost:53415");
            });
                
    }
}
