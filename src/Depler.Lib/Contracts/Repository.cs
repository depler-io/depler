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
    
    public IDictionary<string, PackageConsumer> Consumers { get; }
    public IDictionary<string, PackageProducer> Producers { get; }

    public Repository(RepositoryIdentity identity, AbsolutePath clonePath,
        IDictionary<string, PackageConsumer> consumers, IDictionary<string, PackageProducer> producers)
    {
        Must.NotBeNull(identity, nameof(identity));
        Must.NotBeNull(clonePath, nameof(clonePath));
        Must.NotBeNull(consumers, nameof(consumers));
        Must.NotBeNull(producers, nameof(producers));

        Identity = identity;
        ClonePath = clonePath;
        Consumers = consumers;
        Producers = producers;
    }

    public PackageConsumer[] GetConsumersByPackageId(string packageId)
    {
        return Consumers.Values.Where(c => c.ConsumedPackages.ContainsKey(packageId)).ToArray();
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
