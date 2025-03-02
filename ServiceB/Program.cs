
using EventBus;
using RabbitMQ.Client;

namespace ServiceB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
           
            builder.Services.AddSingleton<IConnectionFactory>(new ConnectionFactory { HostName = "localhost" });
            builder.Services.AddSingleton<IRabbitMQEventBus, RabbitMQEventBus>();

            builder.Services.AddLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();  
            });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddHostedService<RabbitMQConsumer>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
