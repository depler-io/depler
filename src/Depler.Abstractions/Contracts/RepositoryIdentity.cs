using Depler.Infrastructure;
using Depler.Validation;

namespace Depler.Abstractions.Contracts;

public class RepositoryIdentity : IIdentity<RepositoryIdentity>
{
    // Do not change that !!!!
    private static readonly Guid _namespaceId = Guid.Parse("58b89a61-270c-4d75-9026-2debd050da67");

    private Guid? _id;
    public Guid Id
    {
        get
        {
            return _id ?? (_id = DeterministicGuidGenerator.Create(_namespaceId, Name + Url)).Value;
        }
    }
    public string Name { get; }
    public string Url { get; }

    public RepositoryIdentity(string name, string url)
    {
        Must.NotBeNullOrEmpty(name);
        Must.NotBeNullOrEmpty(url);
        
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
