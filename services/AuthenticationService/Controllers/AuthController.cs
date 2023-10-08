using AuthenticationService.Models.DTOs;
using AuthenticationService.Services.Abstract;
using AuthenticationService.Utilities.Results;
using Microsoft.AspNetCore.Mvc;
using IResult = AuthenticationService.Utilities.Results.IResult;


namespace AuthenticationService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    [HttpPost("Signup")]
    public async Task<IResult> Signup([FromBody] SignupDto dto)
    {
        var result =  await _authService.Signup(dto);
        return result;
    }

    [HttpPost("Login")]
    public async Task<IDataResult<string>> Login([FromBody] LoginByEmailDto dto)
    {
        var result = await _authService.LoginByEmail(dto);
        return result;
    }
}
