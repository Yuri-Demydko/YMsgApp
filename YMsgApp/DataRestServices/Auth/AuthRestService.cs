using System.Text.Json;
using YMsgApp.Enums;
using YMsgApp.Models.DtoModels.RequestModels;
using YMsgApp.Models.DtoModels.ResponseModels;
using YMsgApp.Models.DtoModels.ResponseModels.Wrappers;
using YMsgApp.Models.Entities;

namespace YMsgApp.DataRestServices.Auth;

public class AuthRestService:BaseRestService, IAuthRestService
{
    public AuthRestService(string baseAddress, JsonSerializerOptions jsonSerializerOptions = null) : base(baseAddress, "auth", jsonSerializerOptions)
    {
    }

    public async Task<ResponseWrapper<Token>> LoginAsync(LoginRequest request)
    {
        return await WrapRequestMethod<Token, LoginRequest>(request, "login", RequestType.Post);
    }

    public async Task<ResponseWrapper<User>> RegisterAsync(RegisterRequest request)
    {
        return await WrapRequestMethod<User, RegisterRequest>(request, "register", RequestType.Post);
    }

    public async Task<ResponseWrapper<Token>> RefreshTokenAsync(RefreshTokenRequest request)
    {
        return await WrapRequestMethod<Token, RefreshTokenRequest>(request, "refresh", RequestType.Post);
    }

    public async Task<NoContentResponseWrapper> RevokeAsync(string username)
    {
        return await WrapRequestMethod("revoke", RequestType.Post, (nameof(username), username));
    }
}