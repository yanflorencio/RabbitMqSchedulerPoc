using MassTransit;
using Shared;
using System.Text.Json;

namespace Consumer.Consumers
{
    public class GettingStartedConsumer :
        IConsumer<HelloMessage>
    {

        public Task Consume(ConsumeContext<HelloMessage> context)
        {
            var message = context.Message;

            var messageJson = JsonSerializer.Serialize(message);

            Console.WriteLine($"GettingStartedConsumer - Data Real da Entrega: {DateTime.Now}");
            Console.WriteLine($"GettingStartedConsumer - Mensagem: {messageJson}");

            return Task.CompletedTask;
        }
    }
}
