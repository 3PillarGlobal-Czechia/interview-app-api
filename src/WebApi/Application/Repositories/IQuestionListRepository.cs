using Application.UseCases.QuestionList.GetQuestionList;
using Domain.Entities;
using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Repositories;

public interface IQuestionListRepository : IGenericRepository<QuestionListModel, IEntity>
{
    Task<bool> AddQuestionsToList(QuestionListModel questionListModel, IEnumerable<int> interviewQuestionIds);
    Task<IEnumerable<QuestionListModel>> Get(GetQuestionListInput input);
}
