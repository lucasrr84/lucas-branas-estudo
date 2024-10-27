using src.application.usecase;
using src.infra.queue;

namespace src.infra.controller;

public class QueueController
{
    public QueueController(IQueue queue, ProcessPayment processPayment)
    {
        queue.consume("rideCompleted.processPayment", async (object input) => {
            await processPayment.execute((InputProcessPayment)input);
        });
    }
}