using EventBus;

namespace ServiceB
{
    //    public class RabbitMQConsumer : BackgroundService
    //    {
    //        private readonly IRabbitMQEventBus _eventBus;
    //        public RabbitMQConsumer(IRabbitMQEventBus eventBus) => _eventBus = eventBus;

    //        protected override Task ExecuteAsync(CancellationToken stoppingToken)
    //        {
    //            _eventBus.Subscribe("helloQueue", message => Console.WriteLine($"Received: {message}"));
    //            return Task.CompletedTask;
    //        }
    //    }


    public class RabbitMQConsumer : BackgroundService
    {
        private readonly IRabbitMQEventBus _eventBus;
        private readonly ILogger<RabbitMQConsumer> _logger;

        public RabbitMQConsumer(IRabbitMQEventBus eventBus, ILogger<RabbitMQConsumer> logger)
        {
            _eventBus = eventBus;
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("RabbitMQ Consumer started...");

            _eventBus.Subscribe("helloQueue", message =>
            {
                _logger.LogInformation($"Received message: {message}");
                Console.WriteLine($"Received: {message}");
            });

            return Task.CompletedTask;
        }
    }


}
