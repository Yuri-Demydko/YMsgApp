using System.ComponentModel.DataAnnotations;

namespace YMsgApp.Models.Caching.CacheModels;

public class UserCache
{
    [Key]
    public int Id { get; set; }
    
    public string DisplayName { get; set; }
    
    public string UserName { get; set; }
    
    public string ExternalId { get; set; }
    
    public bool Own { get; set; }
}