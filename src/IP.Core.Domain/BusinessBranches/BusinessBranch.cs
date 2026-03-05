using IP.Core.Domain.BusinessSegments;

namespace IP.Core.Domain.BusinessBranches;

public sealed class BusinessBranch : EntityAuditableDeletableActivable<BusinessBranchId>
{
    public const int NAME_MAX_LENGTH = 100;
    public const int NAME_MIN_LENGTH = 3;
    public BusinessBranch()
    {
        Id = BusinessBranchId.Create();
    }
    public BusinessBranch(string name) : this()
    {
        Name = new BusinessBranchNameValue(name) { Value = name };
        ActivableInfo.SetAsActive();
    }
    public BusinessBranchNameValue Name { get; private set; } = null!;

    public ICollection<BusinessSegment> Segments { get; set; } = []!;
    public BusinessSegment? GetSegment(BusinessSegmentId businessSegmentId) => 
        Segments.FirstOrDefault(x => x.Id == businessSegmentId);
    public static BusinessBranch Create(string name) => new(name);
    public void Update(string name)
    {
        Name = new BusinessBranchNameValue(name) { Value = name };
    }
    public override string ToEntityName() => "Ramo de Negócio";
    public override string ToString() => $"{Name}";
}
public sealed class BusinessBranchNameValue : ValueObject
{
    public BusinessBranchNameValue()
    {
    }
    public BusinessBranchNameValue(string name)
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

public sealed class BusinessBranchId :
    TypedValue<Guid>,
    IEntityId<Guid>,
    ICreateGuid<BusinessBranchId, Guid>
{
    public BusinessBranchId() : base(Guid.CreateVersion7())
    {
    }
    public BusinessBranchId(Guid value) : base(value)
    {
    }
    public static BusinessBranchId Create() => new(Guid.NewGuid());
    public static BusinessBranchId Create(Guid value) => new(value);
    public override string ToString() => Value.ToString();
}
