using Application.Repositories;
using System;
using System.Threading.Tasks;

namespace Application.UseCases.User.GetUser
{
    public class GetUserUseCase : IGetUserUseCase
    {
        private IOutputPort _outputPort;

        private IUserRepository _userRepository;

        public GetUserUseCase(IUserRepository userRepository)
        {
            _outputPort = new GetUserPresenter();
            _userRepository = userRepository;
        }

        public async Task Execute(GetUserInput input)
        {
            await GetUserInternal(input.UserId);
        }

        public void SetOutputPort(IOutputPort outputPort) => this._outputPort = outputPort;

        private async Task GetUserInternal(int userId)
        {
            var user = await _userRepository.GetById(userId);

            _outputPort.Ok(user);
        }
    }
}
