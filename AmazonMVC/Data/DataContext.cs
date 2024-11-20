using AmazonMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AmazonMVC.Data
{
    public class DataContext : IdentityDbContext<IdentityUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Product>(eb =>
            {
                eb.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);

                eb.HasMany(p => p.OrderItems)
                .WithOne(oi => oi.Product)
                .HasForeignKey(oi => oi.ProductId);

                eb.HasMany(p => p.CartItems)
                .WithOne(ci => ci.Product)
                .HasForeignKey(ci => ci.ProductId);

                eb.Property(x => x.Id).ValueGeneratedOnAdd();
                eb.Property(x => x.Name).HasColumnType("varchar(255)");
                eb.Property(x => x.Price).HasColumnType("decimal(10,2)");
                eb.Property(x => x.StockQuantity).HasColumnType("int");

            });

            builder.Entity<Category>(eb =>
            {
                eb.Property(x => x.Id).ValueGeneratedOnAdd();
                eb.Property(x => x.Name).HasColumnType("varchar(255)");
            });

            builder.Entity<Order>(eb =>
            {
                eb.HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId);

                eb.HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId);

                eb.Property(x => x.Id).ValueGeneratedOnAdd();
                eb.Property(x => x.OrderDate).HasColumnType("date");
            });

            builder.Entity<OrderItem>(eb =>
            {
                eb.Property(x => x.Id).ValueGeneratedOnAdd();
                eb.Property(x => x.Quantity).HasColumnType("int");
                eb.Property(x => x.Price).HasColumnType("decimal(10,2)");
            });

            builder.Entity<CartItem>(eb =>
            {
                eb.HasOne(ci => ci.User)
                .WithMany(u => u.CartItems)
                .HasForeignKey(ci => ci.UserId);

                eb.Property(x => x.Id).ValueGeneratedOnAdd();
                eb.Property(x => x.Quantity).HasColumnType("int");
                eb.Property(x => x.Price).HasColumnType("decimal(10,2)");
            });
        }





    }
}
