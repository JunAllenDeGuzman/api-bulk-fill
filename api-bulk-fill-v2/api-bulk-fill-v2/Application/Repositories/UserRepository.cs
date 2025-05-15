using api_bulk_fill_v2.Application.Interfaces;
using api_bulk_fill_v2.Domain;
using api_bulk_fill_v2.Features.Users.CreateUser;
using api_bulk_fill_v2.Infrastructure.Persistence;
using AutoMapper;

namespace api_bulk_fill_v2.Application.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<CreateUserResponse> AddUser(CreateUserRequest request)
        {
            var user = _mapper.Map<User>(request);
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<CreateUserResponse>(user);
        }
    }
}
