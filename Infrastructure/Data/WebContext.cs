using Microsoft.EntityFrameworkCore;
using ApplicationCore.Entities.IdentityAggregate;

namespace Infrastructure.Data
{
    public class WebContext : DbContext
    {
        // Required by Entity Framework
        public WebContext(DbContextOptions<WebContext> options) : base(options) { }
        public DbSet<IdentityInfos> IdentityInfo { get; set; }
        public DbSet<Roles> Roles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityInfos>()
                .HasIndex(u => u.Email)  // build index
                .IsUnique();
        }
    }
}
