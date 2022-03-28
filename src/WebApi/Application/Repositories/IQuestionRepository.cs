﻿using Application.UseCases.InterviewQuestion.GetInterviewQuestion;
using Domain.Entities;
using Domain.Models;
using Domain.Models.Agreggates;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Repositories;

public interface IQuestionRepository : IGenericRepository<QuestionModel, IEntity>
{
    Task<IEnumerable<QuestionModel>> Get(GetInterviewQuestionInput input);

    Task<IEnumerable<QuestionWithOrder>> GetQuestionsBySetId(int id);
}
