namespace YMsgApp.CacheServices;

public interface ISynchronizableCache<T>
{
    public Task SetWithDbUpdateAsync(T obj);

    public Task RemoveWithDbUpdateAsync(int key);
}