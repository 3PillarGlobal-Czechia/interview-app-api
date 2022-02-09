using Application.UseCases.QuestionList.CreateQuestionList;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.UseCases.v1.QuestionList.CreateQuestionList;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class QuestionListsController : ControllerBase, IOutputPort
{
    private IActionResult _viewModel;

    private ICreateQuestionListUseCase _useCase;

    public QuestionListsController(ICreateQuestionListUseCase useCase)
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
    public async Task<IActionResult> Create(CreateQuestionListRequest request)
    {
        var input = new CreateQuestionListInput
        {
            Title = request.Title,
            Description = request.Description,
            InterviewQuestionIds = request.InterviewQuestionIds ?? Enumerable.Empty<int>()
        };

        _useCase.SetOutputPort(this);

        await _useCase.Execute(input);

        return _viewModel;
    }
}
