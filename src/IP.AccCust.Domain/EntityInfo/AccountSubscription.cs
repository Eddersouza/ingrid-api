namespace IP.AccCust.Domain.EntityInfo;

public sealed class AccountSubscription : EntityAuditableDeletableActivable<AccountSubscriptionId>
{
    public const int EXTERNAL_ID_MAX_LENGTH = 50;
    public const int NAME_MAX_LENGTH = 50;
    public const int NAME_MIN_LENGTH = 3;

    public AccountSubscription()
    {
        Id = AccountSubscriptionId.Create();
    }

    public AccountSubscription(string name, string? externalId = null) : this()
    {
        Name = new AccountSubscriptionNameValue(name) { Value = name };
        ExternalId = externalId;
        ActivableInfo.SetAsActive();
    }

    public string? ExternalId { get; private set; } = null;
    public AccountSubscriptionNameValue Name { get; private set; } = null!;

    public static AccountSubscription Create(string name, string? externalId) => new(name, externalId);

    public void SetName(string name) => Name = new AccountSubscriptionNameValue(name) { Value = name };

    public override string ToEntityName() => "Plano";

    public override string ToString() => $"{Name}";

    public void Update(string name)
    {
        Name = new AccountSubscriptionNameValue(name) { Value = name };
    }
}

public sealed class AccountSubscriptionNameValue : ValueObject
{
    public AccountSubscriptionNameValue()
    {
    }

    public AccountSubscriptionNameValue(string name)
    {
        Value = name;
        ValueNormalized = name.NormalizeCustom();
    }

    public string Value { get; init; } = null!;
    public string ValueNormalized { get; init; } = null!;

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return ValueNormalized;
        yield return Value;
    }
}

public sealed class AccountSubscriptionId :
    TypedValue<Guid>,
    IEntityId<Guid>,
    ICreateGuid<AccountSubscriptionId, Guid>
{
    public AccountSubscriptionId() : base(Guid.CreateVersion7())
    {
    }

    public AccountSubscriptionId(Guid value) : base(value)
    {
    }

    public static AccountSubscriptionId Create() => new();

    public static AccountSubscriptionId Create(Guid id) => new(id);

    public override string ToString() => Value.ToString();
}