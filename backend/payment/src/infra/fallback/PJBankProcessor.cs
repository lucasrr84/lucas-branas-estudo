using src.infra.gateway;

namespace src.infra.fallback;

public class PJBankProcessor : IPaymentProcessor
{
    private readonly IPaymentProcessor _next;

    public PJBankProcessor()
    {
        
    }

    public PJBankProcessor(IPaymentProcessor next)
    {
        
    }

    public async Task<OutputPaymentGateway> processPayment(InputPaymentGateway input)
    {
        throw new NotImplementedException();
    }
}
