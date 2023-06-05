using Depler.Core.Contracts;

namespace Depler.Core.Repositories;

public class PackageDependency
{
    public PackageDependency(PackageProducer producer, PackageConsumer consumer)
    {
        Producer = producer;
        Consumer = consumer;
    }

    public PackageProducer Producer { get; }
    
    public PackageConsumer Consumer { get; }
    
}
