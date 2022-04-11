using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApi.Extensions;

namespace WebApi.UseCases.v1.Question.GetQuestion;

public record GetQuestionRequest
{
    [MaxLength(50)]
    public string Category { get; set; }

    [MaxLength(250)]
    public string Text { get; set; }

    [RangeValidationAttributeExtension(Min = 0, Max = 100, ErrorMessage = "The Difficulty field is out of range ( 0 <= Difficulty <= 100 )")]
    public IEnumerable<int> Difficulties { get; set; }
}
