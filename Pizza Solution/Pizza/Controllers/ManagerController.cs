using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pizza.Exceptions;
using Pizza.Models;
using Pizza.Services;

namespace Pizza.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IManagerService _managerService;

        public ManagerController(IManagerService managerService)
        {
            this._managerService = managerService;
        }

        [HttpPost("login-manager")]
        public IActionResult Login([FromQuery]string email, [FromQuery]string password)
        {
            string token;
            try
            {
                token = _managerService.Login(email, password);

            }catch(UserNotFoundException ex) {
                return NotFound(ex.Message);
            }catch(IncorrectEmailOrPasswordException ex) { 
                return Unauthorized(ex.Message);
            }
            catch (Exception ex) {
                return StatusCode(500, "An error occurred: " + ex.Message);//500
            }

            return Ok("JWT Token: "+token);
        }

        //[HttpPost("add-menu")]
        //public IActionResult EditMenu([FromBody] Menu menu)
        //{
        //    return Ok("Item Added In Menu");
        //}

        [Authorize(Roles = "manager")]
        [HttpGet("view-all-orders")]
        public IActionResult ViewAllOrders()
        {
            return Ok(_managerService.ViewAllOrders());
        }

        [Authorize(Roles = "manager")]
        [HttpPut("manage-order")]
        public IActionResult ManageOrder([FromQuery] string order_Id, [FromQuery] string order_status) {
            try
            {
                _managerService.ManageOrder(order_Id, order_status);
            }catch (OrderNotFound ex) {
                return NotFound(ex.Message);
            }
            return Ok("Order Status Update Success");
        }

        [Authorize(Roles = "manager")]
        [HttpGet("order-details")]
        public IActionResult OrderDetails(string order_id)
        {
            Models.OrderDetails orderDetails;
            try
            {
                orderDetails = _managerService.OrderDetails(order_id);
            }catch(OrderNotFound ex)
            {
                return NotFound(ex.Message);
            }
            return Ok();
        }
    }
}
