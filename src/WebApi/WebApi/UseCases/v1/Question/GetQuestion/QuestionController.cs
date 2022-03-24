using Application.UseCases.InterviewQuestion.GetInterviewQuestion;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;

namespace WebApi.UseCases.v1.Question.GetQuestion;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class QuestionController : ControllerBase, IOutputPort
{
    private IActionResult _viewModel;

    private readonly IGetInterviewQuestionUseCase _useCase;

    public QuestionController(IGetInterviewQuestionUseCase getInterviewQuestions)
    {
        _useCase = getInterviewQuestions;
    }

    void IOutputPort.Invalid()
    {
        _viewModel = BadRequest();
    }

    void IOutputPort.NotFound()
    {
        _viewModel = NotFound();
    }

    void IOutputPort.Ok(IEnumerable<QuestionModel> interviewQuestions)
    {
        _viewModel = Ok(interviewQuestions);
    }

    [HttpGet(Name = "GetQuestions")]
    [ProducesResponseType(typeof(IEnumerable<QuestionModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Get([Required][FromQuery] GetQuestionRequest request)
    {
        var input = new GetInterviewQuestionInput
        {
            Category = request.Category,
            Text = request.Text,
            Difficulties = request.Difficulties
        };

        _useCase.SetOutputPort(this);

        await _useCase.Execute(input);

        return _viewModel;
    }
}
