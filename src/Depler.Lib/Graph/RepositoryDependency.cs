using Depler.Lib.Contracts;
using Depler.Validation;
using QuikGraph;

namespace Depler.Lib.Graph;

public class RepositoryDependency : IEdge<Repository>
{
    public RepositoryDependency(Repository source, Repository target, IEnumerable<PackageDependency> packageDependencies)
    {
        var dependencies = 
            packageDependencies as PackageDependency[] 
            ?? packageDependencies.ToArray();
        
        Must.NotBeEmpty(dependencies, nameof(packageDependencies));
        
        Source = source;
        Target = target;
        PackageDependencies = dependencies;
    }

    public Repository Source { get; }
    public Repository Target { get; }
    
    public PackageDependency[] PackageDependencies { get; }
}
