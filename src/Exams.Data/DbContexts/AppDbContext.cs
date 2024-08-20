using Exams.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Exams.Data.DbContexts
{
    public sealed class AppDbContext : DbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<VerificationCode> VerificationCodes => Set<VerificationCode>();
        public DbSet<ResetCode> ResetCodes => Set<ResetCode>();

        public AppDbContext(DbContextOptions options) : base(options)
        {
            this.Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
