using api_bulk_fill_v2.Features.Users.CreateUser;

namespace api_bulk_fill_v2.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<CreateUserResponse> AddUser(CreateUserRequest user);
    }
}
