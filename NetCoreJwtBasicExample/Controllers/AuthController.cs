using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetCoreJwtBasicExample.Helpers;

namespace NetCoreJwtBasicExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly TokenGenerator TokenGenerator;
        public AuthController()
        {
            TokenGenerator = new TokenGenerator("NetCoreJwtBasicExample");
        }

        //api/Auth/Login
        [HttpGet("[action]")]
        public IActionResult Login()
        {
            return Created("", TokenGenerator.CreateToken());
        }

        //api/Auth/AdminLogin
        [HttpGet("[action]")]
        public IActionResult AdminLogin()
        {
            return Created("", TokenGenerator.CreateTokenWithAdminRole());
        }

        //api/Auth/AdminDashboard
        [Authorize(Roles = "Admin")]
        [HttpGet("[action]")]
        public IActionResult AdminDashboard()
        {
            return Ok("You have authorized successfully with an admin role");
        }

        //api/Auth/Dashboard
        [Authorize]
        [HttpGet("[action]")]
        public IActionResult Dashboard()
        {
            return Ok("You have authorized successfully");
        }
    }
}