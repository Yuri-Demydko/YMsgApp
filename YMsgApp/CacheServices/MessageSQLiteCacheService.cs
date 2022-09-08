using Microsoft.EntityFrameworkCore;
using YMsgApp.Models.Caching;
using YMsgApp.Models.Caching.CacheModels;
using Z.EntityFramework.Plus;

namespace YMsgApp.CacheServices;

public class MessageSQLiteCacheService:BaseSQLiteCacheService<MessageCache>
{
    public MessageSQLiteCacheService(CacheDbContext cacheDb) : base(cacheDb)
    {
    }

    public override async Task<List<MessageCache>> GetAsync()
    {
        return await _cacheDb.MessageCaches.ToListAsync();
    }

    public override async Task<MessageCache> GetAsync(int key)
    {
        return await _cacheDb.MessageCaches.FirstOrDefaultAsync(r => r.Id == key);
    }


    public override async Task SetAsync()
    {
        throw new NotImplementedException();
    }

    public override async Task SetAsync(MessageCache obj)
    {
        await _cacheDb.MessageCaches.AddAsync(obj);
        await _cacheDb.SaveChangesAsync();
    }

    public override async Task RemoveAsync()
    {
        await _cacheDb.MessageCaches.ForEachAsync(r=> _cacheDb.MessageCaches.Remove(r));

        await _cacheDb.SaveChangesAsync();
    }

    public override async Task RemoveAsync(int key)
    {
        var item = await _cacheDb.MessageCaches.FirstOrDefaultAsync(r => r.Id == key);

        if (item != null)
        {
            _cacheDb.MessageCaches.Remove(item);
            await _cacheDb.SaveChangesAsync();
        }
    }
}