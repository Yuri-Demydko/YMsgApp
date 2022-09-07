using System.Net;

namespace YMsgApp.Models.DtoModels.ResponseModels.Wrappers;

public class ResponseWrapper<T> where T:class
{
    public T? ResponseObject { get; set; }
    
    public HttpStatusCode? StatusCode { get; set; }
    
    public string ResponseMessage { get; set; }
    
    public bool IsSuccess { get; set; }
}