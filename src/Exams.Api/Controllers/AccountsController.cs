using Exams.Application.Interfaces;
using Exams.Domain.Entities;
using Exams.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Exams.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController(IAccountService accountService) : ControllerBase
    {
        [HttpPost("send-verification-code")]
        public async ValueTask<IActionResult> SendVerificationCode()
        {
            var userId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(claim
                => claim.Type == "Id")!.Value);

            var result = await accountService.SendVerificationCodeAsync(userId);

            return Ok(result);
        }

        [HttpPut("verify")]
        public async ValueTask<IActionResult> VerifyAccount(string code)
        {
            var userId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(claim
                           => claim.Type == "Id")!.Value);

            var result = await accountService.VerifyCodeAsync(userId, code);

            return Ok(result);
        }

        [HttpPut("update-password")]
        [Authorize]
        public async ValueTask<IActionResult> UpdatePassword([FromBody] UpdatePasswordDetails updatePasswordDetails)
        {
            var userId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "Id")!.Value);

            var result = await accountService.UpdatePasswordAsync(userId, updatePasswordDetails);

            return Ok(result);
        }   
    }
}
