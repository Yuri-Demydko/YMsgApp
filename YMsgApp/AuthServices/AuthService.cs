using System.Net;
using YMsgApp.Background;
using YMsgApp.CacheServices;
using YMsgApp.DataRestServices.Auth;
using YMsgApp.Enums;
using YMsgApp.Models.Caching.CacheModels;
using YMsgApp.Models.DtoModels.RequestModels;

namespace YMsgApp.AuthServices;

public class AuthService
{
    private readonly IAuthRestService _authRestService;
    private readonly TokenSQLiteCacheService _tokenCache;

    public AuthService(IAuthRestService authRestService, TokenSQLiteCacheService tokenCache)
    {
        _authRestService = authRestService;
        _tokenCache = tokenCache;
    }

    public async Task<LoginResult> LoginAsync(string userName, string pass)
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

            case HttpStatusCode.Unauthorized:
                return LoginResult.WrongData;

            case HttpStatusCode.OK:
                await _tokenCache.RemoveAsync();
                await _tokenCache.SetAsync(new TokenCache()
                {
                    AccessToken = response!.ResponseObject!.AccessToken,
                    AccessExpiryTime = response!.ResponseObject!.AccessExpiryTime,
                    RefreshToken = response!.ResponseObject!.RefreshToken,
                    RefreshExpiryTime = response!.ResponseObject!.RefreshExpiryTime
                });
                
                return LoginResult.Success;
        }

        return LoginResult.ConnectionError;
    }

    public async Task<RegisterResult> RegisterAsync(string userName, string pass, string? displayName)
    {
        var response = await _authRestService.RegisterAsync(new RegisterRequest()
        {
            Username = userName,
            Password = pass,
            DisplayName = displayName
        });
        
        switch (response.StatusCode)
        {
            case HttpStatusCode.BadRequest:
                return RegisterResult.AlreadyExist;

            case HttpStatusCode.OK:
                return RegisterResult.Success;
        }

        return RegisterResult.ConnectionError;
    }

    public async Task<RefreshTokenResult> RefreshTokenAsync(string accessToken, string refreshToken)
    {
        var response = await _authRestService.RefreshTokenAsync(new RefreshTokenRequest()
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        });
        
        switch (response.StatusCode)
        {
            case HttpStatusCode.NotFound:
                return RefreshTokenResult.InvalidToken;

            case HttpStatusCode.BadRequest:
                return RefreshTokenResult.InvalidRequest;

            case HttpStatusCode.OK:
                await _tokenCache.SetAsync(new TokenCache()
                {
                    AccessToken = response!.ResponseObject!.AccessToken,
                    AccessExpiryTime = response!.ResponseObject!.AccessExpiryTime,
                    RefreshToken = response!.ResponseObject!.RefreshToken,
                    RefreshExpiryTime = response!.ResponseObject!.RefreshExpiryTime
                });
                return RefreshTokenResult.Success;
        }

        return RefreshTokenResult.ConnectionError;
    }

    

    public async Task RefreshTokenJob()
    {
        var tokenCaches = await _tokenCache.GetAsync();
        var oldToken = tokenCaches.LastOrDefault();
        
        if(oldToken==null)
        {
            return;
        }

        await RefreshTokenAsync(oldToken.AccessToken, oldToken.RefreshToken);
    }
}