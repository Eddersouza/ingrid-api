namespace IP.Shared.Domain.Entities;

public abstract class ValueObjectEntity : ValueObject
{
    protected ValueObjectEntity()
    {
    }

    protected ValueObjectEntity(
        Guid? id,
        string name,
        string nameNormalized)
    {
        Id = id;
        Name = name;
        NameNormalized = nameNormalized;
    }

    public Guid? Id { get; init; } = default;
    public string Name { get; set; } = default!;
    public string NameNormalized { get; set; } = default!;

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Id!;
        yield return Name;
        yield return NameNormalized;
    }
}