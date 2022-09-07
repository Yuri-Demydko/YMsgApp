namespace YMsgApp.Models.DtoModels.RequestModels;

public class RefreshTokenRequest
{
    public string? AccessToken { get; set; }
    
    public string? RefreshToken { get; set; }
}