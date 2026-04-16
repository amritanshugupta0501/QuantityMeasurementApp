using Google.Apis.Auth;
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
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            return Ok(new { message = "To logout, please delete the JWT token from your client/browser local storage." });
        }
        [HttpPost("google")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleAuthRequest request)
        {
            try
            {
                var payload = await GoogleJsonWebSignature.ValidateAsync(request.Token, new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = new[] { "YOUR_GOOGLE_CLIENT_ID_HERE" } 
            });
            string email = payload.Email;
            string name = payload.Name;
            var myToken = _authService.GoogleLogin(email, name);
            return Ok(new { token = myToken.Token, message = "Google Login Successful!" });
            }
            catch (InvalidJwtException)
            {
                return Unauthorized(new { error = "Invalid Google Token." });
            }
        }
    }
}
