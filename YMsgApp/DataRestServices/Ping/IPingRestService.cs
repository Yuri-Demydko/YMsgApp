using YMsgApp.Models.DtoModels.ResponseModels;
using YMsgApp.Models.DtoModels.ResponseModels.Wrappers;
using YMsgApp.Models.Entities;

namespace YMsgApp.DataRestServices.Ping;

public interface IPingRestService
{
    public Task<ResponseWrapper<Models.Entities.Ping>> PingAsync();
}