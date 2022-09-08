using YMsgApp.Models.Caching;
using YMsgApp.Models.Caching.CacheModels;

namespace YMsgApp.CacheServices;

public abstract class BaseSQLiteCacheService<T> where T : class
{
    protected readonly CacheDbContext _cacheDb;

    protected BaseSQLiteCacheService(CacheDbContext cacheDb)
    {
        _cacheDb = cacheDb;
    }

    public abstract Task<List<T>> GetAsync();
    
    public abstract Task<T> GetAsync(int key);
    
    public abstract Task SetAsync();
    
    public abstract Task SetAsync(T obj);

    public abstract Task RemoveAsync();

    public abstract Task RemoveAsync(int key);

}