namespace WebApi.UseCases.v1.CreateUser;

public record CreateUserResponse
{
    public string Name { get; init; }

    public string Email { get; init; }

    public string Age { get; init; }
}
