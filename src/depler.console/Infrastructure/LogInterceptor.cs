using Depler.Console.Commands;
using Serilog.Core;
using Serilog.Events;
using Spectre.Console.Cli;

namespace Depler.Console.Infrastructure;

public class LogInterceptor : ICommandInterceptor
{
    public static readonly LoggingLevelSwitch LogLevel = new();

    public void Intercept(CommandContext context, CommandSettings settings)
    {
        if (settings is LogSettings rootSettings)
        {
            LogLevel.MinimumLevel = rootSettings.Verbose ? LogEventLevel.Verbose : LogEventLevel.Information;
        }
    }
}
