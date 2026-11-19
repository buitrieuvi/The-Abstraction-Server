using TheAbstraction.Domain.Entities;
using TheAbstraction.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TheAbstraction.Infrastructure.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : IdentityDbContext<ApplicationUser>(dbContextOptions)
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }
        
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .Property(x => x.TotalPrice)
                .HasPrecision(18, 2);
            modelBuilder.Entity<Order>()
                .Property(x => x.OrderStatus)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<OrderDetail>()
                .Property(x => x.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<ProductVariant>()
                .Property(x => x.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Product>().Property(x => x.Name).HasMaxLength(200).IsRequired();
            modelBuilder.Entity<Product>().Property(x => x.Description).HasMaxLength(1000);
            modelBuilder.Entity<Product>().HasIndex(x => x.Name);
            modelBuilder.Entity<ProductVariant>().Property(x => x.Model).HasMaxLength(50);
            modelBuilder.Entity<ProductVariant>().Property(x => x.Color).HasMaxLength(50);
            modelBuilder.Entity<ProductVariant>().Property(x => x.Size).HasMaxLength(20);
            modelBuilder.Entity<Product>().HasMany<ProductVariant>().WithOne()
                .HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ProductVariant>().HasIndex(x => x.ProductId);

            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
