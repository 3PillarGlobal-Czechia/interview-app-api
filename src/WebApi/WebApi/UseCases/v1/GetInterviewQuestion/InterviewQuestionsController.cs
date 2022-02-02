using Application.UseCases.InterviewQuestion.GetInterviewQuestion;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace WebApi.UseCases.v1.GetInterviewQuestion;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class InterviewQuestionsController : ControllerBase, IOutputPort
{
    private IActionResult _viewModel;

    private readonly IGetInterviewQuestionUseCase _useCase;

    public InterviewQuestionsController(IGetInterviewQuestionUseCase getInterviewQuestions)
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

    void IOutputPort.Ok(IEnumerable<InterviewQuestionModel> interviewQuestions)
    {
        _viewModel = Ok(interviewQuestions);
    }

    [HttpGet]
    public async Task<IActionResult> Get([Required][FromQuery] GetInterviewQuestionRequest request)
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
