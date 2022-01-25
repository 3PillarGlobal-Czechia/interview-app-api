using Domain.Models;
using System.Collections.Generic;

namespace WebApi.UseCases.v1.GetInterviewQuestion;

public record GetInterviewQuestionResponse
{
    public IEnumerable<InterviewQuestionModel> InterviewQuestions { get; set; }
}
