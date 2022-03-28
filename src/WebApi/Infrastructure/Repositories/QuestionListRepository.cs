using Application.Repositories;
using AutoMapper;
using Domain.Models;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories;

public class QuestionListRepository : GenericRepository<QuestionSetModel, QuestionList>, IQuestionSetRepository
{
    private readonly IQuestionSetQuestionRepository _questionSetQuestionRepository;

    public QuestionListRepository(MyDbContext dbContext, IMapper mapper, IQuestionSetQuestionRepository questionSetQuestionRepository) : base(dbContext, mapper)
    {
        _questionSetQuestionRepository = questionSetQuestionRepository;
    }

    public async Task<bool> AddQuestionsToList(int questionListId, IEnumerable<int> interviewQuestionIds)
    {
        QuestionList list = await DbContext.QuestionLists.FindAsync(questionListId);
        DbContext.Entry(list).State = EntityState.Detached;

        list.UpdatedAt = DateTime.Now;

        int currentMaxOrder = await DbContext.QuestionListInterviewQuestions.Where(qliq => qliq.QuestionListId == list.Id).CountAsync();

        foreach (int id in interviewQuestionIds)
        {
            var created = await _questionSetQuestionRepository.Create(new QuestionSetQuestionModel
            {
                Order = ++currentMaxOrder,
                QuestionListId = list.Id,
                InterviewQuestionId = id,
            });

            if (created is null)
            {
                return false;
            }
        }

        try
        {
            DbContext.QuestionLists.Update(list);
            await DbContext.SaveChangesAsync();
            DbContext.Entry(list).State = EntityState.Detached;
            return true;
        }
        catch (DbUpdateException)
        {
            return false;
        }
        catch (InvalidOperationException)
        {
            return false;
        }
    }

    public async Task<bool> RemoveQuestionsFromList(int questionListId, IEnumerable<int> interviewQuestionIds)
    {
        QuestionList list = await DbContext.QuestionLists.FindAsync(questionListId);
        await DbContext.Entry(list).Collection(ql => ql.QuestionListInterviewQuestions).LoadAsync();

        bool anyRemoved = false;

        foreach (int id in interviewQuestionIds)
        {
            var toRemove = list.QuestionListInterviewQuestions.FirstOrDefault(qliq => qliq.InterviewQuestionId == id);

            if (toRemove is null)
            {
                return false;
            }

            anyRemoved |= await _questionSetQuestionRepository.Remove(toRemove.QuestionListId, toRemove.InterviewQuestionId);
        }

        if (!anyRemoved)
        {
            return false;
        }

        list.UpdatedAt = DateTime.Now;

        try
        {
            DbContext.QuestionLists.Update(list);
            await DbContext.SaveChangesAsync();
            DbContext.Entry(list).State = EntityState.Detached;
            return true;
        }
        catch (DbUpdateException)
        {
            return false;
        }
        catch (InvalidOperationException)
        {
            return false;
        }
    }
}
