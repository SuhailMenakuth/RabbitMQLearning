using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client.Events;
using System.Threading.Channels;
using Microsoft.Extensions.Logging;

namespace EventBus
{
    public class RabbitMQEventBus : IRabbitMQEventBus
    {
        //private readonly IConnectionFactory _factory;
        //public RabbitMQEventBus()
        //{
        //    _factory = new ConnectionFactory { HostName = "localhost" };
        //}


        //public void Publish(string queue, string message)
        //{
        //    using var connection = _factory.CreateConnection();
        //    using var channel = connection.CreateModel();
        //    Console.WriteLine("Connected to RabbitMQ successfully!");
        //    channel.QueueDeclare(queue, false, false, false, null);
        //    var body = Encoding.UTF8.GetBytes(message);
        //    channel.BasicPublish("", queue, null, body);
        //}

        //public void Subscribe(string queue, Action<string> onMessageReceived)
        //{
        //    var connection = _factory.CreateConnection();
        //    var channel = connection.CreateModel();
        //    channel.QueueDeclare(queue, false, false, false, null);
        //    var consumer = new EventingBasicConsumer(channel);
        //    consumer.Received += (model, ea) =>
        //    {
        //        var body = ea.Body.ToArray();
        //        var message = Encoding.UTF8.GetString(body);
        //        onMessageReceived(message);
        //    };
        //    channel.BasicConsume(queue, true, consumer);
        //}
        private readonly IConnectionFactory _factory;  //declare a connection to rabbitmqserver
        private readonly IConnection _connection; // it represents an anctive connection to rabbitmq brocker
        private readonly IModel _channel;  //
        private readonly ILogger<RabbitMQEventBus> _logger;

        public RabbitMQEventBus(IConnectionFactory factory , ILogger<RabbitMQEventBus> logger)
        {
            _factory = factory;
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
            _logger = logger;
        }

        public void Publish(string queue, string message)
        {
            _channel.QueueDeclare(queue, false, false, false, null);
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish("", queue, null, body);
        }

     
        public void Subscribe(string queue, Action<string> onMessageReceived)
        {
            var connection = _factory.CreateConnection();
            var channel = connection.CreateModel();
            channel.QueueDeclare(queue, false, false, false, null);

            _logger.LogInformation($"Subscribed to queue: {queue}");

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                _logger.LogInformation($"Received from queue: {queue} -> {message}");
                onMessageReceived(message);
            };
            channel.BasicConsume(queue, true, consumer);
        }


    }
}
