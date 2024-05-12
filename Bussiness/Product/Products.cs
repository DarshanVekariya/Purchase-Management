using System.ComponentModel.DataAnnotations;

namespace Bussiness.Product
{
    public class Products
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public decimal Price { get; set; } 
    }
}
