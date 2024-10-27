using System.Collections.Concurrent;
using invoice.src.application.usecase;
using invoice.src.infra.queue;
using invoice.src.domain.dto;

namespace invoice.src.domain.service;

public class MessageConsumer : BackgroundService
{
    private readonly IQueue _queue;
    private readonly GenerateInvoice _generateInvoice;
    private ConcurrentQueue<string> _receivedMessages = new ConcurrentQueue<string>();

    public MessageConsumer(IQueue queue, GenerateInvoice generateInvoice)
    {
        _queue = queue;
        _generateInvoice = generateInvoice;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _queue.connect();

        await _queue.consume<GenerateInvoiceInputDto>("rideCompleted.generateInvoice", async (dynamic message) =>
        {
            _receivedMessages.Enqueue(message.ToString());
            await _generateInvoice.execute((GenerateInvoiceInputDto)message);

            await Task.CompletedTask;
        });
    }

    public ConcurrentQueue<string> getMessages()
    {
        return _receivedMessages;
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await _queue.disconnect();
        await base.StopAsync(cancellationToken);
    }
}
