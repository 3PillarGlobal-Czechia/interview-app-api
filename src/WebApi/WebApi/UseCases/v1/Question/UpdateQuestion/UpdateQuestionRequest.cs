using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WebApi.UseCases.v1.Question.UpdateQuestion;

public record UpdateQuestionRequest
{
    [MaxLength(100)]
    public string Title { get; set; }

    [Range(0, 100, ErrorMessage = "The Difficulty field is out of range ( 0 - 100 )")]
    public int? Difficulty { get; set; }

    [MaxLength(50)]
    public string Category { get; set; }

    [MaxLength(250)]
    public string Content { get; set; }
}
