using Application.UseCases.InterviewQuestion.GetInterviewQuestion;
using Domain.Entities;
using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Repositories;

public interface IInterviewQuestionRepository : IGenericRepository<InterviewQuestionModel, IEntity>
{
    Task<IEnumerable<InterviewQuestionModel>> Get(GetInterviewQuestionInput input);
}
