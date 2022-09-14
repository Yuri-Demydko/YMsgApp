using System.ComponentModel.DataAnnotations;

namespace YMsgApp.Models.Caching.CacheModels.MessageCache;

public class MessageCache
{
    [Key]
    public int Id { get; set; }

    public string ExternalId { get; set; }

    public string CreatedAt { get; set; }

    public string Text { get; set; }
    
    public string ShortenedText { get; set; }

    public string UserFromExternalId { get; set; }
    
    public string UserFromUsername { get; set; }

    public string? UserFromDisplayName { get; set; }
    
    public string UserToExternalId { get; set; }
    
    public string UserToUsername { get; set; }

    public string? UserToDisplayName { get; set; }

    // private User _userFrom;
    // private string _userFromId;
    // private User _userTo;
    // private string _userToId;
    // private string _text;
    // private DateTime _createdAt;
}