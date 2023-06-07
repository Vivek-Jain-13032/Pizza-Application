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
        
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromQuery]String email, [FromQuery]String password) {
            string JwtToken;
            try
            {
                JwtToken = _userService.login(email, password);
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
            return Ok("JWT Token: "+ JwtToken);//200
        }

        [HttpPost("signup")]
        public IActionResult Register(User user)
        {
            bool flag;
            try
            {
                flag = _userService.register(user);
            }
            catch (UserAlreadyExistsException ex)
            {
                return Conflict(ex.Message);//409
            }
            if (flag)
                return Ok("Register Successfully");

            return StatusCode(500, "An error occurred");
        }

        [HttpGet("forgetPassword")]
        public IActionResult ForgetPassword([FromQuery]string email)
        {
            string password;
            try
            {
                password = _userService.ForgetPassword(email);
            }
            catch(UserNotFoundException ex)
            {
                return NotFound(ex.Message);//409
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);//500
            }

            return Ok(password);//200
        }

        [HttpPost("view-menu")]
        public IActionResult ViewMenu(Menu menu)
        {
            return Ok("working");
        }
    }
}
