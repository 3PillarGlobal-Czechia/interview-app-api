
using System.Threading.Tasks;
using Application.Repositories;
using Domain.Models;

namespace Application.UseCases.User.CreateUser;

public class EnsureCreatedUserUseCase : IEnsureCreatedUserUseCase
{
    private IOutputPort _outputPort;

    private readonly IUserRepository _userRepository;

    public EnsureCreatedUserUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Execute(EnsureCreatedUserInput input)
    {
        var user = await _userRepository.GetUserByGoogleId(input.GoogleId);

        if(user is null)
        {
            UserModel model = new()
            {
                UserName = input.UserName,
                FirstName = input.FirstName,
                LastName = input.LastName,
                Email = input.Email,
                GoogleId = input.GoogleId,
                ImageURL = input.ImageURL,
            };

            model = await _userRepository.Create(model);

            if (model is null)
            {
                _outputPort.Invalid();
                return;
            }
        }

        _outputPort.Ok();
    }
    public void SetOutputPort(IOutputPort outputPort) => _outputPort = outputPort;
}

