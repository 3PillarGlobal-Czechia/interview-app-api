using Domain.Entities;
using Domain.Models;

namespace Application.Repositories
{
    public interface IAccountInterface : IGenericRepository<AccountModel, IEntity>
    {
    }
}
