using System;

namespace ride.src.infra.queue;

public interface IQueue
{
    Task connect();
    Task disconnect();
    Task publish(string exchange, object data);
    Task consume<T>(string queue, Func<object, Task> callback);
}
