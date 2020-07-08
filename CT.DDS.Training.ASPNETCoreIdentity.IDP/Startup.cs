using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using CT.DDS.Training.ASPNETCoreIdentity.IDP.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CT.DDS.Training.ASPNETCoreIdentity.IDP.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace CT.DDS.Training.ASPNETCoreIdentity.IDP
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("ASPNETCoreIdentity")));

            services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
                // this brings default user and role stores
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddClaimsPrincipalFactory<MyUserClaimsPrincipalFactory>();
            // register the email sender
            services.AddTransient<IEmailSender, EmailSender>();
            //Register configuration
            services.Configure<AuthMessageSenderOptions>(Configuration.GetSection("sendGrid"));

            services.AddRazorPages();

            var migrationAssembly = this.GetType().Assembly.GetName().Name;

            var builder = services.AddIdentityServer()
                //.AddInMemoryIdentityResources(Config.Ids)
                //.AddInMemoryApiResources(Config.Apis)
                //.AddInMemoryClients(Config.Clients)
                //.AddInMemoryApiScopes(Config.ApiScopes)
                .AddAspNetIdentity<AppUser>();

            
            builder.AddConfigurationStore(options =>
            {
                //ConfigurationDbContext 
                options.ConfigureDbContext = builder =>
                    builder.UseSqlServer(Configuration.GetConnectionString("IdentityServer"),
                    // we use this because the Migrations and context are in different assemblies
                    options => options.MigrationsAssembly(migrationAssembly));

            });

            builder.AddOperationalStore(options =>
            {
                //PersistedGrantDbContext
                options.ConfigureDbContext = builder =>
                    builder.UseSqlServer(Configuration.GetConnectionString("IdentityServer"),
                    // we use this because the Migrations and context are in different assemblies
                    options => options.MigrationsAssembly(migrationAssembly));

            });
            builder.AddDeveloperSigningCredential();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseIdentityServer();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
