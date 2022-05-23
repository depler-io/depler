using Depler.Lib.Validation;
using Nuke.Common.IO;

namespace Depler.Lib.IO;

public static class Extensions
{
    public static AbsolutePath ToAbsolutePath(this string path, string? rootPath = null)
    {
        Contract.RequiresNotNullOrEmpty(path, nameof(path));

        if (!string.IsNullOrEmpty(rootPath))
        {
            return (AbsolutePath)rootPath / path;
        }
        
        return (AbsolutePath)path;
    }
}
