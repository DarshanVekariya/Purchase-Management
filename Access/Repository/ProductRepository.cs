using Access.Interface;
using Bussiness.db;
using Bussiness.Product;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Access.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly MyDbContext _db;

        public ProductRepository(MyDbContext db)
        {
            _db = db;
        }
        public async Task<List<Products>> GetAll()
        {
            return await _db.Products.OrderByDescending(p => p.Id).ToListAsync();
        }
        public async Task<Products> GetById(int id)
        {
            return await _db.Products.SingleOrDefaultAsync(p => p.Id == id);

        }
        public async Task<int> Add(Products products)
        {
            await _db.Products.AddAsync(products);
           return await _db.SaveChangesAsync();  
        }
        public async Task<int> Delete(int id)
        {
            var product = await _db.Products.FindAsync(id);
            _db.Products.Remove(product);
            return await _db.SaveChangesAsync();
        }
        public async Task<Products> Update(Products updatedProduct)
        {
            var existingProduct = await _db.Products.FindAsync(updatedProduct.Id);

            existingProduct.Name = updatedProduct.Name;
            existingProduct.Price = updatedProduct.Price;

            _db.Products.Update(existingProduct);
            await _db.SaveChangesAsync();

            return updatedProduct;
        }
        
    }
}
