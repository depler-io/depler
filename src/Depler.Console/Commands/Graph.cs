using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;
using Spectre.Console.Cli;

namespace Depler.Console.Commands;

public class Graph : Command<Graph.Settings>
{
    private ILogger<Graph> _logger;

    public Graph(ILogger<Graph> logger)
    {
        _logger = logger;
        _logger.LogDebug("{Class} initialized", nameof(Graph));
        _logger.LogInformation("{Class} initialized", nameof(Graph));
    }
    
    public sealed class Settings : LogSettings
    {
        
    }

    public override int Execute([NotNull] CommandContext context, [NotNull] Settings settings)
    {
        _logger.LogDebug("Executing {CommandName} command", nameof(Graph));
        _logger.LogInformation("Executing {CommandName} command", nameof(Graph));
        return 0;
    }
}
