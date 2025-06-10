using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LapTrinhWeb.Models.Entities;
using LapTrinhWeb.Models;
using LapTrinhWeb.Tools;
using System.Text;
using LapTrinhWeb.Data;

namespace LapTrinhWeb.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _context;

    public HomeController(ILogger<HomeController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        var products = _context.Products.ToList();
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
        var product = _context.Products.FirstOrDefault(p => p.ProductId == id);
        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }
    
    public IActionResult Search(string query, int? categoryId = null)
    {
        // Lấy tất cả sản phẩm và categories
        var products = _context.Products.ToList();
        var categories = _context.Categories.ToList();
        
        // Thêm thông tin Category cho mỗi sản phẩm (vì EF Core không load eager)
        foreach (var product in products)
        {
            product.Category = categories.FirstOrDefault(c => c.CategoryId == product.CategoryId);
        }
        
        // Lọc theo từ khóa tìm kiếm
        var filteredProducts = products;
        // Lọc theo danh mục nếu có
        if (categoryId.HasValue && categoryId.Value > 0)
        {
            filteredProducts = filteredProducts.Where(p => p.CategoryId == categoryId.Value).ToList();
            System.Console.WriteLine($"Lọc theo danh mục: {categoryId.Value}");
            System.Console.WriteLine($"Số sản phẩm sau khi lọc: {filteredProducts.Count}");
            foreach (var p in filteredProducts)
            {
                System.Console.WriteLine($"- {p.Name} (CategoryId: {p.CategoryId})");
            }
        }
        
        // Lọc theo từ khóa tìm kiếm
        if (!string.IsNullOrEmpty(query))
        {
            string searchQuery = query.ToLower();
            filteredProducts = filteredProducts.Where(p => p.Name.ToLower().Contains(searchQuery)).ToList();
        }
        
        ViewData["SearchQuery"] = query;
        ViewData["Categories"] = categories;
        ViewData["SelectedCategoryId"] = categoryId;
        return View(filteredProducts);
    }

    public IActionResult CheckDatabase()
    {
        var output = new StringBuilder();
        
        using (var writer = new StringWriter(output))
        {
            // Lưu trữ đầu ra Console
            var originalOut = Console.Out;
            Console.SetOut(writer);

            try
            {
                // Sửa lại để sử dụng instance method thay vì static method
                var checker = new DatabaseSchemaChecker(_context);
                checker.CheckSchema();
            }
            finally
            {
                // Khôi phục đầu ra Console
                Console.SetOut(originalOut);
            }
        }

        ViewBag.DatabaseInfo = output.ToString().Replace(Environment.NewLine, "<br />");
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

