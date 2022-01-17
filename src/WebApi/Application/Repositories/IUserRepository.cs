using Domain.Entities;
using Domain.Models;

namespace Application.Repositories
{
    public interface IUserRepository : IGenericRepository<UserModel, IEntity>
    {
    }
}
