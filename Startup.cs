using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using eventy.Data;
using eventy.Models;
using eventy.Services;
using System.Linq;
using System.IO;
using System;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using System.Net.Sockets;
using eventy.Repositories;

namespace eventy
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment HostingEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            
            services.AddDbContext<EventyDbContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
            
            services.AddTransient<FamiliesRepository>();

            services.AddMvc();

            // Extra for user friendliness
            if (!HostingEnvironment.IsDevelopment())
            {
                printInstructions();
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            initializeDabases(app);
        }

        private void initializeDabases(IApplicationBuilder app)
        {
            createSqliteDbFile();

            // In-case that we need to do some migrations.
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                ApplicationDbContext applicationDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                if (applicationDbContext.Database.GetPendingMigrations().Any())
                {
                    applicationDbContext.Database.Migrate();
                }
                EventyDbContext eventyDbContext = scope.ServiceProvider.GetRequiredService<EventyDbContext>();
                if (eventyDbContext.Database.GetPendingMigrations().Any())
                {
                    eventyDbContext.Database.Migrate();
                }
                SeedData.Initialize(scope.ServiceProvider.GetRequiredService<IServiceProvider>());
            }
        }
        private static void createSqliteDbFile()
        {
            // In case that the user is using sqlite.
            if (!File.Exists("app.db"))
            {
                // Worst case, we don't have the app.db!
                File.Create("app.db").Close();
            }
        }

        private void printInstructions()
        {
            Console.WriteLine("Please try the following urls below in your browser to access the site:");
            printLocalIpAddress();
            Console.WriteLine("");
        }

        private void printLocalIpAddress()
        {
            foreach(NetworkInterfaceType networkInterfaceType in Enum.GetValues(typeof(NetworkInterfaceType)))
            {
                printLocalIPAddressBasedOnType(networkInterfaceType);
            }
        }

        private void printLocalIPAddressBasedOnType(NetworkInterfaceType networkInterfaceType)
        {
            foreach(var ipAddress in GetAllLocalIPv4(networkInterfaceType))
            {
                Console.WriteLine(ipAddress);
            }
        }

        // https://stackoverflow.com/questions/6803073/get-local-ip-address
        private string[] GetAllLocalIPv4(NetworkInterfaceType _type)
        {
            List<string> ipAddrList = new List<string>();
            foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (item.NetworkInterfaceType == _type && item.OperationalStatus == OperationalStatus.Up)
                {
                    foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            ipAddrList.Add(ip.Address.ToString());
                        }
                    }
                }
            }
            return ipAddrList.ToArray();
        }
    }
}
