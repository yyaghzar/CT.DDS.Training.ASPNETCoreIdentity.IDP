using System;
using CT.DDS.Training.ASPNETCoreIdentity.IDP.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(CT.DDS.Training.ASPNETCoreIdentity.IDP.Areas.Identity.IdentityHostingStartup))]
namespace CT.DDS.Training.ASPNETCoreIdentity.IDP.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}