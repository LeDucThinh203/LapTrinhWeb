
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LapTrinhWeb.Models.Entities;

namespace LapTrinhWeb.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json")
                    .Build();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Seed Category trước để tránh lỗi khóa ngoại
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Thời trang" },
                new Category { CategoryId = 2, Name = "Giày dép" },
                new Category { CategoryId = 3, Name = "Phụ kiện" }
            );            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, Name = "Áo Thun Năng Động", Price = 150000, Image = "/images/aothun.jpg", Description = "Áo thun chất liệu cotton mềm mại, thấm hút mồ hôi, phù hợp đi chơi hoặc tập thể thao.", CategoryId = 1, IsFeatured = true },
                new Product { ProductId = 2, Name = "Quần Jean Cá Tính", Price = 300000, Image = "/images/jean.jpg", Description = "Thiết kế trẻ trung, phong cách với chất liệu Jean co giãn nhẹ, dễ phối đồ.", CategoryId = 1, IsFeatured = false },
                new Product { ProductId = 3, Name = "Giày Sneaker Thời Trang", Price = 500000, Image = "/images/sneaker.jpg", Description = "Sneaker siêu nhẹ, đế cao su chống trượt, phù hợp với cả nam và nữ.", CategoryId = 2, IsFeatured = true },
                new Product { ProductId = 4, Name = "Áo Khoác Gió", Price = 400000, Image = "/images/aokhoac.jpg", Description = "Áo khoác gió chống nước, nhẹ, phù hợp đi chơi, du lịch.", CategoryId = 1, IsFeatured = true },
                new Product { ProductId = 5, Name = "Balo Thời Trang", Price = 250000, Image = "/images/balo.jpg", Description = "Balo vải bố bền đẹp, nhiều ngăn tiện lợi cho học sinh, sinh viên.", CategoryId = 3, IsFeatured = false }
            );
        }
    }
}