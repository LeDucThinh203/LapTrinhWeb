using System.Collections.Generic;
namespace LapTrinhWeb.Models.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Product
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;
    public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    
    // Thêm các thuộc tính cho view    
    public string Image { get; set; } = string.Empty;    
    public string Description { get; set; } = string.Empty;
    public bool IsFeatured { get; set; } = false;
}