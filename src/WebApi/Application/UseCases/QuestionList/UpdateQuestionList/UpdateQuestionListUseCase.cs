using Application.Repositories;
using System;
using System.Threading.Tasks;

namespace Application.UseCases.QuestionList.UpdateQuestionList;

public class UpdateQuestionListUseCase : IUpdateQuestionListUseCase
{
    private IOutputPort _outputPort;

    private readonly IQuestionListRepository _questionListRepository;

    public UpdateQuestionListUseCase(IQuestionListRepository questionListRepository)
    {
        _outputPort = new UpdateQuestionListPresenter();
        _questionListRepository = questionListRepository;
    }

    public async Task Execute(UpdateQuestionListInput input)
    {
        if (input == null)
        {
            throw new ArgumentNullException(nameof(input));
        }

        await UpdateQuestionListInternal(input);
    }

    public void SetOutputPort(IOutputPort outputPort) => _outputPort = outputPort;

    private async Task UpdateQuestionListInternal(UpdateQuestionListInput input)
    {
        var list = await _questionListRepository.GetById(input.Id);

        if (list == null)
        {
            _outputPort.NotFound();
            return;
        }

        list.Title = input.Title;
        list.Description = input.Description;

        bool isUpdated = await _questionListRepository.Update(list);

        if (isUpdated)
        {
            _outputPort.Ok();
        }
        else
        {
            _outputPort.Invalid();
        }
    }
}
