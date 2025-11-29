using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Entities;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("Auth")]
    public class AuthController(IAuthServices authServices):ControllerBase
    {
        [HttpPost]
        [Route("SignUp/{Role}")]
        public async Task<IActionResult> SignUp([FromBody] RegisterDto registerDto, [FromRoute] string Role)
        {
            var result = await authServices.SignUpAsync(registerDto, Role);

            if (result.Contains("successfully"))
                return Ok(result);
            
            return BadRequest(result);


        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var result = await authServices.LoginAsync(loginDto);
            return result.Contains("not Correct") || result.Contains("not Found") || result.Contains("Confirm") ? BadRequest(result) : Ok(result);
        }

        [HttpGet]
        [Route("ConfirmEmail")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string userId, [FromQuery] string token)
        {
            string decodedToken = Uri.UnescapeDataString(token);
            var result = await authServices.ConfirmEmailAsync(userId, decodedToken);

            if (result)
            {
                return Content("<h2 style='color:green;'> Email confirmed successfully!</h2>", "text/html");
            }
            else
            {
                return Content("<h2 style='color:red;'> Invalid or expired confirmation link.</h2>", "text/html");
            }
        }


    }
    
}
