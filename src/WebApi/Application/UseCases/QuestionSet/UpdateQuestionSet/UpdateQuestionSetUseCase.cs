using Application.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Application.UseCases.QuestionSet.UpdateQuestionSet;

public class UpdateQuestionSetUseCase : IUpdateQuestionSetUseCase
{
    private IOutputPort _outputPort;

    private readonly IQuestionSetRepository _questionSetRepository;

    public UpdateQuestionSetUseCase(IQuestionSetRepository questionSetRepository)
    {
        _questionSetRepository = questionSetRepository;
    }

    public async Task Execute(UpdateQuestionSetInput input)
    {
        var list = await _questionSetRepository.GetById(input.Id);

        if (list is null)
        {
            _outputPort.NotFound();
            return;
        }

        list.Title = input.Title;
        list.Description = input.Description;

        bool isUpdated = await _questionSetRepository.Update(list);

        if (input.QuestionsToAdd.Any())
        {
            isUpdated &= await _questionSetRepository.AddQuestionsToList(list, input.QuestionsToAdd);
        }

        if (input.QuestionsToRemove.Any())
        {
            isUpdated &= await _questionSetRepository.RemoveQuestionsFromList(list, input.QuestionsToRemove);
        }

        if (!isUpdated)
        {
            _outputPort.Invalid();
            return;
        }

        _outputPort.Ok();
    }

    public void SetOutputPort(IOutputPort outputPort) => _outputPort = outputPort;
}
