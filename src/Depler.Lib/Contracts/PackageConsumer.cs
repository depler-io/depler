using System.Collections.ObjectModel;
using Depler.Validation;

namespace Depler.Lib.Contracts;

public class PackageConsumer
{
    public PackageConsumer(string id, IDictionary<string, Package> consumedPackages)
    {
        Must.NotBeNullOrEmpty(id, nameof(id));
        Must.NotBeEmpty(consumedPackages, nameof(consumedPackages));
        
        Id = id;
        ConsumedPackages = new Dictionary<string, Package>(consumedPackages, StringComparer.OrdinalIgnoreCase);
    }

    public string Id { get; }
    public IReadOnlyDictionary<string, Package> ConsumedPackages { get; }
}
