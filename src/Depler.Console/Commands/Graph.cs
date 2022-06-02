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
    }
    
    public sealed class Settings : LogSettings
    {
        
    }

    public override int Execute([NotNull] CommandContext context, [NotNull] Settings settings)
    {
        return 0;
    }
}
