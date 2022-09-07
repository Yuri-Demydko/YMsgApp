using YMsgApp.Models.DtoModels.RequestModels;
using YMsgApp.Models.DtoModels.ResponseModels;
using YMsgApp.Models.DtoModels.ResponseModels.Wrappers;
using YMsgApp.Models.Entities;

namespace YMsgApp.DataServices.Auth;

public interface IAuthRestService
{
    public Task<ResponseWrapper<TokenResponse>> LoginAsync(LoginRequest request);

    public Task<ResponseWrapper<User>> RegisterAsync(RegisterRequest request);

    public Task<ResponseWrapper<TokenResponse>> RefreshTokenAsync(RefreshTokenRequest request);

    public Task<EmptyResponseWrapper> RevokeAsync(string username);
}