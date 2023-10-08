using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthenticationService.DataAccess.Abstract;
using AuthenticationService.Models.Concrete;
using AuthenticationService.Models.DTOs;
using AuthenticationService.Services.Abstract;
using AuthenticationService.Utilities.Results;
using AuthenticationService.Utilities.Security.Hashing;
using Microsoft.IdentityModel.Tokens;
using IResult = AuthenticationService.Utilities.Results.IResult;



namespace AuthenticationService.Services.Concrete;

public class AuthManager : IAuthService
{
    private IUserDal _userDal;
    private readonly string key;
    
    public AuthManager(IUserDal userDal, IConfiguration configuration)
    {
        _userDal = userDal;
        this.key = configuration.GetSection("JwtKey").ToString();
    }
    
    public async Task<IResult> Signup(SignupDto dto)
    {
        if (string.IsNullOrEmpty(dto.Password) || string.IsNullOrEmpty(dto.Email) || string.IsNullOrEmpty(dto.Username))
            return new ErrorResult("Username, Email, and Password cannot be empty");

        var userExist = await _userDal.GetAsync(p => p.Email == dto.Email || p.Username == dto.Username);
        if (userExist != null)
        {
            return new ErrorResult("The user already exist");
        }
        
        byte[] passwordHash, passwordSalt;
        HashingHelper.CreatePasswordHash(dto.Password, out passwordHash, out passwordSalt);
        var user = new User
        {
            Email = dto.Email,
            Username = dto.Username,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
        };

        var addedUser = await _userDal.AddAsync(user);
        return new SuccessResult("User is added successfully");
    }

    public async Task<IDataResult<string>> LoginByEmail(LoginByEmailDto dto)
    {   
        if(string.IsNullOrEmpty(dto.Email) || string.IsNullOrEmpty(dto.Password))
            return new ErrorDataResult<string>("Username, Email, and Password cannot be empty");

        var user = await _userDal.GetAsync(x => x.Email == dto.Email);
        if (user == null)
        {
            return new ErrorDataResult<string>("User cannot be found");
        }
        
        if (!HashingHelper.VerifyPasswordHash(dto.Password, user.PasswordHash, user.PasswordSalt))
        {
            return new ErrorDataResult<string>("Email or Password is wrong");
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.ASCII.GetBytes(key);
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Email, dto.Email),
                new Claim("Username", user.Username.ToString()),
                new Claim("Point", user.Point.ToString()),
            }),
            
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature)

        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var data = tokenHandler.WriteToken(token);
        return new SuccessDataResult<string>("token", data);
    }

}