using System.ComponentModel.DataAnnotations;

namespace YMsgApp.Models.DtoModels.RequestModels;

public class LoginRequest
{
    public string? Username { get; set; }
    
    public string? Password { get; set; }
}