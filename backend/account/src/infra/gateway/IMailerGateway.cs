
namespace account.src.infra.gateway;

public interface IMailerGateway
{
    Task send(string recipient, string subject, string message);
}
