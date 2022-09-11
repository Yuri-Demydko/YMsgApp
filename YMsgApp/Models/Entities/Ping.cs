namespace YMsgApp.Models.Entities;

public class Ping
{
    public DateTime CurrentServerDateTime { get; set; }
    
    public string CacheDbVersion { get; set; }
    
    public string Message { get; set; }
}