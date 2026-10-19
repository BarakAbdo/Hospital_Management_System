using Hospital_Management_System.Application.Dtos;
using Hospital_Management_System.Application.Dtos.LoginsDtos;
using Hospital_Management_System.Application.Dtos.RegistersDtos;
using Hospital_Management_System.Application.Services.Base;
using Microsoft.AspNetCore.Mvc;


namespace Hospital_Management_System.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            bool userFound = _userService.Login(loginDto);

            if (userFound)
            {
                return Ok(new
                {
                    message = "Login Successful"
                });
            }
            else
            {
                return Unauthorized(new
                {
                    message = "Invalid username or password"
                });
            }
        }




        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterDto registerDto)
        {
            if (string.IsNullOrWhiteSpace(registerDto.Name) ||
                string.IsNullOrWhiteSpace(registerDto.Email) ||
                string.IsNullOrWhiteSpace(registerDto.Username) ||
                string.IsNullOrWhiteSpace(registerDto.Password))
            {
                return BadRequest(new
                {
                    message = "All fields are required"
                });
            }

            if (registerDto.Password.Length < 6)
            {
                return BadRequest(new
                {
                    message = "Password must be at least 6 characters"
                });
            }

            bool isRegistered = _userService.Register(registerDto);

            if (isRegistered)
            {
                return Ok(new
                {
                    message = "Registration Successful"
                });
            }

            return Conflict(new
            {
                message = "Username or Email already exists"
            });
        }




    }
}