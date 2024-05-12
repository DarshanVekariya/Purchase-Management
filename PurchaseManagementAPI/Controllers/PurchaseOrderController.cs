using Access.Interface;
using Business.User;
using Bussiness.Cart;
using Bussiness.Product;
using Bussiness.Purchaseorder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace PurchaseManagementAPI.Controllers
{
    [ApiController]
    public class PurchaseOrderController : ControllerBase
    {
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;

        public PurchaseOrderController(IPurchaseOrderRepository purchaseOrderRepository)
        {
            _purchaseOrderRepository = purchaseOrderRepository;
        }

        [HttpGet]
        [Route("api/purchaseorder/{UserId}")]
        public async Task<ActionResult<Products>> Getlist(int UserId)
        {
            try
            {
                List<PurchaseOrder> purchaseOrderItems = new List<PurchaseOrder>();
                purchaseOrderItems = await _purchaseOrderRepository.GetList(UserId);

                return Ok(purchaseOrderItems);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while getting to purchaseOrder.");
            }
        }
        [HttpPost]
        [Route("api/purchaseorder")]
        public async Task<ActionResult<Products>> AddNew(List<PurchaseOrder> purchaseOrder)
        {
            try
            {
                List<PurchaseOrder> purchaseOrderItems = new List<PurchaseOrder>();

                purchaseOrderItems = await _purchaseOrderRepository.GenerateOrderAndItems(purchaseOrder);

                return Ok(purchaseOrderItems);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while adding to purchaseOrder.");
            }
        }
    }
}
