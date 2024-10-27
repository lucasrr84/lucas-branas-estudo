using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ride.src.infra.queue;

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
        createChannel();
        
        //Definindo a troca
        _channel.ExchangeDeclare("rideCompleted", "direct", durable: true);

        //Definindo as filas
        _channel.QueueDeclare("rideCompleted.generateInvoice", durable: true, exclusive: false, arguments: null);
        //_channel.QueueDeclare("rideCompleted.processPayment", durable: true, exclusive: false, arguments: null);
        //_channel.QueueDeclare("rideCompleted.sendReceipt", durable: true, exclusive: false, arguments: null);

        //Vinculando as filas à troca
        _channel.QueueBind("rideCompleted.generateInvoice", "rideCompleted", "");
        //_channel.QueueBind("rideCompleted.processPayment", "rideCompleted", "");
        //_channel.QueueBind("rideCompleted.sendReceipt", "rideCompleted", "");

        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(data));
        _channel.BasicPublish(exchange: exchange, routingKey: "", body: body);

        //Console.WriteLine($"Publicando mensagem: {data}");
        //Console.WriteLine($"Publicando mensagem serializada: {JsonSerializer.Serialize(data)}");
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

            var data = JsonSerializer.Deserialize<T>(message); // Ajuste para seu tipo específico
            await callback(data); // Processa a mensagem

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
}
