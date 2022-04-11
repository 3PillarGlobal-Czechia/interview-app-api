using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WebApi.UseCases.v1.Question.CreateQuestion;

public record CreateQuestionRequest
{
    [Required]
    [MaxLength(100)]
    public string Title { get; set; }

    [Range(0, 100, ErrorMessage = "The Difficulty field is out of range ( 0 - 100 )")]
    [Required]
    public int? Difficulty { get; set; }

    [Required]
    [MaxLength(50)]
    public string Category { get; set; }

    [Required]
    [MaxLength(250)]
    public string Content { get; set; }
}
