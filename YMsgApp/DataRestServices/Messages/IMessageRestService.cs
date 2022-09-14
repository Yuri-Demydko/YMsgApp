using YMsgApp.Models.DtoModels.ResponseModels.Wrappers;
using YMsgApp.Models.Entities;

namespace YMsgApp.DataRestServices.Messages;

public interface IMessageRestService
{
    public Task<ResponseWrapper<ODataObjectWrapper<Message>>> GetAsync();
}