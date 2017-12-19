using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using eventy.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace eventy.Data
{
    public class EventyDbContext : DbContext
    {
        public ApplicationDbContext ApplicationDbContext { get; }
        public IHttpContextAccessor HttpContextAccessor { get; }

        public EventyDbContext(
            DbContextOptions<EventyDbContext> options,
            ApplicationDbContext applicationDbContext,
            IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            ApplicationDbContext = applicationDbContext;
            HttpContextAccessor = httpContextAccessor;
        }

        public override int SaveChanges()
        {
            AddTimeStamps();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default(CancellationToken))
        {
            AddTimeStamps();
            return await base.SaveChangesAsync();
        }

        private void AddTimeStamps()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));
            ApplicationUser currentUser = null;

            if (HttpContextAccessor.HttpContext != null)
            {
                currentUser = ApplicationDbContext.Users
                    .Where(u => u.UserName == HttpContextAccessor.HttpContext.User.Identity.Name)
                    .OrderBy(u => u.UserName)
                    .First();
            }
            else
            {
                // We are doing a DB Seed.
                currentUser = ApplicationDbContext.Users
                    .OrderBy(u => u.Id)
                    .First();
            }

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).DateCreated = DateTime.UtcNow;
                    ((BaseEntity)entity.Entity).UserCreated = currentUser.UserName;
                }

                ((BaseEntity)entity.Entity).DateModified = DateTime.UtcNow;
                ((BaseEntity)entity.Entity).UserModified = currentUser.UserName;
            }
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<EventsFamilies> EventsFamilies { get; set; }
        public DbSet<EventsFamilyMembers> EventsFamilyMembers { get; set; }
        public DbSet<Family> Families { get; set; }
        public DbSet<FamilyMember> FamilyMembers { get; set; }

    }
}