using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.UseCases.v1.GetUser;

public record GetUserResponse
{
    [Required]
    public Guid Guid { get; init; }
}
