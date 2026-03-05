namespace IP.AccIPInfo.Domain.IntegratorSystems;

public sealed class IntegratorSystem : EntityAuditableDeletableActivable<IntegratorSystemId>
{
    public const int NAME_MAX_LENGTH = 50;
    public const int NAME_MIN_LENGTH = 3;
    public const int SITE_URL_MAX_LENGTH = 100;
    public const int DESCRIPTION_MAX_LENGTH = 100;

    public IntegratorSystem()
    {
        Id = IntegratorSystemId.Create();
    }

    public IntegratorSystem(string name, string siteUrl, string description) : this()
    {
        Name = new IntegratorSystemNameValue(name) { Value = name };
        SiteUrl = siteUrl;
        Description = description;

        ActivableInfo.SetAsActive();
    }

    public IntegratorSystemNameValue Name { get; private set; } = null!;
    public string SiteUrl { get; private set; } = null!;
    public string Description { get; private set; } = null!;

    public static IntegratorSystem Create(string name, string siteUrl, string description) => new(name, siteUrl, description);

    public void Update(string name, string siteUrl, string description)
    {
        Name = new IntegratorSystemNameValue(name) { Value = name };
        SiteUrl = siteUrl;
        Description = description;
    }

    public override string ToEntityName() => "Sistema";

    public override string ToString() => $"{Name}";
}

public sealed class IntegratorSystemNameValue : ValueObject
{
    public IntegratorSystemNameValue()
    {
    }

    public IntegratorSystemNameValue(string name)
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

public sealed class IntegratorSystemId :
    TypedValue<Guid>,
    IEntityId<Guid>,
    ICreateGuid<IntegratorSystemId, Guid>
{
    public IntegratorSystemId() : base(Guid.CreateVersion7())
    {
    }

    public IntegratorSystemId(Guid value) : base(value)
    {
    }

    public static IntegratorSystemId Create() => new();

    public static IntegratorSystemId Create(Guid id) => new(id);

    public override string ToString() => Value.ToString();
}