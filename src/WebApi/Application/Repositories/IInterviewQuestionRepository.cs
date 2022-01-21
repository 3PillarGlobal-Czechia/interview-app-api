using Domain.Entities;
using Domain.Models;

namespace Application.Repositories
{
    public interface IInterviewQuestionRepository : IGenericRepository<InterviewQuestionModel, IEntity>
    {
    }
}
