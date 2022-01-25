using Application.UseCases.InterviewQuestion.UpdateInterviewQuestion;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.UseCases.v1.UpdateInterviewQuestion;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class InterviewQuestionsController : ControllerBase, IOutputPort
{
    private IActionResult _viewModel;

    private readonly IUpdateInterviewQuestionUseCase _useCase;

    public InterviewQuestionsController(IUpdateInterviewQuestionUseCase updateInterviewQuestion)
    {
        _useCase = updateInterviewQuestion;
    }

    void IOutputPort.Invalid()
    {
        _viewModel = BadRequest();
    }

    void IOutputPort.NotFound()
    {
        _viewModel = NotFound();
    }

    void IOutputPort.Ok()
    {
        _viewModel = Ok();
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> Update([FromBody] UpdateInterviewQuestionRequest request)
    {
        var input = new UpdateInterviewQuestionInput
        {
            Id = request.Id,
            Category = request.Category,
            Title = request.Title,
            Difficulty = request.Difficulty,
            Content = request.Content,
        };

        _useCase.SetOutputPort(this);

        await _useCase.Execute(input);

        return _viewModel;
    }
}
