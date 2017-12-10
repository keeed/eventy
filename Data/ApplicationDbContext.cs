using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using eventy.Models;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace eventy.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public IHttpContextAccessor HttpContextAccessor { get; }

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options, 
            IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            HttpContextAccessor = httpContextAccessor;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
