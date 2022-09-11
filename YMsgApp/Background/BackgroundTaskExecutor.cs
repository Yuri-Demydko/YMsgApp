using System.Diagnostics;
using YMsgApp.AuthServices;

namespace YMsgApp.Background;

public static class BackgroundTaskExecutor
{
    private static readonly Dictionary<Guid, Thread> Threads = new();

    private static IServiceProvider _serviceProvider;

    public static void ConfigureServiceProvider(IServiceProvider provider)
    {
        _serviceProvider = provider;
    }

    public static void KillJob(Guid id)
    {
        if (!Threads.ContainsKey(id)) return;
        Threads[id].Interrupt();
        Threads.Remove(id);
    }

    public static void KillAllJobs()
    {
        Threads.Values.ToList().ForEach(t=>t.Interrupt());
        Threads.Clear();
    }
    

    public static Guid Execute<TService>(string method, TimeSpan? retryRate, params object?[] parameters)
    {
        void Ts()
        {
            var retry = retryRate.HasValue;
            do
            {
                var auth = _serviceProvider.GetService<TService>();
                var methodInfo = typeof(TService).GetMethod(method);
                methodInfo?.Invoke(auth, parameters);
                if (retryRate.HasValue)
                {
                    Thread.Sleep(retryRate.Value);
                }
            } while (retry);
        }

        var workerThread = new Thread(Ts);
        var workId = Guid.NewGuid();
        Threads.Add(workId,workerThread);
        workerThread.Start();
        return workId;
    }
    
}