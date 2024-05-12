using Access.Interface;
using Business.User;
using Bussiness.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PurchaseManagementAPI.Controllers
{
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        [HttpGet]
        [Route("api/Products")]
        public async Task<ActionResult<List<Products>>> GetList()
        {
            try
            {
                var productList = await productRepository.GetAll();
                return Ok(productList);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the product list.");
            }
        }

        [HttpGet]
        [Route("api/products/{id}")]
        public async Task<ActionResult<Products>> GetProduct(int id)
        {
            try
            {
                var product = await productRepository.GetById(id);
                if (product == null)
                {
                    return NotFound(); 
                }
                return Ok(product); 
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the product.");
            }
        }

        [HttpPost]
        [Route("api/addproduct")]
        public async Task<ActionResult<Products>> AddNew(Products products)
        {
            try
            {
                var addedProduct = await productRepository.Add(products);
                return Ok(addedProduct);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while adding the product.");
            }
        }
        [HttpDelete]
        [Route("api/products/{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            try
            {
                var product = await productRepository.Delete(id);
                return Ok(product); 
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting the product.");
            }
        }
        [HttpPut]
        [Route("api/updateproducts")]
        public async Task<ActionResult> UpdateProduct(Products updatedProduct)
        {
            try
            {

                await productRepository.Update(updatedProduct);
                return Ok(updatedProduct); 
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the product.");
            }
        }

    }
}
