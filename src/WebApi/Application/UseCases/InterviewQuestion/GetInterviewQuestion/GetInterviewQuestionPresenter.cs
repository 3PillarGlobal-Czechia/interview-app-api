using Domain.Models;
using System.Collections.Generic;

namespace Application.UseCases.InterviewQuestion.GetInterviewQuestion;

public class GetInterviewQuestionPresenter : IOutputPort
{
    public void Invalid()
    {
        throw new System.NotImplementedException();
    }

    public void NotFound()
    {
        throw new System.NotImplementedException();
    }

    public void Ok(IEnumerable<InterviewQuestionModel> interviewQuestions)
    {
        throw new System.NotImplementedException();
    }
}
