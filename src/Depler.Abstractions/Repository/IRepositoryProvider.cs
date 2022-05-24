using Depler.Abstractions.Contracts;

namespace Depler.Abstractions.Repository;

public interface IRepositoryProvider
{
    RepositoryIdentity[] GetRepositories();
}
