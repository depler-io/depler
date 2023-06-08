using Depler.Console.Commands;
using Depler.Console.Infrastructure;
using Depler.Core.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Spectre.Console.Cli;

namespace Depler.Console;

public static class CommandAppExtensions
{
    public static CommandApp RegisterCommands(this CommandApp app)
    {
        app.Configure(appConfig =>
        {
            appConfig.Settings.ApplicationName = "depler";
            appConfig.SetInterceptor(new LogInterceptor());
            
            appConfig
                .AddCommand<Graph>("graph")
                .WithAlias("g")
                .WithDescription("Generate and show a graph of dependencies between repositories")
                .WithExample("graph");
        });

        return app;
    }
}

public static class ProgramExtensions
{
    public static IServiceCollection ConfigureServices(this IConfiguration config)
    {
        return new ServiceCollection()
            .AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddSerilog(
                    Logger.Configure(LogInterceptor.LogLevel)
                );
            });
    }
}

