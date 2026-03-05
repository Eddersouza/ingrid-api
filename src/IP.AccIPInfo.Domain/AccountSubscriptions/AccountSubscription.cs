namespace IP.AccIPInfo.Domain.AccountSubscriptions;

public sealed class AccountSubscription : EntityAuditableDeletableActivable<AccountSubscriptionId>
{
    public const int NAME_MAX_LENGTH = 50;
    public const int NAME_MIN_LENGTH = 3;
    public const int EXTERNAL_ID_MAX_LENGTH = 50;

    public AccountSubscription()
    {
        Id = AccountSubscriptionId.Create();
    }
    public AccountSubscription(string name, string? externalId) : this()
    {
        Name = new AccountSubscriptionNameValue(name) { Value = name };

        ActivableInfo.SetAsActive();
    }

    public AccountSubscriptionNameValue Name { get; private set; } = null!;

    public string? ExternalId { get; private set; } = null;

    public static AccountSubscription Create(string name, string? externalId) => new(name, externalId);
    public void Update(string name)
    {
        Name = new AccountSubscriptionNameValue(name) { Value = name };
    }


    public override string ToEntityName() => "Plano";

    public override string ToString() => $"{Name}";
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
    public string ValueNormalized { get; init; } = null!;
    public string Value { get; init; } = null!;
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
