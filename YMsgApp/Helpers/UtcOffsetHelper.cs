namespace YMsgApp.Helpers;

public class UtcOffsetHelper
{
    public static TimeSpan UtcOffset => DateTime.UtcNow - DateTime.Now;
}