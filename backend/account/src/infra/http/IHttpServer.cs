
namespace account.src.infra.http;

public interface IHttpServer
{
    void register(string method, string route, Func<object, object, Task<object>> callback);
    void listen(int port);
}
