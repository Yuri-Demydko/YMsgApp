using System.ComponentModel.DataAnnotations;

namespace YMsgApp.Models.DtoModels.RequestModels;

public class RegisterRequest
{
    public string? Username { get; set; }
    
    public string? Password { get; set; }
    
    public string? DisplayName { get; set; }
}