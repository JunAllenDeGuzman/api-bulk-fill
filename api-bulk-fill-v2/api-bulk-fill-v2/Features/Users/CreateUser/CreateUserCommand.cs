using api_bulk_fill_v2.Application.Interfaces;
using api_bulk_fill_v2.Common;
using MediatR;
using System.Net;

namespace api_bulk_fill_v2.Features.Users.CreateUser
{
    public class CreateUserCommand : IRequestHandler<CreateUserRequest, Result<CreateUserResponse>>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserCommand(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<CreateUserResponse>> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {

            var response = await _userRepository.AddUser(request);

            if (response == null)
                return Result<CreateUserResponse>.BadRequestResult("User creation failed.");

            return Result<CreateUserResponse>.SuccessResult(response);

        }
    }
}