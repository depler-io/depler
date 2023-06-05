namespace Depler.Core.Contracts;

public class PackageProducer
{
    public PackageProducer(string id)
    {
        Id = id;
    }

    public string Id { get; }
}
