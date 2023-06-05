using Depler.Validation;
using Nuke.Common.IO;

namespace Depler.Core.IO;

public static class Extensions
{
    public static AbsolutePath ToAbsolutePath(this string path, string? rootPath = null)
    {
        Must.NotBeNullOrEmpty(path);

        if (!string.IsNullOrEmpty(rootPath))
        {
            return (AbsolutePath)rootPath / path;
        }
        
        return (AbsolutePath)path;
    }
}
