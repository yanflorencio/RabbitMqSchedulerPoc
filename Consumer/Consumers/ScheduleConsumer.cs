using MassTransit;
using Shared;

namespace Consumer.Consumers
{
    public class ScheduleConsumer : IConsumer<ScheduleNotification>
    {
        public async Task Consume(ConsumeContext<ScheduleNotification> context)
        {
            Console.WriteLine($"ScheduleConsumer - Data de para realizar a entrega: {context.Message.DeliveryTime}");

            Uri finalDestination = new Uri("queue:getting-started");

            await context.ScheduleSend<HelloMessage>(finalDestination,
                context.Message.DeliveryTime,
                context.Message.HelloMessage
                );
        }
    }
}
