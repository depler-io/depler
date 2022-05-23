using Depler.Console.Commands;
using Depler.Console.Infrastructure;
using Depler.Lib.IO;
using Depler.Lib.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nuke.Common.IO;
using Serilog;
using Spectre.Console.Cli;

namespace Depler.Console;

public class Program
{
    public static Task<int> Main(params string[] args)
    {
        if (!PathProvider.DeplerConfig.FileExists())
        {
            File.Copy("appsettings.json", PathProvider.DeplerConfig);
        }

        var config = new ConfigurationBuilder()
            .SetBasePath(PathProvider.DeplerRoot)
            .AddJsonFile(PathProvider.DeplerConfig, true, true)
            .AddEnvironmentVariables()
            .Build();
        
        var serviceCollection = ConfigureServices(config);
        var registrar = new TypeRegistrar(serviceCollection);

        var app = new CommandApp(registrar);
        app.Configure(appConfig =>
        {
            appConfig.Settings.ApplicationName = "depler";
            appConfig.SetInterceptor(new LogInterceptor());
            
            appConfig
                .AddCommand<Graph>("graph")
                .WithAlias("g")
                .WithDescription("Generate and show a graph of dependencies between repositories")
                .WithExample(new []{"graph"});
        });
        return app.RunAsync(args);
    }
    
    private static IServiceCollection ConfigureServices(IConfiguration config)
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
