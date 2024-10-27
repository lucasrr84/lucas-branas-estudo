
namespace account.src.infra.gateway;

public class MailerGatewayMemory : IMailerGateway
{
    public Task send(string recipient, string subject, string message)
    {
        Console.WriteLine($"{recipient}, {subject}, {message}");
        return Task.CompletedTask;
    }
}
