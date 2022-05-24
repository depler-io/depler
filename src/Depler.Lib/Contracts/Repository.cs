using System.Diagnostics;
using Depler.Abstractions.Contracts;
using Depler.Validation;
using Nuke.Common.IO;

namespace Depler.Lib.Contracts;

[DebuggerDisplay("[{Identity.Name}]")]
public class Repository : IEquatable<Repository>
{
    public RepositoryIdentity Identity { get; }
    public AbsolutePath ClonePath { get; }

    public Repository(RepositoryIdentity identity, AbsolutePath clonePath)
    {
        Must.NotBeNull(identity, nameof(identity));
        Must.NotBeNull(clonePath, nameof(clonePath));
        
        Identity = identity;
        ClonePath = clonePath;
    }

    public bool Equals(Repository? other)
    {
        if (other == null)
            return false;

        return 
            Identity.Equals(other.Identity)
            && ClonePath.Equals(other.ClonePath);
    }
}
