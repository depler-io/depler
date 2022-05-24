using Depler.Validation;

namespace Depler.Abstractions.Contracts;

public class RepositoryIdentity : IEquatable<RepositoryIdentity>
{
    public string Name { get; }
    public string Url { get; }

    public RepositoryIdentity(string name, string url)
    {
        Must.NotBeNullOrEmpty(name, nameof(name));
        Must.NotBeNullOrEmpty(url, nameof(url));
        
        Name = name;
        Url = url;
    }

    public bool Equals(RepositoryIdentity? other)
    {
        if (other == null)
            return false;

        return 
            Name.Equals(other.Name, StringComparison.OrdinalIgnoreCase)
            && Url.Equals(other.Url, StringComparison.OrdinalIgnoreCase);
    }
}
