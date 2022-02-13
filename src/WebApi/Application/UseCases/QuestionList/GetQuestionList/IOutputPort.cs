using Domain.Models;
using System.Collections.Generic;

namespace Application.UseCases.QuestionList.GetQuestionList;

public interface IOutputPort
{
    void Invalid();

    void NotFound();

    void Ok(IEnumerable<QuestionListModel> questionLists);
}
