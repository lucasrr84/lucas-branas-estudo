namespace src.infra.fallback;

public class PaymentProcessorFactory
{
    public static IPaymentProcessor create()
    {
        var cieloProcessor = new CieloProcessor();
        var pjBankProcessor = new PJBankProcessor();

        return pjBankProcessor;
    }
}
