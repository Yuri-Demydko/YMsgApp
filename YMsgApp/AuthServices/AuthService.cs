using System.Net;
using YMsgApp.CacheServices;
using YMsgApp.DataRestServices.Auth;
using YMsgApp.Enums;
using YMsgApp.Models.Caching.CacheModels;
using YMsgApp.Models.DtoModels.RequestModels;

namespace YMsgApp.AuthServices;

public class AuthService
{
    private readonly AuthRestService _authRestService;
    private readonly TokenSQLiteCacheService _tokenCache;

    public AuthService(AuthRestService authRestService, TokenSQLiteCacheService tokenCache)
    {
        _authRestService = authRestService;
        _tokenCache = tokenCache;
    }

    public async Task<LoginResult> Login(string userName, string pass)
    {
        var response = await _authRestService.LoginAsync(new LoginRequest()
        {
            Username = userName,
            Password = pass
        });

        switch (response.StatusCode)
        {
            case HttpStatusCode.NotFound:
                return LoginResult.NotExist;
                break;
            
            case HttpStatusCode.BadRequest:
                return LoginResult.WrongData;
                break;
            
            case HttpStatusCode.OK:
                await _tokenCache.SetAsync(new TokenCache()
                {
                    AccessToken = response!.ResponseObject!.AccessToken,
                    AccessExpiryTime = response!.ResponseObject!.AccessExpiryTime,
                    RefreshToken = response!.ResponseObject!.RefreshToken,
                    RefreshExpiryTime = response!.ResponseObject!.RefreshExpiryTime
                });
                break;
        }

        return LoginResult.TechnicalError;
    }
}