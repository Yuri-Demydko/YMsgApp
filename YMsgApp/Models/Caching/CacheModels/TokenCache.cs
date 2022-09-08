using System.ComponentModel.DataAnnotations;
using YMsgApp.Models.Entities;

namespace YMsgApp.Models.Caching.CacheModels;

public class TokenCache:Token
{
    [Key]
    public int Id;
}