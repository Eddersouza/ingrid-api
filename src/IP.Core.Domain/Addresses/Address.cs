namespace IP.Core.Domain.Addresses;

public sealed class Address : EntityAuditableActivable<AddressId>
{
    public const int NAME_MAX_LENGTH = 100;
    public const int NAME_MIN_LENGTH = 5;
    public const int NEIGHBORHOOD_MAX_LENGTH = 100;
    public const int NEIGHBORHOOD_MIN_LENGTH = 5;
    public const int CODE_MAX_LENGTH = 10;
    public const int CODE_MIN_LENGTH = 8;
    public Address()
    {
        Id = AddressId.Create();
    }

    public Address(
        string name,
        string neighborhood,
        string code, 
        CityId cityId) : this()
    {
        Name = new NameAddressValue(name);
        Neighborhood = new NeighborhoodValue(neighborhood);
        Code = code;
        CityId = cityId;

        ActivableInfo.SetAsActive();
    }

    public NameAddressValue Name { get; private set; } = null!;
    public NeighborhoodValue Neighborhood { get; private set; } = null!;
    public string Code { get; private set; } = null!;
    public CityId CityId { get; private set; } = null!;
    public City City { get; private set; } = null!;

    public static Address Create(string name, string neighborhood, string code, CityId cityId) => new(name, neighborhood, code, cityId);

    public void Update(string name, string neighborhood, string code, CityId cityId)
    {
        Name = new NameAddressValue(name);
        Neighborhood = new NeighborhoodValue(neighborhood);
        Code = code;
        CityId = cityId;
    }
    public override string ToEntityName() => "Endereço";
    public override string ToString() => $"{City.State.Code} - {Name.Value} - {Neighborhood.Value} - {City.Name}";
}
public sealed class NameAddressValue : ValueObject
{
    public NameAddressValue()
    {

    }

    public NameAddressValue(string name)
    {
        Value = name;
        ValueNormalized = name.NormalizeCustom();
    }

    public string ValueNormalized { get; init; } = null!;
    public string Value { get; init; } = null!;

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return ValueNormalized;
        yield return Value;
    }
}
public sealed class NeighborhoodValue : ValueObject
{
    public NeighborhoodValue()
    {

    }

    public NeighborhoodValue(string name)
    {
        Value = name;
        ValueNormalized = name.NormalizeCustom();
    }

    public string ValueNormalized { get; init; } = null!;
    public string Value { get; init; } = null!;

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return ValueNormalized;
        yield return Value;
    }
}


public sealed class AddressId :
TypedValue<Guid>,
IEntityId<Guid>,
ICreateGuid<AddressId, Guid>
{
    public AddressId() : base(Guid.CreateVersion7())
    {
    }

    public AddressId(Guid value) : base(value)
    {
    }

    public static AddressId Create() => new();

    public static AddressId Create(Guid id) => new(id);

    public override string ToString() => Value.ToString();
}

