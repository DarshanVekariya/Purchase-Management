using Bussiness.Cart;
using Bussiness.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Access.Interface
{
    public interface ICartRepository
    {
        Task<List<Products>> Add(int productId, int userId);
        Task<ChkExistInCartModel> ChkExist(int productId, int userId);
        Task<List<Products>> Remove(int productId, int userId);
    }
}
