
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LapTrinhWeb.Models.Entities;
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
        using (var context = new Data.AppDbContext())
        {
            var products = context.Products.ToList();
            return View(products);
        }
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
        using (var context = new Data.AppDbContext())
        {
            var product = context.Products.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = HttpContext.TraceIdentifier });
    }
}
