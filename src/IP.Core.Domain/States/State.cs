namespace IP.Core.Domain.States;

public sealed class State : EntityAuditableDeletable<StateId>
{
    public const int IBGE_CODE_MAX_LENGTH = 2;
    public const int IBGE_CODE_MIN_LENGTH = 2;
    public const int CODE_MAX_LENGTH = 2;
    public const int CODE_MIN_LENGTH = 2;
    public const int NAME_MAX_LENGTH = 50;
    public const int NAME_MIN_LENGTH = 4;

    public State()
    {
        Id = StateId.Create();
    }

    public State(string ibgeCode, string code, string name) : this()
    {
        IBGECode = ibgeCode;
        Code = code;
        Name = new StateNameValue(name) { Value = name };
    }

    public string IBGECode { get; private set; } = null!;
    public string Code { get; private set; } = null!;
    public StateNameValue Name { get; private set; } = null!;

    public static State CreateToSeed(Guid id, string ibgeCode, string code, string name)
        => new(ibgeCode, code, name) { Id = StateId.Create(id) };


    public static State Create(string ibgeCode, string code, string name) => new(ibgeCode, code, name);
    public void Update(string ibgeCode, string code, string name)
    {
        IBGECode = ibgeCode;
        Code = code;
        Name = new StateNameValue(name) { Value = name };
    }

    public override string ToEntityName() => "Estado";

    public override string ToString() => $"{Name} ({Code}) - {IBGECode}";
}

public sealed class StateNameValue : ValueObject
{
    public StateNameValue()
    {

    }

    public StateNameValue(string name)
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

    public override string ToString() => Value;
}

public sealed class StateId :
    TypedValue<Guid>,
    IEntityId<Guid>,
    ICreateGuid<StateId, Guid>
{
    public StateId() : base(Guid.CreateVersion7())
    {
    }

    public StateId(Guid value) : base(value)
    {
    }

    public static StateId Create() => new();

    public static StateId Create(Guid id) => new(id);

    public override string ToString() => Value.ToString();
}