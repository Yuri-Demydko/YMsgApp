using System.Text.Json;
using YMsgApp.Enums;
using YMsgApp.Models.DtoModels.ResponseModels;
using YMsgApp.Models.DtoModels.ResponseModels.Wrappers;
using YMsgApp.Models.Entities;

namespace YMsgApp.DataRestServices.Ping;

public class PingRestService:BaseRestService,IPingRestService
{
    public PingRestService(string baseAddress, JsonSerializerOptions jsonSerializerOptions = null) : base(baseAddress,"", jsonSerializerOptions)
    {
    }
    public async Task<ResponseWrapper<Models.Entities.Ping>> PingAsync()
    {
        return await WrapRequestMethod<Models.Entities.Ping>("ping", RequestType.Get);
    }

    
}