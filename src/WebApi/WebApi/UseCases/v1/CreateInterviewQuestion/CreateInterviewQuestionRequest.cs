using System.ComponentModel.DataAnnotations;

namespace WebApi.UseCases.v1.CreateInterviewQuestion;

public record CreateInterviewQuestionRequest
{
    [Required]
    [MaxLength(100)]
    public string Title { get; set; }

    [Range(1, 5)]
    public int? Difficulty { get; set; }

    [Required]
    [MaxLength(50)]
    public string Category { get; set; }

    [Required]
    [MaxLength(250)]
    public string Content { get; set; }
}
