using Microsoft.AspNetCore.Mvc;
using QuantityMeasurementModel;
using QuantityMeasurementService;
using System;

namespace QuantityMeasurementApp.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserRegisterDTO request)
        {
            try
            {
                return Ok(_authService.Register(request));
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginDTO request)
        {
            try
            {
                return Ok(_authService.Login(request));
            }
            catch (Exception ex)
            {
                return Unauthorized(new { error = ex.Message });
            }
        }
        
        // As discussed, Logout is a frontend concept, but we can provide a dummy endpoint 
        // to return instructions if someone tries to hit it via API.
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            return Ok(new { message = "To logout, please delete the JWT token from your client/browser local storage." });
        }
    }
}