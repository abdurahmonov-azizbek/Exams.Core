using Exams.Application.Interfaces;
using Exams.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Exams.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IAccountService accountService) : ControllerBase
    {
        [HttpPost("sign-up")]
        public async ValueTask<IActionResult> Register([FromBody] RegisterDetails registerDetails)
        {
            var result = await accountService.RegisterAsync(registerDetails);
            return Ok(result);
        }

        [HttpGet("sign-in")]
        public async ValueTask<IActionResult> Login([FromQuery] LoginDetails loginDetails)
        {
            var result = await accountService.LoginAsync(loginDetails);
            return Ok(result);
        }
    }
}
