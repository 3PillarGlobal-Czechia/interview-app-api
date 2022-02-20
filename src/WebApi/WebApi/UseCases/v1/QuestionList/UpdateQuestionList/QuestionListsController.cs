using Application.UseCases.QuestionList.UpdateQuestionList;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebApi.UseCases.v1.QuestionList.UpdateQuestionList;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class QuestionListsController : ControllerBase, IOutputPort
{
    private IActionResult _viewModel;

    private readonly IUpdateQuestionListUseCase _useCase;

    public QuestionListsController(IUpdateQuestionListUseCase useCase)
    {
        _useCase = useCase;
    }

    void IOutputPort.Invalid()
    {
        _viewModel = BadRequest();
    }

    void IOutputPort.Ok()
    {
        _viewModel = Ok();
    }

    void IOutputPort.NotFound()
    {
        _viewModel = NotFound();
    }

    [HttpPost]
    [Route("[action]")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Update([Required][FromBody] UpdateQuestionListRequest request)
    {
        // Check if a question is both being added and removed from list, if so we can ignore it
        var questionsToAddAndRemove = request.QuestionsToAdd?.Intersect(request.QuestionsToRemove ?? Enumerable.Empty<int>()) ?? Enumerable.Empty<int>();

        var input = new UpdateQuestionListInput
        {
            Id = request.Id,
            Title = request.Title,
            Description = request.Description,
            QuestionsToAdd = request.QuestionsToAdd?.Where(x => !questionsToAddAndRemove.Contains(x)) ?? Enumerable.Empty<int>(),
            QuestionsToRemove = request.QuestionsToRemove?.Where(x => !questionsToAddAndRemove.Contains(x)) ?? Enumerable.Empty<int>(),
        };

        _useCase.SetOutputPort(this);

        await _useCase.Execute(input);

        return _viewModel;
    }
}
