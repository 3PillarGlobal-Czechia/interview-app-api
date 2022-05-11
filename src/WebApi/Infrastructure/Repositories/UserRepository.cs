using System.Linq;
using System.Threading.Tasks;
using Application.Repositories;
using AutoMapper;
using Domain.Models;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public sealed class UserRepository : GenericRepository<UserModel, User>, IUserRepository
{

    public UserRepository(MyDbContext context, IMapper mapper) : base(context, mapper)
    {
    }
    public async Task<UserModel> GetUserByGoogleId(string googleId)
    {
        IQueryable<User> users = DbContext.Users;

        var result = await users.Where(u => u.GoogleId == googleId).FirstOrDefaultAsync();

        return _mapper.Map<UserModel>(result);
    }
}
