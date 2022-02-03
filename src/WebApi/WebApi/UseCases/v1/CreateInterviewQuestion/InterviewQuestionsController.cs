using Application.UseCases.InterviewQuestion.CreateInterviewQuestion;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace WebApi.UseCases.v1.CreateInterviewQuestion;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class InterviewQuestionsController : ControllerBase, IOutputPort
{
    private IActionResult _viewModel;

    private readonly ICreateInterviewQuestionUseCase _useCase;

    public InterviewQuestionsController(ICreateInterviewQuestionUseCase createInterviewQuestion)
    {
        _useCase = createInterviewQuestion;
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
    public async Task<IActionResult> Create([Required][FromBody] CreateInterviewQuestionRequest request)
    {
        var input = new CreateInterviewQuestionInput
        {
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
