using Bussiness.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Access.Interface
{
    public interface IProductRepository
    {
        Task<List<Products>> GetAll();
        Task<Products> GetById(int id);
        Task<int> Add(Products products);
        Task<int> Delete(int id);
        Task<Products> Update(Products updatedProduct);
    }
}
