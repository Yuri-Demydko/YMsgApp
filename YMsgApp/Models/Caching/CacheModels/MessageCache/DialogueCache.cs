namespace YMsgApp.Models.Caching.CacheModels.MessageCache;

public class DialogueCache
{
    public List<string> ParticipantsExternalIds { get; set; }
    
    public MessageCache LastMessage { get; set; }

    public string LastMessageText => LastMessage.Text;
}