using Application.Repositories;
using AutoMapper;
using Domain.Models;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class QuestionListRepository : GenericRepository<QuestionListModel, QuestionList>, IQuestionListRepository
{
    public QuestionListRepository(MyDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }
}
