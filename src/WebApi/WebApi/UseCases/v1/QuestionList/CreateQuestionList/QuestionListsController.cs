using Application.UseCases.QuestionList.CreateQuestionList;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebApi.UseCases.v1.QuestionList.CreateQuestionList;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class QuestionListsController : ControllerBase, IOutputPort
{
    private IActionResult _viewModel;

    private readonly ICreateQuestionListUseCase _useCase;

    public QuestionListsController(ICreateQuestionListUseCase useCase)
    {
        _useCase = useCase;
    }

    void IOutputPort.Invalid()
    {
        _viewModel = BadRequest();
    }

    void IOutputPort.Ok(QuestionListModel model)
    {
        _viewModel = Ok(model);
    }

    void IOutputPort.NotFound()
    {
        _viewModel = NotFound();
    }

    [HttpPost]
    [Route("[action]")]
    [ProducesResponseType(typeof(QuestionListModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
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
