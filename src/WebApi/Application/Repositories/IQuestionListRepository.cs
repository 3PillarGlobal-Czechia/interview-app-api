using Domain.Entities;
using Domain.Models;

namespace Application.Repositories;

public interface IQuestionListRepository : IGenericRepository<QuestionListModel, IEntity>
{
}
