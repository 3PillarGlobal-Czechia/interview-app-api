using Application.UseCases.QuestionList.GetQuestionList;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebApi.UseCases.v1.QuestionList.GetQuestionList;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class QuestionListController : ControllerBase, IOutputPort
{
    private IActionResult _viewModel;

    private readonly IGetQuestionListUseCase _useCase;

    public QuestionListController(IGetQuestionListUseCase useCase)
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
    [Route("{id}")]
    [ProducesResponseType(typeof(IEnumerable<QuestionListModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Get(int id)
    {
        var input = new GetQuestionListInput
        {
            Id = id,
            Text = null,
            Categories = Enumerable.Empty<string>()
        };

        _useCase.SetOutputPort(this);

        await _useCase.Execute(input);

        return _viewModel;
    }
}
