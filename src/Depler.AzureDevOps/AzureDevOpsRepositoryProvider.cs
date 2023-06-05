
using Depler.Abstractions.Contracts;
using Depler.Abstractions.Repository;

namespace Depler.AzureDevOps;

public class AzureDevOpsRepositoryProvider : IRepositoryProvider
{
    public RepositoryIdentity[] GetRepositories()
    {
        throw new NotImplementedException();
    }
}
