using Depler.Core.Contracts;
using Depler.Core.Graph;
using Depler.Validation;
// ReSharper disable ForCanBeConvertedToForeach

namespace Depler.Core.Repositories;

public class RepositoriesGraph
{
    private readonly Graph<Repository, RepositoryDependency> _graph = new();

    public RepositoriesGraph(IReadOnlyList<Repository> repositoriesList)
    {
        Must.NotBeEmpty(repositoriesList);

        PopulateGraph(repositoriesList);
    }

    private void PopulateGraph(IReadOnlyList<Repository> repositoriesList)
    {
        for (int i = 0; i < repositoriesList.Count; i++)
        {
            // Sources are the consumers
            var source = repositoriesList[i];

            if (!_graph.ContainsVertex(source))
            {
                _graph.AddVertex(source);
            }

            for (int j = 0; j < repositoriesList.Count; j++)
            {
                // Destination are producers
                var destination = repositoriesList[j];

                if (!_graph.ContainsVertex(destination))
                {
                    _graph.AddVertex(destination);
                }

                var dependency = CheckAndCreateDependency(source, destination);

                if (dependency != null)
                {
                    _graph.AddEdge(dependency);
                }
            }
        }
    }

    private static RepositoryDependency? CheckAndCreateDependency(Repository source, Repository destination)
    {
        var packageDependencies = new List<PackageDependency>();

        foreach (var producer in destination.Producers.Values)
        {
            var consumedPackages = source.GetConsumersByPackageId(producer.Id);
            packageDependencies.AddRange(
                consumedPackages.Select(
                    c => new PackageDependency(producer, c)));
        }

        return packageDependencies.Count > 0
            ? new RepositoryDependency(source, destination, packageDependencies)
            : null;
    }
}
