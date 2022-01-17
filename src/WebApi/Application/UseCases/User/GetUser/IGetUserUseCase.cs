using System.Threading.Tasks;

namespace Application.UseCases.User.GetUser
{
    public interface IGetUserUseCase
    {
        Task Execute(GetUserInput input);

        void SetOutputPort(IOutputPort outputPort);
    }
}
