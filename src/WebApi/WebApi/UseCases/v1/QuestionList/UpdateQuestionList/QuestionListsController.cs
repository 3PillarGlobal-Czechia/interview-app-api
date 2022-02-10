using Application.UseCases.QuestionList.UpdateQuestionList;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
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
    public async Task<IActionResult> Update([Required][FromBody] UpdateQuestionListRequest request)
    {
        var input = new UpdateQuestionListInput
        {
            Id = request.Id,
            Title = request.Title,
            Description = request.Description
        };

        _useCase.SetOutputPort(this);

        await _useCase.Execute(input);

        return _viewModel;
    }
}
