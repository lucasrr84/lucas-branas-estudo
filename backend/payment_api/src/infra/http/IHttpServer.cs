using System;

namespace src.infra.http;

public interface IHttpServer
{
    void register(string method, string url, Func<object, Task> callback);
    void listen(int port);
}
