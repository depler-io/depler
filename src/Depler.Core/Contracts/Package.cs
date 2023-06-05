namespace Depler.Core.Contracts;

public class Package
{
    public Package(string id)
    {
        Id = id;
    }

    public string Id { get; }
}
