using System.Text.Json;
using YMsgApp.CacheServices;
using YMsgApp.Enums;
using YMsgApp.Models.DtoModels.ResponseModels.Wrappers;
using YMsgApp.Models.Entities;

namespace YMsgApp.DataRestServices.Profile;

public class ProfileRestService:BaseRestService,IProfileRestService
{
    public ProfileRestService(TokenSQLiteCacheService tokenSqLiteCache, JsonSerializerOptions jsonSerializerOptions = null) : base("api", tokenSqLiteCache, jsonSerializerOptions)
    {
    }

    public async Task<ResponseWrapper<User>> GetProfile()
    {
        return await WrapRequestMethod<User>("Profile", RequestType.Get);
    }
}