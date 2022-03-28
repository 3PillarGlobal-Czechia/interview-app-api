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
    public QuestionListRepository(MyDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }

    public async Task<bool> AddQuestionsToList(int questionListId, IEnumerable<int> interviewQuestionIds)
    {
        QuestionList list = await DbContext.QuestionLists.FindAsync(questionListId);
        DbContext.Entry(list).State = EntityState.Detached;

        list.UpdatedAt = DateTime.Now;

        int currentMaxOrder = await DbContext.QuestionListInterviewQuestions.Where(qliq => qliq.QuestionListId == list.Id).CountAsync();

        foreach (int id in interviewQuestionIds)
        {
            DateTime now = DateTime.Now;
            var entity = new QuestionListInterviewQuestion
            {
                Order = ++currentMaxOrder,
                QuestionListId = list.Id,
                InterviewQuestionId = id,
                CreatedAt = now,
                UpdatedAt = now,
            };
            
            await DbContext.QuestionListInterviewQuestions.AddAsync(entity);
            
            try
            {
                await DbContext.SaveChangesAsync();
                DbContext.Entry(entity).State = EntityState.Detached;
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

    private async Task<bool> RemoveRelation(int questionSetId, int questionId)
    {
        var entity = await DbContext.QuestionListInterviewQuestions.FindAsync(questionSetId, questionId);
        if (entity is null)
        {
            return false;
        }

        try
        {
            DbContext.QuestionListInterviewQuestions.Remove(entity);
            await DbContext.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return false;
        }

        var related = await DbContext.QuestionListInterviewQuestions.Where(qliq => qliq.QuestionListId == questionSetId && qliq.Order > entity.Order).ToListAsync();

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

            anyRemoved |= await RemoveRelation(toRemove.QuestionListId, toRemove.InterviewQuestionId);
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
