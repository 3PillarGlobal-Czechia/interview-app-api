using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IGenericRepository<TModel, TEntity>
    {
        Task<IEnumerable<TModel>> GetAll();

        Task<TModel> GetById(params object[] key);

        Task<bool> Create(TModel entity);

        Task<bool> BulkCreate(IEnumerable<TModel> entity);

        Task<bool> Update(TModel entity);

        Task<bool> Delete(params object[] key);
    }
}
