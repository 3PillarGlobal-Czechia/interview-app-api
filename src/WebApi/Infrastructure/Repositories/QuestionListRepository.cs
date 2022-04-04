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

        try
        {
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
            
                await DbContext.SaveChangesAsync();
                DbContext.Entry(entity).State = EntityState.Detached;
            }

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

        DbContext.QuestionListInterviewQuestions.Remove(entity);
        await DbContext.SaveChangesAsync();

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

        try
        {
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

    public async Task<bool> UpdateQuestionOrder(int questionSetId, IList<int> orderedQuestionIds)
    {
        DateTime now = DateTime.Now;
        try
        {
            var orderedArray = orderedQuestionIds.ToArray();
            var relations = DbContext.QuestionListInterviewQuestions.Where(qliq => qliq.QuestionListId == questionSetId && orderedQuestionIds.Contains(qliq.InterviewQuestionId)).AsEnumerable();
            
            // Order relations by provided ids
            relations = relations.OrderBy(qliq => { return Array.IndexOf(orderedArray, qliq.InterviewQuestionId); });

            // Update relations order property to index + 1
            relations = relations.Select((qliq, index) => { qliq.Order = index + 1; qliq.UpdatedAt = now; return qliq; });

            DbContext.UpdateRange(relations);
            return await DbContext.SaveChangesAsync() > 0;
        }
        catch (Exception _) when (_ is DbUpdateException || _ is InvalidOperationException)
        {
            return false;
        }
    }
}
