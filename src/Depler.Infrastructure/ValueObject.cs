using System.Collections.Concurrent;
using System.Reflection;
using System.Text;

namespace Depler.Core.Infrastructure;

public abstract class ValueObject
{
    private static readonly ConcurrentDictionary<Type, PropertyInfo[]> _typeProperties = new();

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true;
        if (ReferenceEquals(null, obj)) return false;
        if (GetType() != obj.GetType()) return false;
        var other = obj as ValueObject;
        return other != null && GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    public override int GetHashCode()
    {
        unchecked
        {
            return GetEqualityComponents().Aggregate(17, (current, obj) => current * 23 + (obj?.GetHashCode() ?? 0));
        }
    }

    public static bool operator ==(ValueObject left, ValueObject right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(ValueObject? left, ValueObject? right)
    {
        return !Equals(left, right);
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        foreach (var f in GetProperties())
        {
            sb.Append(f.Name);
            sb.Append(":");
            sb.Append(f.GetValue(this));
        }

        return sb.ToString();
    }

    protected virtual object?[] GetEqualityComponents()
    {
        var props = GetProperties();
        var result = new object?[props.Length];

        for (var i = 0; i < props.Length; i++)
            result[i] = props[i].GetValue(this);

        return result;
    }

    protected virtual PropertyInfo[] GetProperties()
    {
        return _typeProperties.GetOrAdd(
            GetType(),
            t => t
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .OrderBy(p => p.Name)
                .ToArray());
    }
}
