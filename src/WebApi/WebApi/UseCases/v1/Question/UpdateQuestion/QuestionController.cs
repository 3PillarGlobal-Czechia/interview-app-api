using Application.UseCases.InterviewQuestion.UpdateInterviewQuestion;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;

namespace WebApi.UseCases.v1.Question.UpdateQuestion;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class QuestionController : ControllerBase, IOutputPort
{
    private IActionResult _viewModel;

    private readonly IUpdateInterviewQuestionUseCase _useCase;

    public QuestionController(IUpdateInterviewQuestionUseCase updateInterviewQuestion)
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

    [HttpPut("{id}", Name = "UpdateQuestion")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Update(int id, [Required][FromBody] UpdateQuestionRequest request)
    {
        var input = new UpdateInterviewQuestionInput
        {
            Id = id,
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
