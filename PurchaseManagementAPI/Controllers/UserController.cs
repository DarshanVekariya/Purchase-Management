using Access.Interface;
using Access.Repository;
using Business.User;
using Bussiness.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PurchaseManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<UserController>> GetUser(string username , string password)
        {
            try
            {
                var user = await _userRepository.GetUser(username , password);   
                if (user == null)
                {
                    return NotFound();
                }
                HttpContext.Session.SetString("UserId",Convert.ToString(user.Id));
                return Ok("Success");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the product.");
            }
        }
    }
}
