namespace YMsgApp.Models.Entities;

public class Token
{
    public string? AccessToken { get; set; }
    
    public string? RefreshToken { get; set; }
    
    public DateTime AccessExpiryTime { get; set; }
    
    public DateTime RefreshExpiryTime { get; set; }
}