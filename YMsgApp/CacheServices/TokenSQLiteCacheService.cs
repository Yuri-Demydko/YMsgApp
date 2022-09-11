using Microsoft.EntityFrameworkCore;
using YMsgApp.Models.Caching;
using YMsgApp.Models.Caching.CacheModels;

namespace YMsgApp.CacheServices;

public class TokenSQLiteCacheService:BaseSQLiteCacheService<TokenCache>
{
    public TokenSQLiteCacheService(CacheDbContext cacheDb) : base(cacheDb)
    {
    }

    public async Task<TokenCache> GetLastAsync()
    {
        return await _cacheDb.TokenCaches.OrderByDescending(r=>r.RefreshExpiryTime).FirstOrDefaultAsync();
    }
    

    public override async Task<List<TokenCache>> GetAsync()
    {
        return await _cacheDb.TokenCaches.ToListAsync();
    }

    public override async Task<TokenCache> GetAsync(int key)
    {
        return await _cacheDb.TokenCaches.FirstOrDefaultAsync(r => r.Id == key);
    }
    

    public override async Task SetAsync(TokenCache obj)
    {
        await _cacheDb.TokenCaches.AddAsync(obj);
        await _cacheDb.SaveChangesAsync();
    }

    public override async Task RemoveAsync()
    {
        await _cacheDb.TokenCaches.ForEachAsync(r=> _cacheDb.TokenCaches.Remove(r));

        await _cacheDb.SaveChangesAsync();
    }

    public override async Task RemoveAsync(int key)
    {
        var item = await _cacheDb.TokenCaches.FirstOrDefaultAsync(r => r.Id == key);

        if (item != null)
        {
            _cacheDb.TokenCaches.Remove(item);
            await _cacheDb.SaveChangesAsync();
        }
    }
}