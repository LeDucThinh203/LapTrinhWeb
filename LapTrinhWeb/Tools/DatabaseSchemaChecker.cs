using System;
using System.Linq;
using LapTrinhWeb.Data;

namespace LapTrinhWeb.Tools
{
    public class DatabaseSchemaChecker
    {
        public static void CheckSchema()
        {
            using (var context = new AppDbContext())
            {
                // Lấy tất cả các sản phẩm
                var products = context.Products
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
                    
                    // Kiểm tra xem có thuộc tính Id không
                    var productType = firstProduct.GetType();
                    var hasIdProperty = productType.GetProperty("Id") != null;
                    
                    Console.WriteLine();
                    Console.WriteLine($"Có thuộc tính Id dư thừa: {hasIdProperty}");
                }
                else
                {
                    Console.WriteLine("Không có sản phẩm nào trong cơ sở dữ liệu");
                }
                
                Console.WriteLine("===========================================");
            }
        }
    }
}
