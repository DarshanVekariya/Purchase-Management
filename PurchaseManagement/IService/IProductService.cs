using Bussiness.Product;

namespace PurchaseManagement.IService
{
    public interface IProductService
    {
        Task<List<Products>> GetAll();
        Task<Products> Add(Products products);
        Task<Products> Delete(int productId);
        Task<Products> Update(Products products);
    }
}
