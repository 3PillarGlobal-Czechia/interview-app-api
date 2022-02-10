using Application.UseCases.QuestionList.GetQuestionList;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.UseCases.v1.QuestionList.GetQuestionList;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class QuestionListsController : ControllerBase, IOutputPort
{
    private IActionResult _viewModel;

    private readonly IGetQuestionListUseCase _useCase;

    public QuestionListsController(IGetQuestionListUseCase useCase)
    {
        _useCase = useCase;
    }

    void IOutputPort.Invalid()
    {
        _viewModel = BadRequest();
    }

    void IOutputPort.Ok(IEnumerable<QuestionListModel> questionLists)
    {
        _viewModel = Ok(questionLists);
    }

    void IOutputPort.NotFound()
    {
        _viewModel = NotFound();
    }

    [HttpGet]
    public async Task<IActionResult> Get([Required][FromQuery] GetQuestionListRequest request)
    {
        var input = new GetQuestionListInput
        {
            Text = request.Text,
            Categories = request.Categories ?? Enumerable.Empty<string>()
        };

        _useCase.SetOutputPort(this);

        await _useCase.Execute(input);

        return _viewModel;
    }
}
