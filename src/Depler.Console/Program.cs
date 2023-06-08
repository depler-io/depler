using Depler.Console;
using Depler.Console.Infrastructure;
using Depler.Core.IO;
using Microsoft.Extensions.Configuration;
using Nuke.Common.IO;
using Spectre.Console.Cli;

if (!PathProvider.DeplerConfig.FileExists())
{
    File.Copy("appsettings.json", PathProvider.DeplerConfig);
}

var config = new ConfigurationBuilder()
    .SetBasePath(PathProvider.DeplerRoot)
    .AddJsonFile(PathProvider.DeplerConfig, true, true)
    .AddEnvironmentVariables()
    .Build();

var serviceCollection = config.ConfigureServices();
var registrar = new TypeRegistrar(serviceCollection);

Banner.PrintLogo();

return await new CommandApp(registrar)
    .RegisterCommands()
    .RunAsync(args);
