using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Pizza.Exceptions;
using Pizza.Models;
using Pizza.Services;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using Size = Pizza.Models.Size;

namespace Pizza.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtToken jwtToken;
        
        public UserController(IUserService userService, IJwtToken jwtToken)
        {
            _userService = userService;
            this.jwtToken = jwtToken; 
        }

        [HttpPost("signup")]
        public IActionResult Register(NewUser user)
        {
            string user_id;
            try
            {
                user_id = _userService.Register(user);
            }catch (UserAlreadyExistsException ex)
            {
                return Conflict(ex.Message);//409
            }catch (Exception ex)
            {
                return StatusCode(500, "An error occurred, try again later");
            }
            
            if (user_id != "")
                return Ok("Register Successfully, "+"Your User ID Is: "+user_id);

            return StatusCode(500, "An error occurred, try again later");
        }

        [HttpPost("login")]
        public IActionResult Login([FromQuery]String email, [FromQuery]String password) {
            NewUser customer;
            string token;
            try
            {
                customer = _userService.Login(email, password);
                token = jwtToken.CreateJwtToken(customer.Email, "user");
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
            return Ok("JWT Token: "+token);//200
        }

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

        [Authorize(Roles ="user")]
        [HttpGet("view-menu")]
        public IActionResult ViewMenu()
        {
            return Ok(_userService.ViewMenu());
        }

        [Authorize(Roles = "user")]
        [HttpPost("create-order")]
        public IActionResult CreateOrder([FromBody]OrderPizza order)
        {
            return Ok(_userService.CreateOrder(order));
        }

        [Authorize(Roles = "user")]
        [HttpGet("track-order")]
        public IActionResult TrackOrders()  
        {
            return Ok(_userService.TrackOrder());
        }

        [Authorize(Roles = "user")]
        [HttpGet("order-history")]
        public IActionResult OrderHistory()
        {
            return Ok(_userService.OrderHistory());
        }
    }

}
