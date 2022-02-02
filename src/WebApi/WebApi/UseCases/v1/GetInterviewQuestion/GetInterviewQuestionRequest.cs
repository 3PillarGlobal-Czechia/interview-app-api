using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi.UseCases.v1.GetInterviewQuestion;

public record GetInterviewQuestionRequest
{
    [MaxLength(50)]
    public string Category { get; set; }

    [MaxLength(250)]
    public string Text { get; set; }

    public IEnumerable<int> Difficulties { get; set; }
}
