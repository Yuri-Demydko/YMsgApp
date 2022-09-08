using YMsgApp.Models.DtoModels.RequestModels;
using YMsgApp.Models.DtoModels.ResponseModels;
using YMsgApp.Models.DtoModels.ResponseModels.Wrappers;
using YMsgApp.Models.Entities;

namespace YMsgApp.DataRestServices.Auth;

public interface IAuthRestService
{
    public Task<ResponseWrapper<Token>> LoginAsync(LoginRequest request);

    public Task<ResponseWrapper<User>> RegisterAsync(RegisterRequest request);

    public Task<ResponseWrapper<Token>> RefreshTokenAsync(RefreshTokenRequest request);

    public Task<NoContentResponseWrapper> RevokeAsync(string username);
}