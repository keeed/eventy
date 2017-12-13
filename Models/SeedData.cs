using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using eventy.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace eventy.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            Console.WriteLine("Attempting to seed data...");
            seedAdmin(serviceProvider);
            using (var context = serviceProvider.GetRequiredService<EventyDbContext>())
            {
                seedFamilies(context);
                seedFamilyMembers(context);
            }
            Console.WriteLine("Done seeding!");
            Console.WriteLine();
        }
        private static void seedAdmin(IServiceProvider serviceProvider)
        {
            using (var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>())
            {
                if (userManager.FindByNameAsync("adminadmin").Result != null)
                {
                    // We have already done the seed before. 
                    return;
                }

                if (!File.Exists("Seeds/admin.json"))
                {
                    // We need at least one user!
                    return;
                }

                Console.WriteLine("Seeding admin...");

                Dictionary<string, string> admin =
                    JsonConvert.DeserializeObject<Dictionary<string, string>>(
                        File.ReadAllText("Seeds/admin.json"));

                ApplicationUser applicationUser = new ApplicationUser()
                {
                    UserName = admin["username"],
                    Email = admin["email"]
                };

                userManager.CreateAsync(applicationUser, admin["password"]);
            }
        }

        private static void seedFamilies(EventyDbContext eventyDbContext)
        {
            if (!eventyDbContext.Families.Any())
            {
                seedFamily(eventyDbContext);
            }
        }

        private static void seedFamily(EventyDbContext eventyDbContext)
        {
            if (!File.Exists(@"Seeds/families.json"))
            {
                return;
            }

            if (!eventyDbContext.Families.Any())
            {
                Console.WriteLine("Seeding families...");

                var families = JsonConvert.DeserializeObject<List<Family>>(
                    File.ReadAllText(@"Seeds/families.json")
                );

                eventyDbContext.Families.AddRange(families);
                eventyDbContext.SaveChanges();
            }
        }

        private static void seedFamilyMembers(EventyDbContext eventyDbContext)
        {
            if (!eventyDbContext.FamilyMembers.Any())
            {
                seedFamilyMember(eventyDbContext);
            }
        }

        private static void seedFamilyMember(EventyDbContext eventyDbContext)
        {
            if (!File.Exists(@"Seeds/familymembers.json"))
            {
                return;
            }

            if (!eventyDbContext.FamilyMembers.Any())
            {
                Console.WriteLine("Seeding family members...");

                var familyMembers = JsonConvert.DeserializeObject<List<FamilyMember>>(
                    File.ReadAllText(@"Seeds/familymembers.json")
                );

                eventyDbContext.FamilyMembers.AddRange(familyMembers);
                eventyDbContext.SaveChanges();
            }
        }
    }
}