using System;

namespace Application.UseCases.User.GetUser
{
    public record GetUserInput
    {
        public int UserId { get; init; }
    }
}