using Access.Interface;
using Business.User;
using Bussiness.Cart;
using Bussiness.Product;
using Bussiness.Purchaseorder;
using Bussiness.User;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PurchaseManagement.IService;

namespace PurchaseManagement.Controllers
{
    [Auth]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IProductService _productService;

        public CartController(ICartService cartService, IProductService productService)
        {
            _cartService = cartService;
            _productService = productService;
        }


        public async Task<IActionResult> Index(int productId)
        {
            List<Products> products = new List<Products>();
            List<PurchaseOrder> purchaseOrderIteams = new List<PurchaseOrder>();
            decimal total = 0;
            string userId = HttpContext.Session.GetString("UserId");
            if (!string.IsNullOrEmpty(userId))
                products = await _cartService.Add(productId, Convert.ToInt32(userId));
            foreach (var product in products)
            {
                total += product.Price;
                purchaseOrderIteams.Add(new PurchaseOrder
                {
                    ProductName = product.Name,
                    ProductId = product.Id,
                    ProductPrice = product.Price,
                    UserId = Convert.ToInt32(userId)
                });
            }
            ViewBag.Total = total;
            return View(purchaseOrderIteams);
        }

        [HttpPost]
        public async Task<IActionResult> Index(List<PurchaseOrder> item)
        {
            List<PurchaseOrder> purchaseOrderIteams = new List<PurchaseOrder>();
            purchaseOrderIteams = await _cartService.PurchaseOrder(item);

            return RedirectToAction("PurchaseOrder", purchaseOrderIteams);
        }
        public async Task<IActionResult> RemoveItem(int productId)
        {
            List<Products> products = new List<Products>();
            string userId = HttpContext.Session.GetString("UserId");
            if (!string.IsNullOrEmpty(userId))
                products = await _cartService.Remove(productId, Convert.ToInt32(userId));

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> PurchaseOrder(List<PurchaseOrder> item)
        {
            string userId = HttpContext.Session.GetString("UserId");
            if (item.Count == 0 && !string.IsNullOrEmpty(userId))
                item = await _cartService.PurchaseOrderList(Convert.ToInt32(userId));

            return View(item);
        }
    }
}
