using src.domain;
using src.infra.fallback;
using src.infra.gateway;
using src.infra.repository;

namespace src.application.usecase;

public class ProcessPayment
{
    private readonly IPaymentProcessor _paymentProcessor;
    private readonly ITransactionRepository _transactionRepository;

    public ProcessPayment(IPaymentProcessor paymentProcessor, ITransactionRepository transactionRepository)
    {
        _paymentProcessor = paymentProcessor;
        _transactionRepository = transactionRepository;
    }


    public async Task execute(InputProcessPayment input)
    {
        Console.WriteLine($"processPayment: {input}");

        var inputTransaction = new InputPaymentGateway();
        inputTransaction.cardHolder = "Cliente Exemplo";
        inputTransaction.creditCardNumber = "4012001037141112";
        inputTransaction.expDate = "05/2027";
        inputTransaction.cvv = "123";
        inputTransaction.amount = input.amount;

        var transaction = Transaction.create(input.rideId, input.amount);

        try
        {
            var outputCreateTransaction = await _paymentProcessor.processPayment(inputTransaction);
            if (outputCreateTransaction.status == "approved")
            {
                transaction.pay();
                _transactionRepository.saveTransaction(transaction);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"error: {e.Message}");
        }
    }
}

public class InputProcessPayment 
{
    public string rideId;
    public decimal amount;

}
