using System.ComponentModel.DataAnnotations;

namespace WebApi.UseCases.v1.CreateUser;

public record CreateUserRequest
{
    [Required]
    [MaxLength(250)]
    public string Name { get; init; }

    [Required]
    [MaxLength(250)]
    public string Email { get; init; }

    [Required]
    public string Age { get; init; }
}
