﻿using Application.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Application.UseCases.QuestionSet.GetQuestionSet;

public class GetQuestionSetUseCase : IGetQuestionSetUseCase
{
    private IOutputPort _outputPort;

    private readonly IQuestionSetRepository _questionSetRepository;

    public GetQuestionSetUseCase(IQuestionSetRepository questionSetRepository)
    {
        _questionSetRepository = questionSetRepository;
    }

    public async Task Execute(GetQuestionSetInput input)
    {
        if (input is null)
        {
            throw new ArgumentNullException(nameof(input));
        }

        await GetQuestionSetInternal(input);
    }

    public void SetOutputPort(IOutputPort outputPort) => _outputPort = outputPort;

    private async Task GetQuestionSetInternal(GetQuestionSetInput input)
    {
        var questionSet = await _questionSetRepository.GetById(input.Id);

        if (questionSet is null)
        {
            _outputPort.NotFound();
            return;
        }

        _outputPort.Ok(questionSet);
    }
}