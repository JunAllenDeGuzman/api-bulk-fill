using api_bulk_fill_v2.Features.Users.CreateUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace api_bulk_fill_v2.Features.Users.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            var result = await _mediator.Send(request);

            if (result.IsSuccess)
            {
                return StatusCode(result.StatusCode, result.Value);
            }
            else
            {
                return StatusCode(result.StatusCode, new { error = result.Error });
            }
        }
    }
}
