using System;

namespace src.infra.gateway;

public class CieloGateway : IPaymentGateway
{
    public async Task<OutputPaymentGateway> createTransaction(InputPaymentGateway input)
    {
        throw new NotImplementedException();
    }
}
