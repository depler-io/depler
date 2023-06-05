namespace Depler.Console;

public static class Banner
{
    private static readonly string[] _consoleLogo =
    {
        @"     _            _           ",
        @"    | |          | |          ",
        @"  __| | ___ _ __ | | ___ _ __ ",
        @" / _` |/ _ \ '_ \| |/ _ \ '__|",
        @"| (_| |  __/ |_) | |  __/ |   ",
        @" \__,_|\___| .__/|_|\___|_|   ",
        @"           | |                ",
        @"           |_|                "
    };
    
    public static void PrintLogo()
    {
        foreach (var row in _consoleLogo)
            System.Console.WriteLine(row);
    }
}
