using MassTransit;
using Microsoft.Extensions.Hosting;
using Shared;
using System.Text.Json;

namespace Worker
{
    public class Publisher : BackgroundService
    {
        readonly IBus _bus;

        public Publisher(IBus bus)
        {
            _bus = bus;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var helloMessage = new HelloMessage()
            {
                Body = "teste",
                EmailAddress = "teste@teste.com",
            };

            var notification = new ScheduleNotification()
            {
                HelloMessage = helloMessage,
                DeliveryTime = DateTime.Now.AddSeconds(60)
            };

            var message = JsonSerializer.Serialize(notification);

            Console.WriteLine($"Worker - Enviando mensagem: {message}");

            await _bus.Publish(notification);
        }
    }
}
