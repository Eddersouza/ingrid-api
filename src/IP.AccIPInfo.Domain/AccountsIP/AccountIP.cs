using System.Text;

namespace IP.AccIPInfo.Domain.AccountsIP;

public sealed class AccountIP : EntityAuditableDeletable<AccountIPId>
{    
    public const int STATUS_MAX_LENGTH = 50;
    public const int TYPE_MAX_LENGTH = 50;

    public AccountIP()
    {
        Id = AccountIPId.Create();
    }

    public AccountIP(
        int number,
        AccountIPAlias? alias,
        AccountIPCustomer customer,
        AccountIPStatus? statusCode,
        AccountIPType? typeCode) : this()
    {
        Number = number;
        Alias = alias;
        Customer = customer;
        StatusCode = statusCode ?? AccountIPStatus.Active;
        TypeCode = typeCode ?? AccountIPType.Transactional;
    }

    public AccountIPAlias? Alias { get; private set; } = null!;
    public AccountIPBusinessBranchSegment? BusinessBranchSegment { get; private set; } = null!;
    public AccountIPCustomer Customer { get; private set; } = null!;
    public AccountIPIntegratorSistem? Integrator { get; private set; } = null!;
    public int Number { get; private set; } = default;
    public AccountIPOwner? Owner { get; private set; } = null!;
    public AccountIPRetailer? Retailer { get; private set; } = null!;
    public AccountIPStatus StatusCode { get; private set; }
    public string StatusDescription => StatusCode.GetDescription();
    public AccountIPAccountSubscription? Subscription { get; private set; } = null!;
    public AccountIPType TypeCode { get; private set; }
    public string TypeDescription => TypeCode.GetDescription();

    public static AccountIP Create(
        int number,
        AccountIPAlias? alias,
        AccountIPCustomer customer,
        AccountIPStatus? statusCode,
        AccountIPType? typeCode) =>
        new(number, alias, customer, statusCode, typeCode);

    public void AddBusinessBranch(
        AccountIPBusinessBranchSegment businessBranchSegment) =>
        BusinessBranchSegment = businessBranchSegment;

    public void AddIntegrator(AccountIPIntegratorSistem accountIPIntegratorSistem) =>
        Integrator = accountIPIntegratorSistem;

    public void AddOwner(AccountIPOwner accountIPOwner) =>
        Owner = accountIPOwner;

    public void AddRetailer(AccountIPRetailer accountIPRetailer) =>
        Retailer = accountIPRetailer;

    public void AddSubscription(AccountIPAccountSubscription accountIPIntegratorSistem) =>
        Subscription = accountIPIntegratorSistem;

    public void SetAlias(AccountIPAlias? alias) =>
        Alias = alias;

    public void SetCustomer(AccountIPCustomer customer) =>
        Customer = customer;

    public void SetNumber(int number) =>
        Number = number;

    public void SetStatusCode(AccountIPStatus? statusCode) =>
        StatusCode = statusCode ?? AccountIPStatus.Active;

    public void SetTypeCode(AccountIPType? typeCode) =>
        TypeCode = typeCode ?? AccountIPType.Transactional;

    public override string ToEntityName() => "Conta";

    public override string ToString()
    {
        StringBuilder sb = new();
        sb.Append($"{Number} - {Customer.Name} - {StatusCode.GetDescription()}");
        return sb.ToString();
    }
}

public sealed class AccountIPAccountSubscription
    : ValueObjectEntity
{
    public const int MAX_LENGTH = 256;
    public const int MIN_LENGTH = 10;

    public AccountIPAccountSubscription()
    { }

    public AccountIPAccountSubscription(
        Guid? subscriptionId,
        string subscriptionName) :
        base(subscriptionId,
            subscriptionName,
            subscriptionName.NormalizeCustom())
    { }
}

public sealed class AccountIPAlias : ValueObject
{
    public const int MAX_LENGTH = 256;

    public AccountIPAlias()
    { }

    public AccountIPAlias(string name)
    {
        Value = name;
        ValueNormalized = name.NormalizeCustom();
    }

    public string? Value { get; init; } = null!;
    public string? ValueNormalized { get; init; } = null!;

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return ValueNormalized!;
        yield return Value!;
    }
}

public sealed class AccountIPBusinessBranchSegment : ValueObject
{
    public const int BRANCH_NAME_MAX_LENGTH = 256;
    public const int BRANCH_NAME_MIN_LENGTH = 3;
    public const int SEGMENT_NAME_MAX_LENGTH = 256;
    public const int SEGMENT_NAME_MIN_LENGTH = 3;

    public AccountIPBusinessBranchSegment()
    { }

    public AccountIPBusinessBranchSegment(
        Guid? branchId,
        string branchName,
        Guid? segmentId,
        string segmentName)
    {
        BranchId = branchId;
        BranchName = branchName;
        SegmentId = segmentId;
        SegmentName = segmentName;
    }

    public Guid? BranchId { get; init; } = default;
    public string BranchName { get; set; } = null!;

    public Guid? SegmentId { get; init; } = default;
    public string SegmentName { get; set; } = null!;

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return BranchId!;
        yield return BranchName;
        yield return SegmentId!;
        yield return SegmentName;
    }
}

public sealed class AccountIPCustomer : ValueObjectEntity
{
    public const int MAX_LENGTH = 256;
    public const int MIN_LENGTH = 10;

    public AccountIPCustomer()
    { }

    public AccountIPCustomer(
        Guid customerId,
        string customerName) :
        base(customerId,
            customerName,
            customerName.NormalizeCustom())
    { }
}

public sealed class AccountIPIntegratorSistem : ValueObjectEntity
{
    public const int MAX_LENGTH = 256;
    public const int MIN_LENGTH = 10;

    public AccountIPIntegratorSistem()
    { }

    public AccountIPIntegratorSistem(
        Guid? integratorId,
        string integratorName) :
        base(integratorId,
            integratorName,
            integratorName.NormalizeCustom())
    { }
}

public sealed class AccountIPOwner : ValueObjectEntity
{
    public const int MAX_LENGTH = 256;
    public const int MIN_LENGTH = 10;

    public AccountIPOwner()
    { }

    public AccountIPOwner(
        bool ownerIsIP) :
        base(null, null!, null!)
    {
        OwnerIsIP = ownerIsIP;
    }

    public AccountIPOwner(
       Guid? customerId = null,
       string? customerName = null) :
       base(customerId,
           customerName!,
           customerName?.NormalizeCustom() ?? null!)
    {
        OwnerIsIP = false;
    }

    public bool? OwnerIsIP { get; private set; } = null;
}

public sealed class AccountIPRetailer : ValueObjectEntity
{
    public const int MAX_LENGTH = 256;
    public const int MIN_LENGTH = 10;

    public AccountIPRetailer()
    { }

    public AccountIPRetailer(
        Guid? customerId,
        string customerName) :
        base(customerId,
            customerName,
            customerName.NormalizeCustom())
    { }
}

public sealed class AccountIPId :
    TypedValue<Guid>,
    IEntityId<Guid>,
    ICreateGuid<AccountIPId, Guid>
{
    public AccountIPId() : base(Guid.CreateVersion7())
    {
    }

    public AccountIPId(Guid value) : base(value)
    {
    }

    public static AccountIPId Create() => new();

    public static AccountIPId Create(Guid id) => new(id);

    public override string ToString() => Value.ToString();
}