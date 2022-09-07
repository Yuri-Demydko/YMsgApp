using YMsgApp.Models.DtoModels.ResponseModels;
using YMsgApp.Models.DtoModels.ResponseModels.Wrappers;

namespace YMsgApp.DataServices.Ping;

public interface IPingRestService
{
    public Task<ResponseWrapper<PingResponse>> PingAsync();
}