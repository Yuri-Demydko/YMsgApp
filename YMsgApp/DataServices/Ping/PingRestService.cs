using System.Net;
using System.Text.Json;
using YMsgApp.Constants;
using YMsgApp.Models.DtoModels.ResponseModels;
using YMsgApp.Models.DtoModels.ResponseModels.Wrappers;

namespace YMsgApp.DataServices.Ping;

public class PingRestService:BaseRestService,IPingRestService
{
    public PingRestService(string baseAddress, JsonSerializerOptions jsonSerializerOptions = null) : base(baseAddress,"", jsonSerializerOptions)
    {
    }
    public async Task<ResponseWrapper<PingResponse>> PingAsync()
    {
        if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
        {
            return new ResponseWrapper<PingResponse>()
            {
                ResponseMessage = Constants.ResponseMessages.NoInternetAccess,
                StatusCode = null,
                ResponseObject = null,
                IsSuccess = false
            };
        }

        try
        {
            var response = await _httpClient.GetAsync($"{_apiAddress}/ping");

            if (response.IsSuccessStatusCode)
            {
                var stringContent = await response.Content.ReadAsStringAsync();

                var pingResponse = JsonSerializer.Deserialize<PingResponse>(stringContent, _jsonSerializerOptions);

                return new ResponseWrapper<PingResponse>()
                {
                    IsSuccess = true,
                    ResponseObject = pingResponse,
                    StatusCode = response.StatusCode
                };
            }
            else
            {
                return new ResponseWrapper<PingResponse>()
                {
                    ResponseMessage = response.ReasonPhrase,
                    StatusCode = response.StatusCode,
                    ResponseObject = null,
                    IsSuccess = false
                };
            }
        }
        catch (WebException e)
        {
            return new ResponseWrapper<PingResponse>()
            {
                ResponseMessage = e.Message,
                StatusCode = HttpStatusCode.ServiceUnavailable,
                ResponseObject = null,
                IsSuccess = false
            };
        }
        catch (Exception e)
        {
            return new ResponseWrapper<PingResponse>()
            {
                ResponseMessage = Constants.ResponseMessages.ClientSideError+$": {e.Message}",
                StatusCode = null,
                ResponseObject = null,
                IsSuccess = false
            };
        }
    }

    
}