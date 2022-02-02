﻿using System.ComponentModel.DataAnnotations;

namespace WebApi.UseCases.v1.UpdateInterviewQuestion;

public record UpdateInterviewQuestionRequest
{
    [Required]
    public int Id { get; set; }

    [MaxLength(100)]
    public string Title { get; set; }
    
    [Range(1, 5)]
    public int? Difficulty { get; set; }

    [MaxLength(50)]
    public string Category { get; set; }

    [MaxLength(250)]
    public string Content { get; set; }
}
