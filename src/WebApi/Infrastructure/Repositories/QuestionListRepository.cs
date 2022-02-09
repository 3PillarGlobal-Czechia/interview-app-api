using Application.Repositories;
using AutoMapper;
using Domain.Models;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories;

public class QuestionListRepository : GenericRepository<QuestionListModel, QuestionList>, IQuestionListRepository
{
    public QuestionListRepository(MyDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }

    public async Task<bool> AddQuestionsToList(QuestionListModel questionListModel, IEnumerable<int> interviewQuestionIds)
    {
        var questionList = _mapper.Map<QuestionList>(questionListModel);

        questionList.UpdatedAt = DateTime.Now;

        foreach (int id in interviewQuestionIds)
        {
            var question = new InterviewQuestion { Id = id };
            questionList.InterviewQuestions.Add(question);
            DbContext.Entry(question).State = EntityState.Unchanged;
        }

        try
        {
            DbContext.QuestionLists.Update(questionList);
            await DbContext.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException)
        {
            return false;
        }
    }
}
