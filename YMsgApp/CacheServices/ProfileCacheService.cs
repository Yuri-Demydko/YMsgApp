using System.Net;
using YMsgApp.DataRestServices.Profile;
using YMsgApp.Models.Caching;
using YMsgApp.Models.Caching.CacheModels;

namespace YMsgApp.CacheServices;

public class ProfileCacheService:BaseSQLiteCacheService<UserCache>, ISynchronizableCache<UserCache>
{
    private readonly ProfileRestService _profileRestService;



    public async Task<UserCache> GetProfile()
    {
        var response = await _profileRestService.GetProfile();
        UserCache item = null;
        if (response.IsSuccess)
        {
            var oldProfileCaches = _cacheDb.UserCaches.Where(r => r.Own);
            
            _cacheDb.UserCaches.RemoveRange(oldProfileCaches);
            item = new UserCache()
            {
                UserName = response!.ResponseObject!.UserName,
                DisplayName = response!.ResponseObject!.DisplayName,
                Own = true,
                ExternalId = response!.ResponseObject.Id
            };
            await _cacheDb.UserCaches.AddAsync(item);
            await _cacheDb.SaveChangesAsync();
        }

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            return item;
        }

        return _cacheDb.UserCaches.FirstOrDefault(r => r.Own);
    }

    public ProfileCacheService(CacheDbContext cacheDb, ProfileRestService profileRestService) : base(cacheDb)
    {
        _profileRestService = profileRestService;
    }

    public override Task<List<UserCache>> GetAsync()
    {
        throw new NotImplementedException();
    }

    public override Task<UserCache> GetAsync(int key)
    {
        throw new NotImplementedException();
    }

    public override Task SetAsync(UserCache obj)
    {
        throw new NotImplementedException();
    }

    public override Task RemoveAsync()
    {
        throw new NotImplementedException();
    }

    public override Task RemoveAsync(int key)
    {
        throw new NotImplementedException();
    }

    public Task SetWithDbUpdateAsync(UserCache obj)
    {
        throw new NotImplementedException();
    }

    public Task RemoveWithDbUpdateAsync(int key)
    {
        throw new NotImplementedException();
    }
}