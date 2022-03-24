using Application.Repositories;
using Application.UseCases.InterviewQuestion.GetInterviewQuestion;
using AutoMapper;
using Domain.Models;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories;

public sealed class InterviewQuestionRepository : GenericRepository<QuestionModel, InterviewQuestion>, IQuestionRepository
{
    public InterviewQuestionRepository(MyDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<IEnumerable<QuestionModel>> Get(GetInterviewQuestionInput input)
    {
        IQueryable<InterviewQuestion> interviewQuestions = DbContext.InterviewQuestions;

        if (input.Category != null)
        {
            interviewQuestions = interviewQuestions.Where(x => x.Category.Equals(input.Category));
        }

        if (input.Text != null)
        {
            interviewQuestions = interviewQuestions.Where(x => x.Title.Contains(input.Text) || x.Content.Contains(input.Text));
        }

        if (input.Difficulties != null && input.Difficulties.Any())
        {
            interviewQuestions = interviewQuestions.Where(x => x.Difficulty.HasValue && input.Difficulties.Contains(x.Difficulty.Value));
        }

        var result = await interviewQuestions.ToListAsync();

        return _mapper.Map<IEnumerable<QuestionModel>>(result);
    }

    public async Task<IEnumerable<QuestionModel>> GetQuestionsBySetId(int id)
    {
        var entity = await DbContext.Set<QuestionList>().Where(x => x.Id == id).Include(q => q.InterviewQuestions).FirstOrDefaultAsync();

        if ( entity is null)
        {
            throw new ArgumentException($"Invalid paramer {nameof(id)}.");
        }

        DbContext.Entry<QuestionList>(entity).State = EntityState.Detached;

        var questions = _mapper.Map<IEnumerable<QuestionModel>>(entity.InterviewQuestions);
        return questions;
    }
}
