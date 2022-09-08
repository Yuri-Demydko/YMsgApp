using Microsoft.Extensions.Configuration;
using YMsgApp.Models.Caching;

namespace YMsgApp.Configurations;

public static class SQLiteCacheConfiguration
{
    public static void ConfigureSQLiteCache(this IServiceCollection services,IConfiguration configuration)
    {

        // services.AddSqlite<CacheDbContext>(configuration.GetConnectionString("Cache"));
        services.AddSqlite<CacheDbContext>("Filename="+Path.Combine(FileSystem.AppDataDirectory, "YMsgAppCache.db3"));
    }
}