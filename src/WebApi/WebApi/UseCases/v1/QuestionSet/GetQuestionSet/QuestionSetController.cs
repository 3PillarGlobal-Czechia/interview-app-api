﻿using Application.UseCases.QuestionList.GetQuestionList;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebApi.UseCases.v1.QuestionSet.GetQuestionSet;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class QuestionSetController : ControllerBase, IOutputPort
{
    private IActionResult _viewModel;

    private readonly IGetQuestionListUseCase _useCase;

    public QuestionSetController(IGetQuestionListUseCase useCase)
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