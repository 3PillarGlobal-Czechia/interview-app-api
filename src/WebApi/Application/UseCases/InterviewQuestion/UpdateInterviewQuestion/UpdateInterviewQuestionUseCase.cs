﻿using Application.Repositories;
using Domain.Models;
using System.Threading.Tasks;

namespace Application.UseCases.InterviewQuestion.UpdateInterviewQuestion;

public class UpdateInterviewQuestionUseCase : IUpdateInterviewQuestionUseCase
{
    private IOutputPort _outputPort;

    private IInterviewQuestionRepository _interviewQuestionRepository;

    public UpdateInterviewQuestionUseCase(IInterviewQuestionRepository interviewQuestionRepository)
    {
        _outputPort = new UpdateInterviewQuestionPresenter();
        _interviewQuestionRepository = interviewQuestionRepository;
    }

    public async Task Execute(UpdateInterviewQuestionInput input)
    {
        var existingInterviewQuestion = await _interviewQuestionRepository.GetById(input.Id);

        if (existingInterviewQuestion == null)
        {
            _outputPort.NotFound();
            return;
        }

        existingInterviewQuestion.Difficulty = input.Difficulty;
        existingInterviewQuestion.Title = input.Title;
        existingInterviewQuestion.Content = input.Content;
        existingInterviewQuestion.Category = input.Category;

        bool isUpdated = await _interviewQuestionRepository.Update(existingInterviewQuestion);

        if (!isUpdated)
        {
            _outputPort.Invalid();
        }

        _outputPort.Ok();
    }

    public void SetOutputPort(IOutputPort outputPort) => _outputPort = outputPort;
}
