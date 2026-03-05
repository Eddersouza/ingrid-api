namespace IP.Shared.Persistence.Converters;

public class EntityIdValueConverter<TValue> :
    ValueConverter<TValue, Guid> where TValue :
    IEntityId<Guid>, ICreateGuid<TValue, Guid>
{
    private static readonly Expression<Func<Guid, TValue>> _convertToEntity =
        value => LambdaHelper<TValue>.Create(value);

    private static readonly Expression<Func<TValue, Guid>> _convertToGuid =
        id => id.Value;

    public EntityIdValueConverter() :
        base(_convertToGuid, _convertToEntity)
    { }

    private static class LambdaHelper<U> where U :
        ICreateGuid<U, Guid>
    {
        public static U Create(Guid value) =>
            U.Create(value);
    }
}