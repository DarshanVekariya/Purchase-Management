using Bussiness.Cart;
using Bussiness.Product;
using Bussiness.Purchaseorder;

namespace PurchaseManagement.IService
{
    public interface ICartService
    {
        Task<List<Products>> Add(int productId, int userId);
        Task<ChkExistInCartModel> ChkExist(int productId, int userId);
        Task<List<Products>> Remove(int productId, int userId);
        Task<List<PurchaseOrder>> PurchaseOrder(List<PurchaseOrder> purchaseOrder);
        Task<List<PurchaseOrder>> PurchaseOrderList(int id);
    }
}
