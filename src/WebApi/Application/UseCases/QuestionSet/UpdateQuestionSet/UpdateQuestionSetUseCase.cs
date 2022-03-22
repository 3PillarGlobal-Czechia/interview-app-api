using Application.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Application.UseCases.QuestionSet.UpdateQuestionList;

public class UpdateQuestionSetUseCase : IUpdateQuestionSetUseCase
{
    private IOutputPort _outputPort;

    private readonly IQuestionSetRepository _questionListRepository;

    public UpdateQuestionSetUseCase(IQuestionSetRepository questionListRepository)
    {
        _questionListRepository = questionListRepository;
    }

    public async Task Execute(UpdateQuestionSetInput input)
    {
        if (input is null)
        {
            throw new ArgumentNullException(nameof(input));
        }

        await UpdateQuestionListInternal(input);
    }

    public void SetOutputPort(IOutputPort outputPort) => _outputPort = outputPort;

    private async Task UpdateQuestionListInternal(UpdateQuestionSetInput input)
    {
        var list = await _questionListRepository.GetById(input.Id);

        if (list is null)
        {
            _outputPort.NotFound();
            return;
        }

        list.Title = input.Title;
        list.Description = input.Description;

        bool isUpdated = await _questionListRepository.Update(list);

        if (input.QuestionsToAdd.Any())
        {
            isUpdated &= await _questionListRepository.AddQuestionsToList(list, input.QuestionsToAdd);
        }

        if (input.QuestionsToRemove.Any())
        {
            isUpdated &= await _questionListRepository.RemoveQuestionsFromList(list, input.QuestionsToRemove);
        }

        if (!isUpdated)
        {
            _outputPort.Invalid();
            return;
        }

        _outputPort.Ok();
    }
}
