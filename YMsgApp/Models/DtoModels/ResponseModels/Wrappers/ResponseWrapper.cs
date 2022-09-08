using System.Net;

namespace YMsgApp.Models.DtoModels.ResponseModels.Wrappers;

public class ResponseWrapper<T> where T:class
{
    public T? ResponseObject { get; init; }
    
    public HttpStatusCode? StatusCode { get; set; }
    
    public string ResponseMessage { get; init; }
    
    public bool IsSuccess { get; init; }
}