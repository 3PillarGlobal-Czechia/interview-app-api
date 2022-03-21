using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi.UseCases.v1.Question.GetQuestion;

public record GetQuestionRequest
{
    [MaxLength(50)]
    public string Category { get; set; }

    [MaxLength(250)]
    public string Text { get; set; }

    public IEnumerable<int> Difficulties { get; set; }
}
