
using System.Threading.Tasks;

namespace Application.UseCases.User.CreateUser;

public interface IEnsureCreatedUserUseCase
{

    Task Execute(EnsureCreatedUserInput input);

    void SetOutputPort(IOutputPort outputPort);
}

