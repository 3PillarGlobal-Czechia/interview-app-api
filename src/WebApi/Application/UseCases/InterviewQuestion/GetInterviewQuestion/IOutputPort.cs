using Domain.Models;
using System.Collections.Generic;

namespace Application.UseCases.InterviewQuestion.GetInterviewQuestion;

public interface IOutputPort
{
    void Invalid();

    void NotFound();

    void Ok(IEnumerable<InterviewQuestionModel> interviewQuestions);
}
