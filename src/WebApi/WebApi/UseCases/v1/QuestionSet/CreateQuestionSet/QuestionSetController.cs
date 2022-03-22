using Application.UseCases.QuestionList.CreateQuestionList;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebApi.UseCases.v1.QuestionSet.CreateQuestionSet;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class QuestionSetController : ControllerBase, IOutputPort
{
    private IActionResult _viewModel;

    private readonly ICreateQuestionSetUseCase _useCase;

    public QuestionSetController(ICreateQuestionSetUseCase useCase)
    {
        _useCase = useCase;
    }

    void IOutputPort.Invalid()
    {
        _viewModel = BadRequest();
    }

    void IOutputPort.Ok(QuestionSetModel model)
    {
        _viewModel = Ok(model);
    }

    void IOutputPort.NotFound()
    {
        _viewModel = NotFound();
    }

    [HttpPost]
    [ProducesResponseType(typeof(QuestionSetModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Create(CreateQuestionSetRequest request)
    {
        var input = new CreateQuestionSetInput
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
