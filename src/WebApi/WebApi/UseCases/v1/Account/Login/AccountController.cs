using System.Linq;
using System.Threading.Tasks;
using Application.UseCases.User.CreateUser;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.UseCases.v1.User.Login;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
[ApiController]
public class AccountController : ControllerBase, IOutputPort
{
    private IActionResult _viewModel;

    private readonly IEnsureCreatedUserUseCase _useCase;

    public AccountController(IEnsureCreatedUserUseCase ensureCreatedUser)
    {
        _useCase = ensureCreatedUser;
    }

    void IOutputPort.Invalid()
    {
        _viewModel = BadRequest();
    }

    void IOutputPort.Ok()
    {
        _viewModel = Ok();
    }

    [HttpGet(Name = "Login")]
    public async Task Login()
    {
        await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties()
        {
            RedirectUri = Url.Action("GoogleResponse")
        });
    }

    [HttpGet(Name = "GoogleResponse")]
    public async Task<IActionResult> GoogleResponse()
    {
        var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        var claims = result.Principal.Identities.FirstOrDefault().Claims.Select(claim => new
        {
            claim.Type,
            claim.Value
        }).ToDictionary(claim => claim.Type.Substring(claim.Type.LastIndexOf("/") + 1), claim => claim.Value);

        var input = new EnsureCreatedUserInput
        {
            GoogleId = claims["nameidentifier"],
            UserName = claims["name"],
            FirstName = claims["givenname"],
            LastName = claims["surname"],
            Email = claims["emailaddress"],
            ImageURL = claims["urn:google:pic"],
        };

        _useCase.SetOutputPort(this);

        await _useCase.Execute(input);

        return _viewModel;
    }

}

