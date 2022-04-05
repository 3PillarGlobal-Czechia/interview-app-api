using Application.UseCases.QuestionSet.DeleteQuestionSet;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace WebApi.UseCases.v1.QuestionSet.DeleteQuestionSet;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class QuestionSetController : ControllerBase, IOutputPort
{

    private IActionResult _viewModel;

    private readonly IDeleteQuestionSetUseCase _useCase;

    public QuestionSetController(IDeleteQuestionSetUseCase useCase)
    {
        _useCase = useCase;
    }

    void IOutputPort.Invalid()
    {
        _viewModel = BadRequest();
    }

    void IOutputPort.NotFound()
    {
        _viewModel = NotFound();
    }

    void IOutputPort.NoContent()
    {
        _viewModel = NoContent();
    }

    [HttpDelete("{id}", Name = "DeleteQuestionSet")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> Delete(int id)
    {
        var input = new DeleteQuestionSetInput
        {
            Id = id
        };

        _useCase.SetOutputPort(this);

        await _useCase.Execute(input);

        return _viewModel;
    }
}
