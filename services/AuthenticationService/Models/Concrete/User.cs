using AuthenticationService.DataAccess.Repository;
using MongoDB.Bson.Serialization.Attributes;

namespace AuthenticationService.Models.Concrete;

public class User : MongoDbEntity
{
    [BsonElement("Email")]
    public string Email { get; set; }
    public string Username { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[]  PasswordSalt { get; set; }
    public int Level { get; set; }
    public long TotalGames { get; set; }
    public long WonGames { get; set; }
    public long LoseGames { get; set; }
}