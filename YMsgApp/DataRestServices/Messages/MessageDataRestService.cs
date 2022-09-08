using System.Text.Json;
using YMsgApp.Enums;
using YMsgApp.Helpers.HelperModels;
using YMsgApp.Models.DtoModels.RequestModels;
using YMsgApp.Models.DtoModels.ResponseModels.Wrappers;
using YMsgApp.Models.Entities;

namespace YMsgApp.DataRestServices.Messages;

public class MessageDataRestService:BaseRestService
{
    public MessageDataRestService(string baseAddress, JsonSerializerOptions jsonSerializerOptions = null) : base(baseAddress, "api", jsonSerializerOptions)
    {
    }
    
    public async Task<ResponseWrapper<List<Message>>> GetAsync()
    {
        var oDataQueryParams = new ODataQueryParams()
            .AddExpand("UserTo(select=id,username,displayName)")
            .AddExpand("UserFrom(select=id,username,displayName)")
            .Compile();
        return await WrapRequestMethod<List<Message>>("Messages", RequestType.Get,oDataQueryParams);
    }
}