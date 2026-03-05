using IP.Core.Domain.VO;

namespace IP.Core.Domain.Customers;

public sealed class Customer : EntityAuditableDeletable<CustomerId>
{
    public const int NAME_MAX_LENGTH = 256;
    public const int NAME_MIN_LENGTH = 5;
    public const int PERSONTYPE_MAX_LENGTH = 50;
    public const int REMARKS_MAX_LENGTH = 5000;
    public const int STATUS_MAX_LENGTH = 50;
    public const int TRADINGNAME_MAX_LENGTH = 256;
    public const int TRADINGNAME_MIN_LENGTH = 5;
    public const int EXTERNAL_ID_MAX_LENGTH = 50;

    public Customer() => Id = CustomerId.Create();

    public Customer(
        PersonTypeEnum personTypeCode,
        CustomerName name,
        CustomerTradingName tradingName,
        CPFOrCNPJ documentNumber,
        CustomerStatusEnum statusCode,
        string? externalId = null) : this()
    {
        Name = name;
        TradingName = tradingName;
        PersonTypeCode = personTypeCode;
        DocumentNumber = documentNumber;
        StatusCode = statusCode;
        ExternalId = externalId;
    }

    public CPFOrCNPJ DocumentNumber { get; private set; } = default!;
    public string? ExternalId { get; private set; } = null!;
    public CustomerName Name { get; private set; } = default!;

    public PersonTypeEnum PersonTypeCode { get; private set; } = default!;
    public string Remarks { get; set; } = null!;
    public CustomerStatusEnum StatusCode { get; private set; } = default!;
    public CustomerTradingName TradingName { get; private set; } = default!;

    public static Customer Create(
        PersonTypeEnum personType,
        CustomerName name,
        CustomerTradingName tradingName,
        CPFOrCNPJ documentNumber,
        CustomerStatusEnum status,
        string? externalId = null) => new(personType, name, tradingName, documentNumber, status, externalId);

    public void SetDocumentNumber(CPFOrCNPJ value) => DocumentNumber = value;

    public void SetName(CustomerName value) => Name = value;

    public void SetPersonType(PersonTypeEnum value) => PersonTypeCode = value;

    public void SetStatus(CustomerStatusEnum value) => StatusCode = value;

    public void SetTradingName(CustomerTradingName value) => TradingName = value;

    public override string ToEntityName() => "Cliente";

    public override string ToString() => $"{Name.Value} - {TradingName.Value}";
}

public sealed class CustomerName : ValueObject
{
    public CustomerName()
    {
    }

    public CustomerName(string name)
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

public sealed class CustomerTradingName : ValueObject
{
    public CustomerTradingName()
    {
    }

    public CustomerTradingName(string name)
    {
        Value = name;

        if (name.IsNotNullOrWhiteSpace())
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

public sealed class CustomerId :
    TypedValue<Guid>,
    IEntityId<Guid>,
    ICreateGuid<CustomerId, Guid>
{
    public CustomerId() : base(Guid.CreateVersion7())
    {
    }

    public CustomerId(Guid value) : base(value)
    {
    }

    public static CustomerId Create() => new();

    public static CustomerId Create(Guid id) => new(id);

    public override string ToString() => Value.ToString();
}