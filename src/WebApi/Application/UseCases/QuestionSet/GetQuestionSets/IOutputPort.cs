using Domain.Models;
using Domain.Models.Views;
using System.Collections.Generic;

namespace Application.UseCases.QuestionSet.GetQuestionSets;

public interface IOutputPort
{
    void Invalid();

    void NotFound();

    void Ok(IEnumerable<QuestionSetListItem> questionSet);
}
