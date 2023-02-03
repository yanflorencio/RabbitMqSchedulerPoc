using Hangfire;
using Microsoft.Extensions.Configuration;

namespace Consumer
{
    public static class HangfireExtensions
    {
        public static BackgroundJobServerOptions ConfigureServerOptions(this BackgroundJobServerOptions serverOptions, IConfiguration configuration)
        {
            configuration.GetSection("Hangfire:BackgroundJobServerOptions").Bind(serverOptions);
            serverOptions.ServerName = serverOptions.ServerName?.Replace("[MACHINE_NAME]", Environment.MachineName);

            return serverOptions;
        }
    }
}
