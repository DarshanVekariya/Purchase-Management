using Access.Interface;
using Access.Repository;
using Business.User;
using Bussiness.Cart;
using Bussiness.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PurchaseManagementAPI.Controllers
{
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;

        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }
        [HttpPost]
        [Route("api/addtocart")]
        public async Task<ActionResult<Products>> AddNew(CartRequestModel cartrequest)
        {
            try
            {
                var addtocart = await _cartRepository.Add(cartrequest.ProductId, cartrequest.UserId);
                return Ok(addtocart);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while adding to cart.");
            }
        }
        [HttpPost]
        [Route("api/chkexist")]
        public async Task<ActionResult<ChkExistInCartModel>> ChkExist(CartRequestModel cartrequest)
        {
            try
            {
                var chkExistInCartModel = await _cartRepository.ChkExist(cartrequest.ProductId, cartrequest.UserId);
                return Ok(chkExistInCartModel);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while adding to cart.");
            }
        }

        [HttpDelete]
        [Route("api/remove")]
        public async Task<ActionResult<Products>> Remove(CartRequestModel cartrequest)
        {
            try
            {
                var addtocart = await _cartRepository.Remove(cartrequest.ProductId, cartrequest.UserId);
                return Ok(addtocart);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while adding to cart.");
            }
        }
    }
}
