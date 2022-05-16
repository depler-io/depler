using Spectre.Console;
namespace depler.lib.extensions;

public static class ConsoleUtilities
{
    public static void ToConsole(this string input, Color color = new(), bool escape = true, bool newLine = true)
    {
        if (newLine)
        {
            if (escape) AnsiConsole.MarkupLineInterpolated($"[{color}]{input}[/]");
            else AnsiConsole.MarkupLine($"[{color}]{input}[/]");
        }
        else
        {
            if (escape) AnsiConsole.MarkupInterpolated($"[{color}]{input}[/]"); 
            else AnsiConsole.Markup($"[{color}]{input}[/]");
        }
    }
}
