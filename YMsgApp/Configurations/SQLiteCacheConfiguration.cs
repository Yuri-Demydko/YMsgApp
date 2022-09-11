using Microsoft.Extensions.Configuration;
using YMsgApp.DataRestServices.Ping;
using YMsgApp.Models.Caching;

namespace YMsgApp.Configurations;

public static class SQLiteCacheConfiguration
{
    public static void ConfigureSQLiteCache(this IServiceCollection services,IConfiguration configuration)
    {
        var fileName = Path.Combine(FileSystem.CacheDirectory, configuration.GetConnectionString("Cache"));
        fileName = $"{fileName}_v{configuration.GetValue<string>("Database:Version")}.db3";
        services.AddSqlite<CacheDbContext>($"Filename={fileName}");
    }
}