using Depler.Lib.Contracts;

namespace Depler.Lib.Graph;

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
