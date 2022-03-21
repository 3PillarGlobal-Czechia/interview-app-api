using Application.Repositories;
using Application.UseCases.QuestionList.GetQuestionList;
using AutoMapper;
using Domain.Models;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
        DbContext.Entry(questionList).State = EntityState.Detached;

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
            DbContext.Entry(questionList).State = EntityState.Detached;
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> RemoveQuestionsFromList(QuestionListModel questionListModel, IEnumerable<int> interviewQuestionIds)
    {
        var questionList = _mapper.Map<QuestionList>(questionListModel);
        DbContext.Entry(questionList).State = EntityState.Unchanged;
        await DbContext.Entry(questionList).Collection(x => x.InterviewQuestions).LoadAsync();

        questionList.UpdatedAt = DateTime.Now;

        foreach (int id in interviewQuestionIds)
        {
            questionList.InterviewQuestions.Remove(questionList.InterviewQuestions.FirstOrDefault(x => x.Id == id));
        }

        try
        {
            DbContext.QuestionLists.Update(questionList);
            await DbContext.SaveChangesAsync();
            DbContext.Entry(questionList).State = EntityState.Detached;
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<IEnumerable<QuestionListModel>> Get(GetQuestionListInput input)
    {
        IQueryable<QuestionList> questionLists = DbContext.QuestionLists.Include(ql => ql.InterviewQuestions);

        if (input.Id != null)
        {
            return _mapper.Map<IEnumerable<QuestionListModel>>(questionLists.Where(ql => ql.Id == input.Id));
        }

        if (input.Text != null)
        {
            questionLists = questionLists.Where(ql => ql.Title.Contains(input.Text) || ql.Description.Contains(input.Text));
        }

        if (input.Categories != null && input.Categories.Any())
        {
            foreach (string category in input.Categories)
            {
                questionLists = questionLists.Where(ql => ql.InterviewQuestions.Any(iq => iq.Category == category));
            }
        }

        var result = await questionLists.ToListAsync();

        return _mapper.Map<IEnumerable<QuestionListModel>>(result);
    }
}
