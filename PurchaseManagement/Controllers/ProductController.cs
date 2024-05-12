using Bussiness.Cart;
using Bussiness.Product;
using Business.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using PurchaseManagement.IService;
using PurchaseManagement.Service;
using System.Runtime.InteropServices;
using StackExchange.Redis;

namespace PurchaseManagement.Controllers
{
    [Auth]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICartService _cartService;

        public ProductController(IProductService productService, ICartService cartService)
        {
            _productService = productService;
            _cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
            List<Products> producs = new List<Products>();
            producs = await _productService.GetAll();
            ViewBag.message = TempData["Message"];
            return View(producs);
        }
        [HttpPost]
        public async Task<IActionResult> Index(int Id)
        {
            List<Products> products = new List<Products>();
            ChkExistInCartModel chkExistInCartModel = new ChkExistInCartModel();
            string userId = HttpContext.Session.GetString("UserId");
            if (!string.IsNullOrEmpty(userId))
            {
                chkExistInCartModel = await _cartService.ChkExist(Id, Convert.ToInt32(userId));
                if(chkExistInCartModel != null) {
                    if (chkExistInCartModel.isexist)
                    {
                        TempData["Message"] = chkExistInCartModel.message;
                        return RedirectToAction("Index");
                    }
                }
                products = await _cartService.Add(Id, Convert.ToInt32(userId));
                ViewBag.message = "Add Successfully!!";
            }
            return View(products);
        }

        [Auth(role: "Admin")]
        public async Task<IActionResult> List()
        {
            List<Products> Products = new List<Products>(); 
            Products = await _productService.GetAll();  
            return View(Products);
        }
        [Auth(role: "Admin")]
        public async Task<IActionResult> Add(Products products)
        {
            Products Products = new Products();
            Products = await _productService.Add(products);
            return Json(Products);
        }
        [Auth(role: "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            Products Products = new Products();
            Products = await _productService.Delete(id);
            return Json(Products);
        }
        [Auth(role: "Admin")]
        [HttpPost]
        public async Task<IActionResult> Save(Products products)
        {
            Products Products = new Products();
            Products = await _productService.Update(products);
            return Json(Products);

        }
    }
}
