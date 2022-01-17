using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Application.UseCases.User.GetUser;
using Domain.Models;

namespace WebApi.UseCases.v1.GetUser;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class UsersController : ControllerBase, IOutputPort
{
    private IActionResult? _viewModel;

    private IGetUserUseCase useCase;
    public UsersController(IGetUserUseCase getUser)
    {
        useCase = getUser;
    }

    void IOutputPort.Invalid()
    {
        _viewModel = this.BadRequest();
    }

    void IOutputPort.Ok(UserModel user)
    {
        _viewModel = this.Ok(new GetUserResponse());
    }

    void IOutputPort.NotFound()
    {
        _viewModel = this.NotFound();
    }

    [HttpGet]
    public async Task<IActionResult> Get(int userId)
    {
        var input = new GetUserInput
        {
            UserId = userId
        };

        useCase.SetOutputPort(this);

        await useCase.Execute(input);

        return _viewModel;
    }
}
