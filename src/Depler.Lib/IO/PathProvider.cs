using Nuke.Common.IO;

namespace Depler.Lib.IO;

public class PathProvider
{
    private static readonly AbsolutePath _deplerRoot;
    static PathProvider()
    {
        var path = 
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
                .ToAbsolutePath();
        _deplerRoot = path / "depler";
        FileSystemTasks.EnsureExistingDirectory(_deplerRoot);
    }

    public static AbsolutePath DeplerRoot => _deplerRoot;
    public static AbsolutePath DeplerConfig => _deplerRoot / "appsettings.json";
}
