using System;
using System.Threading.Tasks;

namespace src.infra.queue;

public interface IQueue
{
    Task connect();
    Task publish(string exchange, object data);
    Task consume(string queue, Func<object, Task> callback);

}
