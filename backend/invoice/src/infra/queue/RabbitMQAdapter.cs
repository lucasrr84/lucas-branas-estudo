using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace invoice.src.infra.queue;

public class RabbitMQAdapter : IQueue
{
    private IConnection _connection;
    private IModel _channel;

    public async Task connect()
    {
        createChannel();
        await Task.CompletedTask;
    }

    private void createChannel()
    {
        if (_connection == null || !_connection.IsOpen)
        {
            var factory = new ConnectionFactory() { Uri = new Uri("amqp://localhost") };
            _connection = factory.CreateConnection();
        }

        if (_channel == null || !_channel.IsOpen)
        {
            _channel = _connection.CreateModel();
        }
    }

    public async Task disconnect()
    {
        _channel?.Close();
        _connection?.Close();
        await Task.CompletedTask;
    }

    public async Task publish(string exchange, object data)
    {
        await Task.CompletedTask;
    }

    public async Task consume<T>(string queue, Func<object, Task> callback)
    {
        createChannel();

        _channel.QueueDeclare(queue: queue, durable: true, exclusive: false, arguments: null);

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (model, ea) => 
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            //Console.WriteLine($"Mensagem recebida bruta: {message}");

            var data = JsonSerializer.Deserialize<T>(message);
            await callback(data);

            try
            {
                _channel.BasicAck(ea.DeliveryTag, false);
            }
            catch (ObjectDisposedException)
            {
                Console.WriteLine("Tentativa de reconhecer uma mensagem em um canal já fechado.");
            }
        };

        _channel.BasicConsume(queue: queue, autoAck: false, consumer: consumer);
        await Task.CompletedTask;
    }

/*
    public async Task consume(string queue, Func<object, Task> callback)
    {
        createChannel();

        _channel.QueueDeclare(queue: queue, durable: true, exclusive: false, arguments: null);

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (model, ea) => 
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine($"Mensagem recebida bruta: {message}");

            var data = JsonSerializer.Deserialize<object>(message);
            await callback(data);

            try
            {
                _channel.BasicAck(ea.DeliveryTag, false);
            }
            catch (ObjectDisposedException)
            {
                Console.WriteLine("Tentativa de reconhecer uma mensagem em um canal já fechado.");
            }
        };

        _channel.BasicConsume(queue: queue, autoAck: false, consumer: consumer);
        await Task.CompletedTask;
    }
*/
}
