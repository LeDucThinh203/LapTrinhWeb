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
        return View();
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
        var products = new[]
        {
            new { Id = 1, Name = "Áo Thun Năng Động", Price = 150000, Image = "/images/aothun.jpg", Description = "Áo thun chất liệu cotton mềm mại, thấm hút mồ hôi, phù hợp đi chơi hoặc tập thể thao." },
            new { Id = 2, Name = "Quần Jean Cá Tính", Price = 300000, Image = "/images/jean.jpg", Description = "Thiết kế trẻ trung, phong cách với chất liệu Jean co giãn nhẹ, dễ phối đồ." },
            new { Id = 3, Name = "Giày Sneaker Thời Trang", Price = 500000, Image = "/images/sneaker.jpg", Description = "Sneaker siêu nhẹ, đế cao su chống trượt, phù hợp với cả nam và nữ." }
        };
        var product = products.FirstOrDefault(p => p.Id == id);
        if (product == null) return NotFound();
        ViewBag.Product = product;
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
