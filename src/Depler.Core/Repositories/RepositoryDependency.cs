using Depler.Core.Contracts;
using Depler.Core.Graph;
using Depler.Validation;

namespace Depler.Core.Repositories;

public class RepositoryDependency : IEdge<Repository>
{
    public RepositoryDependency(Repository source, Repository target, IEnumerable<PackageDependency> packageDependencies)
    {
        var dependencies = 
            packageDependencies as PackageDependency[] 
            ?? packageDependencies.ToArray();
        
        Must.NotBeEmpty(dependencies);
        
        Source = source;
        Target = target;
        PackageDependencies = dependencies;
    }

    public Repository Source { get; }
    public Repository Target { get; }
    
    public PackageDependency[] PackageDependencies { get; }
}
