using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Pizza.Exceptions;
using Pizza.Models;
using Pizza.Services;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Size = Pizza.Models.Size;

namespace Pizza.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        //Register new user account OR throw appropriate exception if registration unsuccessfull.
        [HttpPost("signup")]
        public IActionResult Register(NewUser user)
        {
            string user_id;
            try
            {
                user_id = _userService.Register(user);
            }catch (UserAlreadyExistsException ex) {
                return Conflict(ex.Message);//409
            }catch (Exception ex) {
                return StatusCode(500, "An error occurred, try again later");
            }
            if (user_id != "")
            {
                return Ok("Register Successfully, "+"Your User ID Is: "+user_id);
            }
            return StatusCode(500, "An error occurred, try again later");
        }

        //Login registered user OR throw appropriate exception if login unsuccessfull.
        [HttpPost("login")]
        public IActionResult Login([FromQuery]String email, [FromQuery]String password) {
            string token;
            try
            {
                token = _userService.Login(email, password);
            }
            catch(UserNotFoundException ex)
            {
                return NotFound(ex.Message);//404
            }
            catch(IncorrectEmailOrPasswordException ex)
            {
                return Unauthorized(ex.Message);//401
            }
            catch(Exception ex){
                return StatusCode(500, "An error occurred: "+ ex.Message);//500
            }
            //return JWT Token.
            return Ok("JWT Token: "+"Bearer "+token);//200
        }

        //Return user password by taking correct user-id & email.
        //OR throw appropriate exception if request unsuccessfull.
        [HttpPost("forget-password")]
        public IActionResult ForgetPassword([FromQuery]string user_id, [FromQuery]string email)
        {
            string password;
            try
            {
                password = _userService.ForgetPassword(user_id, email);
            }
            catch(UserNotFoundException ex)
            {
                return NotFound(ex.Message);//404
            }catch(IncorrectEmailOrPasswordException ex)
            {
                return NotFound(ex.Message);//400
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);//500
            }

            return Ok("Your Password is: "+password);//200
        }

        //View menu only after successfull login and using JWT Token as User Role.
        [Authorize(Roles ="user")]
        [HttpGet("view-menu")]
        public IActionResult ViewMenu()
        {
            return Ok(_userService.ViewMenu());
        }

        //Create order after login and using JWT Token as User Role.
        [Authorize(Roles = "user")]
        [HttpPost("create-order")]
        public IActionResult CreateOrder([FromBody]OrderPizza order)
        {
            return Ok(_userService.CreateOrder(order));
        }

        //For login user, Track all orders which are not Delivered.
        [Authorize(Roles = "user")]
        [HttpGet("track-order")]
        public IActionResult TrackOrders()  
        {
            return Ok(_userService.TrackOrder());
        }

        //Show all the order details for login user.
        [Authorize(Roles = "user")]
        [HttpGet("order-history")]
        public IActionResult OrderHistory()
        {
            return Ok(_userService.OrderHistory());
        }

    }

}
