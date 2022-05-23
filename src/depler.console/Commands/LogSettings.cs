using System.ComponentModel;
using Spectre.Console.Cli;

namespace Depler.Console.Commands;

public class LogSettings : CommandSettings
{
    [Description("Turn on verbose (debug) logging")]
    [CommandOption("-v|--verbose")]
    [DefaultValue(false)]
    public bool Verbose { get; set; }
}

