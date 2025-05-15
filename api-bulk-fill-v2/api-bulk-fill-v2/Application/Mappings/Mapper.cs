using api_bulk_fill_v2.Domain;
using api_bulk_fill_v2.Features.Users.CreateUser;
using AutoMapper;

namespace api_bulk_fill_v2.Application.Mappings
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<CreateUserRequest, User>();
            CreateMap<User, CreateUserResponse>();
        }
    }
}
