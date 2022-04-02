using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Repositories;

public interface IGenericRepository<TModel, TEntity>
{
    Task<IEnumerable<TModel>> GetAll();

    Task<TModel> GetById(params object[] id);

    Task<TModel> Create(TModel model);

    Task<bool> BulkCreate(IEnumerable<TModel> model);

    Task<bool> Update(TModel model);

    Task<bool> Delete(params object[] id);
}
