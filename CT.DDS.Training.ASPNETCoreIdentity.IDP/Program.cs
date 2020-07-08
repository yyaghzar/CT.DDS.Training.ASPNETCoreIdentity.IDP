using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CT.DDS.Training.ASPNETCoreIdentity.IDP
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope()) {

                var context = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                context.Database.Migrate();

                if (!context.Clients.Any()) {
                    context.Clients.AddRange(Config.Clients.Select(s => s.ToEntity()));
                    context.SaveChanges();
                }

                if (!context.IdentityResources.Any())
                {
                    context.IdentityResources.AddRange(Config.Ids.Select(s => s.ToEntity()));
                    context.SaveChanges();
                }

                if (!context.ApiResources.Any())
                {
                    context.ApiResources.AddRange(Config.Apis.Select(s => s.ToEntity()));
                    context.SaveChanges();
                }

                if (!context.ApiScopes.Any())
                {
                    context.ApiScopes.AddRange(Config.ApiScopes.Select(s => s.ToEntity()));
                    context.SaveChanges();
                }


            };

                host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
