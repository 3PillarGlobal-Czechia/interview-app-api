using Domain.Models;

namespace Application.UseCases.User.GetUser
{
    public class GetUserPresenter : IOutputPort
    {
        public void Invalid()
        {
            throw new System.NotImplementedException();
        }

        public void NotFound()
        {
            throw new System.NotImplementedException();
        }

        public void Ok(UserModel account)
        {
            throw new System.NotImplementedException();
        }
    }
}
