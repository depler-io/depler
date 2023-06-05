using System.Diagnostics;
using Depler.Abstractions.Contracts;
using Depler.Core.Graph;
using Depler.Validation;
using Nuke.Common.IO;

namespace Depler.Core.Contracts;

[DebuggerDisplay("[{Identity.Name}]")]
public class Repository: IEquatable<Repository>, IVertex
{
    public Guid Id => Identity.Id;
    public RepositoryIdentity Identity { get; }
    public AbsolutePath ClonePath { get; }
    
    public IDictionary<string, PackageConsumer> Consumers { get; }
    public IDictionary<string, PackageProducer> Producers { get; }

    public Repository(RepositoryIdentity identity, AbsolutePath clonePath,
        IDictionary<string, PackageConsumer> consumers, IDictionary<string, PackageProducer> producers)
    {
        Must.NotBeNull(identity);
        Must.NotBeNull(clonePath);
        Must.NotBeNull(consumers);
        Must.NotBeNull(producers);

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
            Id.Equals(other.Id)
            && ClonePath.Equals(other.ClonePath);
    }
}
