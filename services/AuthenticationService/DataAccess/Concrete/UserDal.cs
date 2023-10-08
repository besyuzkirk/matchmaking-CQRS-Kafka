using AuthenticationService.DataAccess.Abstract;
using AuthenticationService.DataAccess.Repository;
using AuthenticationService.Models.Concrete;
using AuthenticationService.Utilities.DatabaseSettings;
using Microsoft.Extensions.Options;

namespace AuthenticationService.DataAccess.Concrete;

public class UserDal : MongoDbRepositoryBase<User>, IUserDal
{
    public UserDal(IOptions<MongoDbSettings> options) : base(options)
    {
    }
}