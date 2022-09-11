using YMsgApp.Models.DtoModels.ResponseModels.Wrappers;
using YMsgApp.Models.Entities;

namespace YMsgApp.DataRestServices.Profile;

public interface IProfileRestService
{
    public Task<ResponseWrapper<User>> GetProfile();
}