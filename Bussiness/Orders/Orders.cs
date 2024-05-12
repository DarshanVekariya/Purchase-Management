using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bussiness.Orders
{
    public class Orders
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }
        [Required(ErrorMessage = "Please enter order date")]
        public DateTime OrderDate { get; set; }

        public User.Users User { get; set; }
    }
}
