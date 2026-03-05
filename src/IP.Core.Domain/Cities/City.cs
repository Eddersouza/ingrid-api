namespace IP.Core.Domain.Cities;

public sealed class  City : EntityAuditableDeletableActivable<CityId>
{
    public const int IBGE_CODE_MAX_LENGTH = 7;
    public const int IBGE_CODE_MIN_LENGTH = 7;
    public const int NAME_MAX_LENGTH = 50;
    public const int NAME_MIN_LENGTH = 4;

    public City()
    {
        Id = CityId.Create();
    }

    public City(string ibgeCode, string name, StateId stateId) : this()
    {
        IBGECode = ibgeCode;
        Name = new CityNameValue(name) { Value = name };
        StateId = stateId;

        ActivableInfo.SetAsActive();

    }
    public string IBGECode { get; private set; } = null!;
    public CityNameValue Name { get; private set; } = null!;
    public StateId StateId { get; private set; } = null!;
    public State State { get; private set; } = null!;

    public static City Create(string ibgeCode, string name, StateId stateId) => new(ibgeCode, name, stateId);
    public void Update(string name, string ibgeCode, StateId stateId)
    {
        Name = new CityNameValue(name) { Value = name };
        IBGECode = ibgeCode;
        StateId = stateId;
    }

    public override string ToEntityName() => "Cidade";

    public override string ToString() => $"{Name}";
}
public sealed class CityNameValue : ValueObject
{
    public CityNameValue()
    {

    }

    public CityNameValue(string name)
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
public sealed class CityId :
    TypedValue<Guid>,
    IEntityId<Guid>,
    ICreateGuid<CityId, Guid>
{
    public CityId() : base(Guid.CreateVersion7())
    {
    }

    public CityId(Guid value) : base(value)
    {
    }

    public static CityId Create() => new();

    public static CityId Create(Guid id) => new(id);

    public override string ToString() => Value.ToString();
}
