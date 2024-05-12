using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Purchaseorder
{
    public class PurchaseOrder
    {
        public int ProductId { get; set; } 
        public int Quantity { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal ProductPrice { get; set; }
        public string OrderDate { get; set; } = string.Empty;
        public int UserId { get; set; }
    }
}
