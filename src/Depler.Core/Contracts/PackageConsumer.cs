using System.Collections.ObjectModel;
using Depler.Validation;

namespace Depler.Core.Contracts;

public class PackageConsumer
{
    public PackageConsumer(string id, IDictionary<string, Package> consumedPackages)
    {
        Must.NotBeNullOrEmpty(id);
        Must.NotBeEmpty(consumedPackages);
        
        Id = id;
        ConsumedPackages = new Dictionary<string, Package>(consumedPackages, StringComparer.OrdinalIgnoreCase);
    }

    public string Id { get; }
    public IReadOnlyDictionary<string, Package> ConsumedPackages { get; }
}
