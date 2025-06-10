using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using LapTrinhWeb.Data;
using LapTrinhWeb.Models.Entities;
using LapTrinhWeb.Models.ViewModels;

namespace LapTrinhWeb.Controllers
{    public class ProductController : Controller
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly AppDbContext _context;

        public ProductController(IWebHostEnvironment hostEnvironment, AppDbContext context)
        {
            _hostEnvironment = hostEnvironment;
            _context = context;
        }        // GET: Product
        public IActionResult Index()
        {
            var products = _context.Products.ToList();
            var categories = _context.Categories.ToList();
            
            // Kết hợp thông tin danh mục
            foreach (var product in products)
            {
                product.Category = categories.FirstOrDefault(c => c.CategoryId == product.CategoryId);
            }
            
            return View(products);
        }        // GET: Product/Create
        public IActionResult Create()
        {
            // Lấy tất cả danh mục để hiển thị trong form
            ViewBag.Categories = new SelectList(_context.Categories.ToList(), "CategoryId", "Name");
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            // Lấy lại danh mục để hiển thị trong form nếu có lỗi
            ViewBag.Categories = new SelectList(_context.Categories.ToList(), "CategoryId", "Name");
            
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Xử lý upload hình ảnh
            string uniqueFileName = null;
            if (model.ImageFile != null)
            {
                // Lấy đường dẫn đến thư mục wwwroot/images/products
                string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "images", "products");
                
                // Tạo tên file duy nhất
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImageFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                
                // Lưu file vào thư mục
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(fileStream);
                }
                
                // Cập nhật đường dẫn ảnh cho sản phẩm
                model.ExistingImage = "/images/products/" + uniqueFileName;
            }
            else
            {
                // Nếu không có ảnh thì dùng ảnh mặc định
                model.ExistingImage = "/images/no-image.svg";
            }

            // Chuyển đổi từ ViewModel sang Entity
            var product = model.ToProduct();
            
            // Thêm sản phẩm vào database
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            
            TempData["Success"] = "Thêm sản phẩm thành công!";
            return RedirectToAction("Index");
        }

        // GET: Product/Edit/5
        public IActionResult Edit(int id)
        {
            // Lấy sản phẩm cần sửa
            var product = _context.Products.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            // Lấy tất cả danh mục để hiển thị trong form
            ViewBag.Categories = new SelectList(_context.Categories.ToList(), "CategoryId", "Name", product.CategoryId);
            
            // Chuyển đổi từ Entity sang ViewModel
            var model = ProductViewModel.FromProduct(product);
            return View(model);
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductViewModel model)
        {
            if (id != model.ProductId)
            {
                return NotFound();
            }

            // Lấy lại danh mục để hiển thị trong form nếu có lỗi
            ViewBag.Categories = new SelectList(_context.Categories.ToList(), "CategoryId", "Name", model.CategoryId);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Xử lý upload hình ảnh
            if (model.ImageFile != null)
            {
                // Lấy đường dẫn đến thư mục wwwroot/images/products
                string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "images", "products");
                
                // Tạo tên file duy nhất
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImageFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                
                // Lưu file vào thư mục
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(fileStream);
                }
                
                // Cập nhật đường dẫn ảnh cho sản phẩm
                model.ExistingImage = "/images/products/" + uniqueFileName;

                // Xóa ảnh cũ nếu có
                var oldProduct = await _context.Products.FindAsync(id);
                if (oldProduct != null && !string.IsNullOrEmpty(oldProduct.Image) && oldProduct.Image.StartsWith("/images/products/"))
                {
                    string oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, oldProduct.Image.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        try
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                        catch (IOException)
                        {
                            // Ghi log lỗi nếu cần
                        }
                    }
                }
            }

            try
            {
                // Chuyển đổi từ ViewModel sang Entity
                var product = model.ToProduct();
                
                // Cập nhật sản phẩm trong database
                _context.Update(product);
                await _context.SaveChangesAsync();
                
                TempData["Success"] = "Cập nhật sản phẩm thành công!";
            }
            catch (Exception)
            {
                if (!ProductExists(model.ProductId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("Index");
        }

        // GET: Product/Delete/5
        public IActionResult Delete(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            // Make sure we have a category associated with the product
            product.Category = _context.Categories.FirstOrDefault(c => c.CategoryId == product.CategoryId);
            
            // Make sure there's an image path (use default if not present)
            if (string.IsNullOrEmpty(product.Image))
            {
                product.Image = "/images/no-image.svg";
            }

            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            // Xóa ảnh nếu có
            if (!string.IsNullOrEmpty(product.Image) && product.Image.StartsWith("/images/products/"))
            {
                string imagePath = Path.Combine(_hostEnvironment.WebRootPath, product.Image.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
                if (System.IO.File.Exists(imagePath))
                {
                    try
                    {
                        System.IO.File.Delete(imagePath);
                    }
                    catch (IOException)
                    {
                        // Ghi log lỗi nếu cần
                    }
                }
            }

            // Xóa sản phẩm khỏi database
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            
            TempData["Success"] = "Xóa sản phẩm thành công!";
            return RedirectToAction("Index");
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
