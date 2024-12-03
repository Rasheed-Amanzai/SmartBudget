using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartBudget.Models;
using SmartBudget.Utils;

namespace SmartBudget.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>  // Inherit from IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Add DbSets for your models
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<BudgetReport> BudgetReports { get; set; }
        public DbSet<UserPreferences> UserPreferences { get; set; }
    }
}
