using Application.Repositories;
using System;
using System.Threading.Tasks;

namespace Application.UseCases.QuestionSet.UpdateQuestionOrder;

public class UpdateQuestionOrderUseCase : IUpdateQuestionOrderUseCase
{
    private IOutputPort _outputPort;

    private readonly IQuestionSetRepository _questionSetRepository;

    public UpdateQuestionOrderUseCase(IQuestionSetRepository questionSetRepository)
    {
        _questionSetRepository = questionSetRepository;
    }

    public async Task Execute(UpdateQuestionOrderInput input)
    {
        var questionSet = await _questionSetRepository.GetById(input.QuestionSetId);

        if (questionSet is null)
        {
            _outputPort.NotFound();
            return;
        }

        bool anyUpdated = await _questionSetRepository.UpdateQuestionOrder(input.QuestionSetId, input.OrderedQuestionIds);

        if (!anyUpdated)
        {
            _outputPort.Invalid();
            return;
        }

        questionSet.UpdatedAt = DateTime.Now;

        await _questionSetRepository.Update(questionSet);

        _outputPort.Ok();
    }

    public void SetOutputPort(IOutputPort outputPort) => _outputPort = outputPort;
}
