using MongoDB.Driver;
using Play.Catalog.Service.Models;

namespace Play.Catalog.Service.Services;

public static class Extensions
{
    public static IServiceCollection AddMongo(this IServiceCollection services)
    {

        services.AddSingleton(serviceProvider =>
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var databaseSettings = configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();

            if (databaseSettings == null)
            {
                throw new Exception("Database settings are not configured.");
            }

            return new MongoClient(databaseSettings.ConnectionString).GetDatabase(databaseSettings.DatabaseName);
        });

        return services;
    }

    public static IServiceCollection AddMongoRepository<T>(this IServiceCollection services, string collectionName)
        where T : IEntity
    {
        services.AddSingleton<IRepository<T>>(serviceProvider =>
        {
            var database = serviceProvider.GetRequiredService<IMongoDatabase>();
            return new MongoRepository<T>(database, collectionName);
        });

        return services;
    }
}