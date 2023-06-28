using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pizza.Exceptions;
using Pizza.Models;
using Pizza.Services;
using Pizza.Utilities;

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

        //Return JWT Token after successfull login by manager OR throw appropriate exceptions if login unsuccessfull.
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
            }catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return StatusCode(500, Message.Server_Error);//500
            }
            return Ok(Message.Login_Message + "Bearer " + token);//200
        }


        //Return details of all the orders of all registered users only after successfull login and using JWT Token as Manager Role.
        [Authorize(Roles = "manager")]
        [HttpGet("view-all-orders")]
        public IActionResult ViewAllOrders()
        {
            return Ok(_managerService.ViewAllOrders());
        }


        //Manage order status of specific order with the help of order-id only after successfull login and using JWT Token as Manager Role.
        //OR throw exception if user is unauthorized or invalid token.
        [Authorize(Roles = "manager")]
        [HttpPut("manage-order")]
        public IActionResult ManageOrder([FromQuery] string order_Id, [FromQuery] string order_status) {
            try
            {
                _managerService.ManageOrder(order_Id, order_status);
            }catch (OrderNotFound ex) {
                return NotFound(ex.Message);
            }
            return Ok(Message.Manage_Order);
        }


        //Get order details by order-id only after successfull login and using JWT Token as Manager Role.
        //OR throw exception if user is unauthorized or invalid token.
        [Authorize(Roles = "manager")]
        [HttpGet("order-details")]
        public IActionResult OrderDetails(string order_id)
        {
            Models.OrderDetails orderDetails;
            try
            {
                orderDetails = _managerService.OrderDetails(order_id);
            }catch(OrderNotFound ex) {
                return NotFound(ex.Message);
            }
            return Ok(orderDetails);
        }
    }
}
