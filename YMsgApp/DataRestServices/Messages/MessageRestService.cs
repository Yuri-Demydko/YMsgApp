using System.Text.Json;
using YMsgApp.CacheServices;
using YMsgApp.Enums;
using YMsgApp.Helpers.HelperModels;
using YMsgApp.Models.DtoModels.RequestModels;
using YMsgApp.Models.DtoModels.ResponseModels.Wrappers;
using YMsgApp.Models.Entities;

namespace YMsgApp.DataRestServices.Messages;

public class MessageRestService:BaseRestService,IMessageRestService
{
    public MessageRestService(TokenSQLiteCacheService tokenCache, JsonSerializerOptions jsonSerializerOptions = null) : base("api",tokenCache , jsonSerializerOptions)
    {
    }
    
    public async Task<ResponseWrapper<ODataObjectWrapper<Message>>> GetAsync()
    {
        var oDataQueryParams = new ODataQueryParams()
            .AddExpand("UserTo(select=id,username,displayName),UserFrom(select=id,username,displayName)")
            .Compile();
        return await WrapRequestMethod<ODataObjectWrapper<Message>>("Messages", RequestType.Get,oDataQueryParams);
    }
}