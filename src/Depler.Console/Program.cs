using Depler.Console.Infrastructure;
using Depler.Core.IO;
using Depler.Core.Logging;
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

        Banner.PrintLogo();
        
        return new CommandApp(registrar)
            .RegisterCommands()
            .RunAsync(args);
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
