using System.Text.Json.Serialization;

namespace YMsgApp.Models.DtoModels.ResponseModels;

public class PingResponse
{
    public DateTime CurrentServerDateTime { get; set; }
    
    public string Message { get; set; }
}