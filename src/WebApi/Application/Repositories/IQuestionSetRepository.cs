using Domain.Entities;
using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Repositories;

public interface IQuestionSetRepository : IGenericRepository<QuestionSetModel, IEntity>
{
    Task<bool> AddQuestionsToList(QuestionSetModel questionListModel, IEnumerable<int> interviewQuestionIds);

    Task<bool> RemoveQuestionsFromList(QuestionSetModel questionListModel, IEnumerable<int> interviewQuestionIds);
}
