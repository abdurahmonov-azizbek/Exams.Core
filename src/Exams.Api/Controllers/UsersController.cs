using Exams.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Exams.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController(IUserService userService) : ControllerBase
    {
        [HttpGet("get-all")]
        public ValueTask<IActionResult> Get()
        {
            var result = userService.GetAll();

            return new(result.Any()
                ? Ok(result)
                : NoContent());
        }

        [HttpGet("{userId:guid}")]
        public async ValueTask<IActionResult> GetById([FromRoute] Guid userId)
        {
            var result = await userService.GetByIdAsync(userId);

            return result is not null
                ? Ok(result)
                : NotFound();
        }

        [HttpGet("me")]
        [Authorize]
        public async ValueTask<IActionResult> GetCurrentUser()
        {
            var userId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "Id")!.Value);

            var result = await userService.GetByIdAsync(userId);

            return result is not null
                ? Ok(result)
                : BadRequest();
        }
    }
}
