using Serilog;
using Serilog.Core;

namespace Depler.Core.Logging;

public static class Logger
{
    public static ILogger Configure(LoggingLevelSwitch? logLevelSwitch = null, bool verbose = false)
    {
        var config = new LoggerConfiguration()
            .ConfigureConsole();

        config = logLevelSwitch != null
            ? config.MinimumLevel.ControlledBy(logLevelSwitch)
            : config.ConfigureLevel(verbose);

        Log.Logger = config.CreateLogger();
        return Log.Logger;
    }
    
    private static LoggerConfiguration ConfigureConsole(this LoggerConfiguration configuration)
    {
        return configuration.WriteTo.Console();
    }

    private static LoggerConfiguration ConfigureLevel(this LoggerConfiguration configuration, bool verbose)
    {
        return verbose ? 
            configuration.MinimumLevel.Verbose() : 
            configuration.MinimumLevel.Information();
    }
}
