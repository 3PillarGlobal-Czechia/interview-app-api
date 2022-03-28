using Application.Repositories;
using AutoMapper;
using Domain.Models;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories;

public class QuestionListInterviewQuestionRepository : GenericRepository<QuestionSetQuestionModel, QuestionListInterviewQuestion>, IQuestionSetQuestionRepository
{
    public QuestionListInterviewQuestionRepository(MyDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }

    public async Task<bool> Remove(int listId, int questionId)
    {
        var model = await GetById(listId, questionId);

        if (!await Delete(model.QuestionListId, model.InterviewQuestionId))
        {
            return false;
        }

        var related = await DbContext.QuestionListInterviewQuestions.Where(qliq => qliq.QuestionListId == model.QuestionListId && qliq.Order > model.Order).ToListAsync();

        if (!related.Any())
        {
            return true;
        }

        foreach (var item in related)
        {
            item.Order -= 1;
        }

        DbContext.QuestionListInterviewQuestions.UpdateRange(related);
        return await DbContext.SaveChangesAsync() > 0;
    }
}
