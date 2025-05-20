using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LapTrinhWeb.Models;   

namespace LapTrinhWeb.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        // Mock data sản phẩm
        var products = new List<Product>
        {
            new Product { Id = 1, Name = "Áo Thun Năng Động",      Price = 150_000m, Image = "/images/aothun.jpg", Description = "Áo thun chất liệu cotton mềm mại, thấm hút mồ hôi, phù hợp đi chơi hoặc tập thể thao." },
            new Product { Id = 2, Name = "Quần Jean Cá Tính",       Price = 300_000m, Image = "/images/jean.jpg",    Description = "Thiết kế trẻ trung, phong cách với chất liệu Jean co giãn nhẹ, dễ phối đồ." },
            new Product { Id = 3, Name = "Giày Sneaker Thời Trang",  Price = 500_000m, Image = "/images/sneaker.jpg", Description = "Sneaker siêu nhẹ, đế cao su chống trượt, phù hợp với cả nam và nữ." },
            // Sản phẩm mới
            new Product { Id = 4, Name = "Áo Khoác Gió", Price = 400_000m, Image = "/images/aokhoac.jpg", Description = "Áo khoác gió chống nước, nhẹ, phù hợp đi chơi, du lịch." },
            new Product { Id = 5, Name = "Balo Thời Trang", Price = 250_000m, Image = "/images/balo.jpg", Description = "Balo vải bố bền đẹp, nhiều ngăn tiện lợi cho học sinh, sinh viên." }
        };
        return View(products);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Cart()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AddToCart(int productId)
    {
        // Lưu số lượng vào Session (giả lập 1 sản phẩm)
        string key = $"Cart_Product_{productId}";
        int quantity = HttpContext.Session.GetInt32(key) ?? 0;
        HttpContext.Session.SetInt32(key, quantity + 1);
        return RedirectToAction("Cart");
    }

    [HttpPost]
    public IActionResult RemoveFromCart(int productId)
    {
        string key = $"Cart_Product_{productId}";
        HttpContext.Session.Remove(key);
        return RedirectToAction("Cart");
    }

    [HttpPost]
    public IActionResult UpdateQuantity(int productId, int change)
    {
        string key = $"Cart_Product_{productId}";
        int quantity = HttpContext.Session.GetInt32(key) ?? 0;
        quantity += change;
        if (quantity < 1) quantity = 1;
        HttpContext.Session.SetInt32(key, quantity);
        return RedirectToAction("Cart");
    }

    public IActionResult ProductDetail(int id)
    {
        // Mock data sản phẩm (đồng bộ với Index)
        var products = new List<Product>
        {
            new Product { Id = 1, Name = "Áo Thun Năng Động",      Price = 150_000m, Image = "/images/aothun.jpg", Description = "Áo thun chất liệu cotton mềm mại, thấm hút mồ hôi, phù hợp đi chơi hoặc tập thể thao." },
            new Product { Id = 2, Name = "Quần Jean Cá Tính",       Price = 300_000m, Image = "/images/jean.jpg",    Description = "Thiết kế trẻ trung, phong cách với chất liệu Jean co giãn nhẹ, dễ phối đồ." },
            new Product { Id = 3, Name = "Giày Sneaker Thời Trang",  Price = 500_000m, Image = "/images/sneaker.jpg", Description = "Sneaker siêu nhẹ, đế cao su chống trượt, phù hợp với cả nam và nữ." },
            new Product { Id = 4, Name = "Áo Khoác Gió", Price = 400_000m, Image = "/images/aokhoac.jpg", Description = "Áo khoác gió chống nước, nhẹ, phù hợp đi chơi, du lịch." },
            new Product { Id = 5, Name = "Balo Thời Trang", Price = 250_000m, Image = "/images/balo.jpg", Description = "Balo vải bố bền đẹp, nhiều ngăn tiện lợi cho học sinh, sinh viên." }
        };
        var product = products.FirstOrDefault(p => p.Id == id);
        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = HttpContext.TraceIdentifier });
    }
}
