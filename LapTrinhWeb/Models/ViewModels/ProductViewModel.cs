using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using LapTrinhWeb.Models.Entities;

namespace LapTrinhWeb.Models.ViewModels
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        
        [Required(ErrorMessage = "Vui lòng nhập tên sản phẩm")]
        [Display(Name = "Tên sản phẩm")]
        public string Name { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Vui lòng nhập giá sản phẩm")]
        [Range(1000, 100000000, ErrorMessage = "Giá sản phẩm phải từ 1.000 đến 100.000.000 VNĐ")]
        [Display(Name = "Giá sản phẩm")]
        public decimal Price { get; set; }
        
        [Required(ErrorMessage = "Vui lòng chọn danh mục")]
        [Display(Name = "Danh mục")]
        public int CategoryId { get; set; }
        
        [Display(Name = "Mô tả")]
        [Required(ErrorMessage = "Vui lòng nhập mô tả sản phẩm")]
        public string Description { get; set; } = string.Empty;
        
        [Display(Name = "Sản phẩm nổi bật")]
        public bool IsFeatured { get; set; }
        
        // Đường dẫn hình ảnh hiện tại (dùng khi sửa sản phẩm)
        public string? ExistingImage { get; set; }
        
        [Display(Name = "Hình ảnh sản phẩm")]
        public IFormFile? ImageFile { get; set; }
        
        // Phương thức chuyển đổi từ ViewModel sang Entity
        public Product ToProduct()
        {
            return new Product
            {
                ProductId = ProductId,
                Name = Name,
                Price = Price,
                CategoryId = CategoryId,
                Description = Description,
                IsFeatured = IsFeatured,
                Image = ExistingImage ?? ""
            };
        }
        
        // Phương thức chuyển đổi từ Entity sang ViewModel
        public static ProductViewModel FromProduct(Product product)
        {
            return new ProductViewModel
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Price = product.Price,
                CategoryId = product.CategoryId,
                Description = product.Description,
                IsFeatured = product.IsFeatured,
                ExistingImage = product.Image
            };
        }
    }
}
