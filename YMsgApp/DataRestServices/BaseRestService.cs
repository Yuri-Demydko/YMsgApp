using System.Net.Http.Json;
using System.Text.Json;
using YMsgApp.Enums;
using YMsgApp.Models.DtoModels.ResponseModels.Wrappers;

namespace YMsgApp.DataRestServices;

public abstract class BaseRestService
{
    private readonly string _baseAddress;
    protected readonly HttpClient _httpClient;
    protected readonly string _apiAddress;
    protected readonly JsonSerializerOptions _jsonSerializerOptions;

    protected BaseRestService(string baseAddress, string apiRouteBase, JsonSerializerOptions jsonSerializerOptions=null)
    {
        _baseAddress = baseAddress;
        _jsonSerializerOptions = jsonSerializerOptions??new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        _httpClient = new HttpClient();
        _apiAddress = $"{baseAddress}/{apiRouteBase}";
    }

    protected async Task<NoContentResponseWrapper> WrapRequestMethod(string endpointName,
        RequestType requestType, params (string,string)[] requestParams)
    {
        var res = await WrapRequestMethod<object, object>(null, endpointName, requestType, requestParams);

        return new NoContentResponseWrapper()
        {
            IsSuccess = res.IsSuccess,
            ResponseMessage = res.ResponseMessage,
            StatusCode = res.StatusCode
        };
    }
    
    protected async Task<NoContentResponseWrapper> WrapRequestMethod<TM>(TM requestBodyModel, string endpointName,
        RequestType requestType, params (string,string)[] requestParams) where TM : class
    {
        var res = await WrapRequestMethod<object, TM>(requestBodyModel, endpointName, requestType, requestParams);

        return new NoContentResponseWrapper()
        {
            IsSuccess = res.IsSuccess,
            ResponseMessage = res.ResponseMessage,
            StatusCode = res.StatusCode
        };
    }
    
    protected async Task<ResponseWrapper<T>> WrapRequestMethod<T>(string endpointName,
        RequestType requestType, params (string,string)[] requestParams) where T : class
    {
        return await WrapRequestMethod<T, object>(new object(), endpointName, requestType, requestParams);
    }

    protected async Task<ResponseWrapper<T>> WrapRequestMethod<T,TM>(TM requestBodyModel, string endpointName, RequestType requestType, params (string,string)[] requestParams) where T : class where TM : class
    {
        if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
        {
            return new ResponseWrapper<T>()
            {
                ResponseMessage = Constants.ResponseMessages.NoInternetAccess,
                StatusCode = null,
                ResponseObject = null,
                IsSuccess = false
            };
        }

        try
        {
            HttpResponseMessage responseMessage;
            var requestParamsString = string.Empty;
            if (requestParams.Any())
            {
                requestParamsString = $"?{string.Join('&', requestParams.Select(r => $"{r.Item1}={r.Item2}"))}";
            }

            var requestString = $"{_apiAddress}/{endpointName}{requestParamsString}";
            switch (requestType)
            {
                case RequestType.Get:
                    responseMessage = await _httpClient.GetAsync(requestString);
                    break;
                case RequestType.Post:
                    responseMessage = await _httpClient.PostAsJsonAsync(requestString, requestBodyModel);
                    break;
                case RequestType.Put:

                    responseMessage = await _httpClient.PutAsJsonAsync(requestString, requestBodyModel);
                    break;
                case RequestType.Patch:
                    var requestBodyContent = new StringContent(JsonSerializer.Serialize(requestBodyModel));
                    responseMessage = await _httpClient.PatchAsync(requestString, requestBodyContent);
                    break;
                case RequestType.Delete:
                    responseMessage = await _httpClient.DeleteAsync(requestString);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(requestType), requestType, null);
            }

            if (responseMessage.IsSuccessStatusCode)
            {
                var stringContent = await responseMessage.Content.ReadAsStringAsync();

                var responseObject = JsonSerializer.Deserialize<T>(stringContent, _jsonSerializerOptions);

                return new ResponseWrapper<T>()
                {
                    IsSuccess = true,
                    ResponseObject = responseObject,
                    StatusCode = responseMessage.StatusCode
                };
            }
            else
            {
                return new ResponseWrapper<T>()
                {
                    ResponseMessage = responseMessage.ReasonPhrase,
                    StatusCode = responseMessage.StatusCode,
                    ResponseObject = null,
                    IsSuccess = false
                };
            }
        }
        catch (System.Net.WebException e)
        {
            return new ResponseWrapper<T>()
            {
                ResponseMessage = e.Message,
                StatusCode = null,
                ResponseObject = null,
                IsSuccess = false
            };
        }
        catch (Exception e)
        {
            return new ResponseWrapper<T>()
            {
                ResponseMessage = Constants.ResponseMessages.ClientSideError,
                StatusCode = null,
                ResponseObject = null,
                IsSuccess = false
            };
        }
    }
    
}