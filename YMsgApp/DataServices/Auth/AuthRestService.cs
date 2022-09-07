using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using YMsgApp.Models.DtoModels.RequestModels;
using YMsgApp.Models.DtoModels.ResponseModels;
using YMsgApp.Models.DtoModels.ResponseModels.Wrappers;
using YMsgApp.Models.Entities;

namespace YMsgApp.DataServices.Auth;

public class AuthRestService:BaseRestService, IAuthRestService
{
    public AuthRestService(string baseAddress, JsonSerializerOptions jsonSerializerOptions = null) : base(baseAddress, "auth", jsonSerializerOptions)
    {
    }

    public async Task<ResponseWrapper<TokenResponse>> LoginAsync(LoginRequest request)
    {
        if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
        {
            return new ResponseWrapper<TokenResponse>()
            {
                ResponseMessage = Constants.ResponseMessages.NoInternetAccess,
                StatusCode = null,
                ResponseObject = null,
                IsSuccess = false
            };
        }

        try
        {
            var response = await _httpClient.PostAsJsonAsync($"{_apiAddress}/login", request, _jsonSerializerOptions);

            if (response.IsSuccessStatusCode)
            {
                var stringContent = await response.Content.ReadAsStringAsync();

                var tokenResponse = JsonSerializer.Deserialize<TokenResponse>(stringContent, _jsonSerializerOptions);

                return new ResponseWrapper<TokenResponse>()
                {
                    IsSuccess = true,
                    ResponseObject = tokenResponse,
                    StatusCode = response.StatusCode
                };
            }
            else
            {
                return new ResponseWrapper<TokenResponse>()
                {
                    ResponseMessage = response.ReasonPhrase,
                    StatusCode = response.StatusCode,
                    ResponseObject = null,
                    IsSuccess = false
                };
            }
        }
        catch (Exception e)
        {
            return new ResponseWrapper<TokenResponse>()
            {
                ResponseMessage = Constants.ResponseMessages.ClientSideError,
                StatusCode = null,
                ResponseObject = null,
                IsSuccess = false
            };
        }
    }

    public Task<ResponseWrapper<User>> RegisterAsync(RegisterRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseWrapper<TokenResponse>> RefreshTokenAsync(RefreshTokenRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<EmptyResponseWrapper> RevokeAsync(string username)
    {
        throw new NotImplementedException();
    }
}