using Domain.Entities;
using Domain.Models;
using System.Threading.Tasks;

namespace Application.Repositories;

public interface IQuestionSetQuestionRepository : IGenericRepository<QuestionSetQuestionModel, IEntity>
{
    Task<bool> Remove(int listId, int questionId);
}
