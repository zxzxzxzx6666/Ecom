using Microsoft.EntityFrameworkCore;
using ApplicationCore.Entities.IdentityAggregate;

namespace Infrastructure.Data
{
    public class WebContext : DbContext
    {
        // Required by Entity Framework
        public WebContext(DbContextOptions<WebContext> options) : base(options) { }
        public DbSet<IdentityInfo> IdentityInfo { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityInfo>()
                .HasIndex(u => u.Email)  // build index
                .IsUnique();
        }
    }
}
