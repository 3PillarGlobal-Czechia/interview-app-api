using Domain.Models;
using System.Collections.Generic;

namespace Application.UseCases.QuestionSet.GetQuestionSets;

public interface IOutputPort
{
    void Invalid();

    void NotFound();

    void Ok(IEnumerable<QuestionSetModel> questionSet);
}
