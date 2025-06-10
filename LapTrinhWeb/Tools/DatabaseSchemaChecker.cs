using System;
using System.Linq;
using LapTrinhWeb.Data;

namespace LapTrinhWeb.Tools
{
    public class DatabaseSchemaChecker
    {
        private readonly AppDbContext _context;

        public DatabaseSchemaChecker(AppDbContext context)
        {
            _context = context;
        }

        public void CheckSchema()
        {
            // Lấy tất cả các sản phẩm
            var products = _context.Products
                .Take(5)
                .ToList();

            Console.WriteLine("======== KIỂM TRA CẤU TRÚC DATABASE ========");
            Console.WriteLine($"Tổng số sản phẩm: {products.Count}");
            Console.WriteLine();

            if (products.Any())
            {
                var firstProduct = products.First();
                Console.WriteLine("Chi tiết sản phẩm đầu tiên:");
                Console.WriteLine($"- ProductId: {firstProduct.ProductId}");
                Console.WriteLine($"- Name: {firstProduct.Name}");
                Console.WriteLine($"- Price: {firstProduct.Price}");
                Console.WriteLine($"- CategoryId: {firstProduct.CategoryId}");
                Console.WriteLine($"- Image: {firstProduct.Image}");
                Console.WriteLine($"- IsFeatured: {firstProduct.IsFeatured}");
                Console.WriteLine();
            }

            // Lấy tất cả các danh mục
            var categories = _context.Categories
                .Take(5)
                .ToList();

            Console.WriteLine($"Tổng số danh mục: {categories.Count}");
            Console.WriteLine();

            if (categories.Any())
            {
                var firstCategory = categories.First();
                Console.WriteLine("Chi tiết danh mục đầu tiên:");
                Console.WriteLine($"- CategoryId: {firstCategory.CategoryId}");
                Console.WriteLine($"- Name: {firstCategory.Name}");
                Console.WriteLine();
            }

            Console.WriteLine("======== KẾT THÚC KIỂM TRA ========");
        }
    }
}
