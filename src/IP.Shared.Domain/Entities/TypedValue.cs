namespace IP.Shared.Domain.Entities;

public interface IEntityId<out T> where T : struct
{
    T Value { get; }
}

public interface ICreateGuid<out T, in Guid>
{
    public abstract static T Create(Guid identifier);
}

public abstract class TypedValue<Type>(Type value)
   : IEquatable<TypedValue<Type>> where Type
   : IComparable<Type>
{
    public Type Value { get; } = value;

    public static bool operator !=(TypedValue<Type>? left, TypedValue<Type>? right) =>
        !Equals(left, right);

    public static bool operator ==(TypedValue<Type>? left, TypedValue<Type>? right) =>
        Equals(left, right);

    public bool Equals(TypedValue<Type>? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return EqualityComparer<Type>.Default.Equals(Value, other.Value);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((TypedValue<Type>)obj);
    }

    public override int GetHashCode() =>
        EqualityComparer<Type>.Default.GetHashCode(Value);
}