using Domain.Models;
using System.Collections.Generic;

namespace Application.UseCases.QuestionList.GetQuestionList;

public class GetQuestionListPresenter : IOutputPort
{
    public void Invalid()
    {
        throw new System.NotImplementedException();
    }

    public void NotFound()
    {
        throw new System.NotImplementedException();
    }

    public void Ok(IEnumerable<QuestionListModel> questionLists)
    {
        throw new System.NotImplementedException();
    }
}
