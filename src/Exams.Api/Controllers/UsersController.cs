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
    }
}
