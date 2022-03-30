using Domain.Entities;
using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Repositories;

public interface IQuestionSetRepository : IGenericRepository<QuestionSetModel, IEntity>
{
    Task<bool> AddQuestionsToList(int questionSetId, IEnumerable<int> questionIds);

    Task<bool> RemoveQuestionsFromList(int questionSetId, IEnumerable<int> questionIds);
}
