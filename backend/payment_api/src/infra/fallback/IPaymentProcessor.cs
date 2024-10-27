using src.infra.gateway;

namespace src.infra.fallback;

public interface IPaymentProcessor
{
    Task<OutputPaymentGateway> processPayment(InputPaymentGateway input);
}