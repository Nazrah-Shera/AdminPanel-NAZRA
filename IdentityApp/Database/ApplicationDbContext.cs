using IdentityApp.Areas.Identity;
using IdentityApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityApp.Database
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
    }

        public DbSet<TBackOfficeUser>  T_backOfficeUsers { get; set; }
        public DbSet<TApplicationLogs> T_applicationLogs { get; set; }
        public DbSet<TExternalLogs> T_externalLogs { get; set; }
        public DbSet<TAuditLogs> T_auditLogs { get; set; }

    }
}
