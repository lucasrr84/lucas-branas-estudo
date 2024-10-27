using System;
using System.Text;
using RabbitMQ.Client;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;

namespace src.infra.queue;

public class RabbitMQAdapter : IQueue
{
    IConnection connection;

    public async Task connect()
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        this.connection = factory.CreateConnection();
        await Task.CompletedTask;
    }

    public async Task publish(string exchange, object data)
    {
        using (var channel = this.connection.CreateModel())
        {
            //Definindo a troca
            channel.ExchangeDeclare("rideCompleted", "direct", durable: true);

            //Definindo as filas
            channel.QueueDeclare("rideCompleted.processPayment", durable: true, exclusive: false, arguments: null);
            channel.QueueDeclare("rideCompleted.generateInvoice", durable: true, exclusive: false, arguments: null);
            channel.QueueDeclare("rideCompleted.sendReceipt", durable: true, exclusive: false, arguments: null);

            //Vinculando as filas à troca
            channel.QueueBind("rideCompleted.processPayment", "rideCompleted", "");
            channel.QueueBind("rideCompleted.generateInvoice", "rideCompleted", "");
            channel.QueueBind("rideCompleted.sendReceipt", "rideCompleted", "");

            //Publicando a mensagem
            var body = Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(data));
            channel.BasicPublish(exchange: exchange, routingKey: "", basicProperties: null, body: body);
        }
    }

    public async Task consume(string queue, Func<object, Task> callback)
    {
        using (var channel = this.connection.CreateModel())
        {
            // Declarando a troca
            channel.ExchangeDeclare("rideCompleted", "direct", durable: true);

            // Declarando as filas
            channel.QueueDeclare("rideCompleted.processPayment", durable: true, exclusive: false, arguments: null);
            channel.QueueDeclare("rideCompleted.generateInvoice", durable: true, exclusive: false, arguments: null);
            channel.QueueDeclare("rideCompleted.sendReceipt", durable: true, exclusive: false, arguments: null);

            // Vinculando as filas à troca
            channel.QueueBind("rideCompleted.processPayment", "rideCompleted", "");
            channel.QueueBind("rideCompleted.generateInvoice", "rideCompleted", "");
            channel.QueueBind("rideCompleted.sendReceipt", "rideCompleted", "");

            // Configurando o consumidor
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var input = Newtonsoft.Json.JsonConvert.DeserializeObject<object>(message);

                await callback(input);

                // Confirmando o recebimento da mensagem
                channel.BasicAck(ea.DeliveryTag, false);
            };

            // Iniciando o consumo
            channel.BasicConsume(queue: queue, autoAck: false, consumer: consumer);
        }
    }    
}
