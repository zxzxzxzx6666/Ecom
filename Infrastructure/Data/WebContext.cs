using Microsoft.EntityFrameworkCore;
using ApplicationCore.Entities.IdentityAggregate;
using ApplicationCore.Entities;
using ApplicationCore.Entities.BasketAggregate;
using ApplicationCore.Entities.OrderAggregate;
using System.Reflection;

namespace Infrastructure.Data
{
    public class WebContext : DbContext
    {
        // Required by Entity Framework
        public WebContext(DbContextOptions<WebContext> options) : base(options) { }
        public DbSet<IdentityInfos> IdentityInfo { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<CatalogItem> CatalogItems { get; set; }
        public DbSet<CatalogBrand> CatalogBrands { get; set; }
        public DbSet<CatalogType> CatalogTypes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityInfos>()
                .HasIndex(u => u.Email)  // build index
                .IsUnique();
            // 從目前專案中找到所有 IEntityTypeConfiguration<T> 的類別，然後自動呼叫它們的 Configure() 方法，套用到 ModelBuilder 上」
            // 這樣就不需要在這裡一個一個手動註冊了
            // 可以全域搜尋 IEntityTypeConfiguration <T> 來找所有的實作類別
            base.OnModelCreating(modelBuilder); // 保留預設行為
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); // 套用自訂設定
        }
    }
}
