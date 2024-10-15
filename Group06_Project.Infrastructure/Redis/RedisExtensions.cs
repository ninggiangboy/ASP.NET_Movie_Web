using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Group06_Project.Infrastructure.Redis;

public static class RedisExtensions
{
    public static void AddRedisCache(this IServiceCollection services, IConfiguration configuration)
    {
        var redisConStr = configuration.GetConnectionString("RedisConnection");
        services.AddStackExchangeRedisCache(options => { options.Configuration = redisConStr; });
        var redis = ConnectionMultiplexer.Connect(redisConStr);
        var db = redis.GetDatabase();
        var server = redis.GetServer(redisConStr);
        services.AddSingleton(server);
        services.AddSingleton(db);
    }
}