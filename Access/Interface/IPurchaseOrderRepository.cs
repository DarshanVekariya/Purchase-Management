using Bussiness.Purchaseorder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Access.Interface
{
    public interface IPurchaseOrderRepository
    {
        Task<List<PurchaseOrder>> GenerateOrderAndItems(List<PurchaseOrder> orderItems);
        Task<List<PurchaseOrder>> GetList(int UserId);
    }
}
