using Access.Interface;
using Bussiness.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NuGet.ProjectModel;
using PurchaseManagement.Models;
using System.Diagnostics;

namespace PurchaseManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(IUserRepository userRepository, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _userRepository = userRepository;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<ActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Login(string username, string password)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(username, password, true, lockoutOnFailure: false);
                var iuser  =  await _userManager.FindByNameAsync(username);
                var roles = await _userManager.GetRolesAsync(iuser);

                if (result.Succeeded)
                {
                    var user = await _userRepository.GetUser(username, password);
                    if (user != null)
                    {
                        HttpContext.Session.SetString("UserId", Convert.ToString(user.Id));
                        HttpContext.Session.SetString("Role", string.Join(",", roles));
                        return RedirectToAction("Index", "Product");
                    }
                }
                ViewBag.ModelError = "Please Enter Valid Username And Password!!";
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            catch (Exception ex)
            {
                // Log or handle exception
                Console.WriteLine(ex.ToString());   
            }
            return View();
        }
        public async Task<ActionResult> SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> SignUp(string username, string password)
        {
            try
            {
                string ModelError = "";
                var user = new ApplicationUser { UserName = username };
                var result = await _userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    // You can customize this redirect URL
                    await _userRepository.AddUser(username,password);
                    return RedirectToAction("Login", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelError += error.Description;
                }
                ViewBag.ModelError = ModelError;
            }
            catch (Exception ex)
            {

                throw;
            }
            return View();  
        }

        public async Task<ActionResult> Logout()
        {
            try
            {
                HttpContext.Session.Remove("UserId");
                HttpContext.Session.Remove("Role");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
            return RedirectToAction("Login");
        }

        public async Task<ActionResult> NoAccess()
        {
            return View();
        }
    }
}
