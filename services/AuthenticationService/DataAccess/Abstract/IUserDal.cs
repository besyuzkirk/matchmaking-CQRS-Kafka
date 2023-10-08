using AuthenticationService.DataAccess.Repository;
using AuthenticationService.Models.Concrete;

namespace AuthenticationService.DataAccess.Abstract;

public interface IUserDal : IRepository<User, string>
{
}