
namespace Application.UseCases.User.CreateUser;

public readonly struct EnsureCreatedUserInput
{
    public string GoogleId { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string UserName { get; init; }
    public string Email { get; init; }
    public string ImageURL { get; init; }
}
