using System.Text.Json;

namespace YMsgApp.DataServices;

public abstract class BaseRestService
{
    protected readonly string _baseAddress;
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
}