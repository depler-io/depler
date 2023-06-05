using Depler.Console.Commands;
using Depler.Console.Infrastructure;
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

