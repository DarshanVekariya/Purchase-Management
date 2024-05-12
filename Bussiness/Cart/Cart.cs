using Bussiness.Product;
using Bussiness.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Cart
{
    public class Cart
    {
        public int Id { get; set; }
        [ForeignKey("Users")]
        public int UserId { get; set; }

        [ForeignKey("Products")]
        public int ProductId { get; set; }
        public Product.Products Products { get; set; }
        public User.Users Users { get; set; }
    }

    public class ChkExistInCartModel
    {
        public string message { get; set; } = string.Empty; 
        public bool isexist { get; set; }
    }
}
