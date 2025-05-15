using api_bulk_fill_v2.Common;
using MediatR;

namespace api_bulk_fill_v2.Features.Users.CreateUser
{
    public class CreateUserRequest : IRequest<Result<CreateUserResponse>>
    {
        public string? Name { get; set; }

        public string? Email { get; set; }
    }
}
