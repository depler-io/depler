using Serilog;

namespace depler.lib.utilities;

public static class Logger
{
    public static void Configure()
    {
        Log.Logger = new LoggerConfiguration()
            .ConfigureConsole()
            .ConfigureLevel()
            .CreateLogger();
    }
    
    private static LoggerConfiguration ConfigureConsole(this LoggerConfiguration configuration)
    {
        return configuration.WriteTo.Console();
    }

    private static LoggerConfiguration ConfigureLevel(this LoggerConfiguration configuration)
    {
        return configuration.MinimumLevel.Verbose();
    }
}
