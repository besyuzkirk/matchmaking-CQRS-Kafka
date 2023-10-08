using AuthenticationService.Models.DTOs;
using AuthenticationService.Utilities.Results;
using IResult = AuthenticationService.Utilities.Results.IResult;


namespace AuthenticationService.Services.Abstract;

public interface IAuthService
{
    Task<IResult> Signup(SignupDto dto);
    Task<IDataResult<string>> LoginByEmail(LoginByEmailDto dto);
}