using Application.UseCases.InterviewQuestion.CreateInterviewQuestion;
using Application.UseCases.Question.CreateInterviewQuestion;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;

namespace WebApi.UseCases.v1.Question.CreateQuestion;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class QuestionController : ControllerBase, IOutputPort
{
    private IActionResult _viewModel;

    private readonly ICreateQuestionUseCase _useCase;

    public QuestionController(ICreateQuestionUseCase createInterviewQuestion)
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

    [HttpPost(Name = "CreateQuestion")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Create([Required][FromBody] CreateQuestionRequest request)
    {
        var input = new CreateQuestionInput
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
