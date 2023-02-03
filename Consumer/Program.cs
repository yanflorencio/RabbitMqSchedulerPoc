using Microsoft.Extensions.Hosting;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Hangfire.MemoryStorage;
using Hangfire;
using Consumer.Consumers;

namespace Consumer
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await CreateHostBuilder(args).Build().RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {

            var configuration = new ConfigurationBuilder()
                    .AddJsonFile($"appsettings.json");

            var config = configuration.Build();

            var schedulerEndpoint = new Uri($"queue:hangfire");

            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddTransient<ServiceProviderHangfireComponentResolver>();
                    //services.AddHangfire(x => x.UseRedisStorage());
                    services.AddHangfire(x => x.UseMemoryStorage());

                    services.AddMassTransit(x =>
                    {
                        x.SetKebabCaseEndpointNameFormatter();

                        x.AddMessageScheduler(schedulerEndpoint);

                        x.SetKebabCaseEndpointNameFormatter();

                        x.UsingRabbitMq((context, cfg) =>
                        {
                            cfg.UseMessageScheduler(schedulerEndpoint);

                            cfg.Host("localhost", "/", h =>
                            {
                                h.Username("guest");
                                h.Password("guest");
                            });

                            var resolver = context.GetRequiredService<ServiceProviderHangfireComponentResolver>();

                            cfg.UseHangfireScheduler(resolver, "hangfire", options =>
                            {
                                options.ConfigureServerOptions(config);
                                options.ServerName = $"MassTransit.{options.ServerName}";
                            });

                            cfg.ConfigureEndpoints(context);
                        });

                        x.AddConsumer<GettingStartedConsumer>();
                        x.AddConsumer<ScheduleConsumer>();
                    });
                });
        }
    }
}
