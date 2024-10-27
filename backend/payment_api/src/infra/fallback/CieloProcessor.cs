using System;
using src.infra.gateway;

namespace src.infra.fallback;

public class CieloProcessor : IPaymentProcessor
{
    private readonly IPaymentProcessor _next;
    
    public CieloProcessor()
    {
        
    }
    
    public CieloProcessor(IPaymentProcessor next)
    {
        
    }

    public async Task<OutputPaymentGateway> processPayment(InputPaymentGateway input)
    {
        try
        {
            var cieloGateway = new CieloGateway();
            var output = await cieloGateway.createTransaction(input);
            return output;

        }
        catch (Exception e)
        {
            if (_next == null) throw new Exception("Out of processors");
            return await _next.processPayment(input);
        }
    }

    public async Task<IPaymentProcessor> next()
    {
        throw new NotImplementedException();
    }

}
