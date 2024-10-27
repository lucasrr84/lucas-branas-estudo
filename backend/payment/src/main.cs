using src.infra.controller;
using src.infra.queue;
using src.application.usecase;
using src.infra.http;
using src.infra.database;
using src.infra.fallback;
using src.infra.repository;

namespace src;

public class main
{
    public async void execute()
    {
        IHttpServer httpServer = new ExpressAdapter();
        IDatabaseConnection databaseConnection = new PgPromisseAdapter();
        IPaymentProcessor paymentProcessor = PaymentProcessorFactory.create();
        ITransactionRepository transactionRepository = new TransactionRepositoryORM();
        IQueue queue = new RabbitMQAdapter();
        await queue.connect();
        new QueueController(queue, new ProcessPayment(paymentProcessor, transactionRepository));
        httpServer.listen(3002);
    }
}
