using AutoMapper;
using Microsoft.EntityFrameworkCore;
using YMsgApp.DataRestServices.Messages;
using YMsgApp.Models.Caching;
using YMsgApp.Models.Caching.CacheModels;
using YMsgApp.Models.Caching.CacheModels.MessageCache;
using Z.EntityFramework.Plus;

namespace YMsgApp.CacheServices;

public class MessageSQLiteCacheService : BaseSQLiteCacheService<MessageCache>, ICacheUpdate
{
    private readonly IMessageRestService _messageRestService;
    private readonly IMapper _mapper;

    public MessageSQLiteCacheService(CacheDbContext cacheDb, IMessageRestService messageRestService, IMapper mapper) :
        base(cacheDb)
    {
        _messageRestService = messageRestService;
        _mapper = mapper;
    }

    public override async Task<List<MessageCache>> GetAsync()
    {
        return await _cacheDb.MessageCaches.ToListAsync();
    }

    public override async Task<MessageCache> GetAsync(int key)
    {
        return await _cacheDb.MessageCaches.FirstOrDefaultAsync(r => r.Id == key);
    }

    public IAsyncEnumerable<MessageCache> GetAsAsyncEnumerable()
    {
        return _cacheDb.MessageCaches.AsAsyncEnumerable();
    }

    public async Task SetAsync()
    {
        var response = await _messageRestService.GetAsync();

        if (response.IsSuccess && response.ResponseObject?.Value?.Count>0)
        {
            _cacheDb.MessageCaches.RemoveRange(_cacheDb.MessageCaches);
            
            var items = _mapper.Map<List<MessageCache>>(response.ResponseObject.Value);

            await _cacheDb.MessageCaches.AddRangeAsync(items);

            await _cacheDb.SaveChangesAsync();
        }
    }

    public List<DialogueCache> GetAsDialogues()
    {
       return  _cacheDb.MessageCaches.ToList()
            .GroupBy(r => new
            {
                Key = new string[]{r.UserFromExternalId,r.UserToExternalId}.OrderBy(f=>f)
            })
            .Select(r => new DialogueCache()
            {
                ParticipantsExternalIds = r.Key.Key.ToList(),
                LastMessage=r.OrderByDescending(r=>r.CreatedAt).First()
            }).ToList();
        
    }

    public override async Task SetAsync(MessageCache obj)
    {
        await _cacheDb.MessageCaches.AddAsync(obj);
        await _cacheDb.SaveChangesAsync();
    }

    public override async Task RemoveAsync()
    {
        await _cacheDb.MessageCaches.ForEachAsync(r => _cacheDb.MessageCaches.Remove(r));

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