using depler.lib.utilities;

Logger.Configure();

if (args.Length < 1)
{
    Serilog.Log.Error(
        "You must pass at least one command to [underline lime]depler[/]. Use [yellow]depler --help[/] to see available options");
    return -1;
}

return 0;
