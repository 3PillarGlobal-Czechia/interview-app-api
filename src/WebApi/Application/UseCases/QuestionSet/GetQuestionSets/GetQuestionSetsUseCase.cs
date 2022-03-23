﻿using Application.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Application.UseCases.QuestionSet.GetQuestionSets;

public class GetQuestionSetsUseCase : IGetQuestionSetsUseCase
{
    private IOutputPort _outputPort;

    private readonly IQuestionSetRepository _questionSetRepository;

    public GetQuestionSetsUseCase(IQuestionSetRepository questionSetRepository)
    {
        _questionSetRepository = questionSetRepository;
    }

    public async Task Execute(GetQuestionSetsInput input)
    {
        // TODO: implement filtering in separate method
        var questionSets = await _questionSetRepository.GetAll();

        // TODO: remap model to QuestionSetItem
        if (questionSets is null || !questionSets.Any())
        {
            _outputPort.NotFound();
            return;
        }

        _outputPort.Ok(questionSets);
    }

    public void SetOutputPort(IOutputPort outputPort) => _outputPort = outputPort;
}
