namespace Depler.Abstractions.Contracts;

public interface IIdentity<TEntity> : IEquatable<TEntity>
{
    Guid Id { get; }
}
