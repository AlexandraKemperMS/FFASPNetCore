using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FFASPNetCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

            public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
                webBuilder.ConfigureAppConfiguration((hostingContext, config) =>
                {
                var settings = config.Build();
                //var connection = settings.GetConnectionString("ConnectionStrings:AppConfig");
                var env = hostingContext.HostingEnvironment.EnvironmentName;
                config.AddAzureAppConfiguration(options =>
                        options
                        .Connect(settings["ConnectionStrings:AppConfig"])
                        .UseFeatureFlags(opt =>
                        {
                            opt.Label = "Other";
                            opt.CacheExpirationInterval = TimeSpan.FromSeconds(5);
                        }));
                }).UseStartup<Startup>());
    }
}
