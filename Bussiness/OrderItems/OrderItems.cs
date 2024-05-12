using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bussiness.OrderItems
{
    public class OrderItems
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Orders")]
        public int OrderId { get; set; }
        [Required]
        [ForeignKey("Products")]
        public int ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }

        public Product.Products Products { get; set; }
        public Orders.Orders Orders { get; set; }
    }
}
