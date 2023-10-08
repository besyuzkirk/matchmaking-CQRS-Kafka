using AuthenticationService.Utilities.DatabaseSettings;

namespace AuthenticationService.Utilities.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMongoDbSettings(this IServiceCollection services,
        IConfiguration configuration)
    {
        return services.Configure<MongoDbSettings>(options =>
        {
            options.ConnectionString = configuration
                .GetSection(nameof(MongoDbSettings) + ":" + MongoDbSettings.ConnectionStringValue).Value;
            options.Database = configuration
                .GetSection(nameof(MongoDbSettings) + ":" + MongoDbSettings.DatabaseValue).Value;
        });
    }
}
