using Application.UseCases.QuestionSet.UpdateQuestionOrder;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;

namespace WebApi.UseCases.v1.QuestionSet.UpdateQuestionOrder;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class QuestionSetController : ControllerBase, IOutputPort
{
    private IActionResult _viewModel;

    private readonly IUpdateQuestionOrderUseCase _useCase;

    public QuestionSetController(IUpdateQuestionOrderUseCase useCase)
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

    [HttpPost("{id}/UpdateQuestionOrder")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Update([Required][FromRoute] int id, [Required][FromBody] UpdateQuestionOrderRequest request)
    {
        var input = new UpdateQuestionOrderInput
        {
            QuestionSetId = id,
            OrderedQuestionIds = request.OrderedQuestionIds,
        };

        _useCase.SetOutputPort(this);

        await _useCase.Execute(input);

        return _viewModel;
    }
}
