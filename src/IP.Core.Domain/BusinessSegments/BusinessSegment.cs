using IP.Core.Domain.BusinessBranches;
using System.Xml.Linq;

namespace IP.Core.Domain.BusinessSegments;

public class BusinessSegment : EntityAuditableDeletableActivable<BusinessSegmentId>
{
    public const int SEGMENT_NAME_MAX_LENGTH = 100;
    public const int SEGMENT_NAME_MIN_LENGTH = 3;

    public BusinessSegment()
    {
        Id = BusinessSegmentId.Create();
    }

    public BusinessSegment(BusinessBranchId businessBranchId, string segmentName) : this()
    {
        BusinessBranchId = businessBranchId;
        SegmentName = new SegmentNameValue(segmentName) { Value = segmentName };
        ActivableInfo.SetAsActive();
    }

    public virtual BusinessBranch BusinessBranch { get; set; } = null!;
    public BusinessBranchId BusinessBranchId { get; set; } = null!;
    public SegmentNameValue SegmentName { get; set; } = null!;

    public static BusinessSegment Create(BusinessBranchId businessBranchId, string segmentName) => new(businessBranchId, segmentName);

    public override string ToEntityName() => "Segmento de Negócio";

    public override string ToString() => CreateStringLabel();

    public void Update(string segmentName)
    {
        SegmentName = new SegmentNameValue(segmentName) { Value = segmentName };
    }

    private string CreateStringLabel()
    {
        string branchName = BusinessBranch is null ? BusinessBranchId.Value.ToString() : BusinessBranch.Name.Value;
        string segmentName = SegmentName is null ? Id.ToString() : SegmentName.Value;

        return $"{branchName} - {segmentName}";
    }
}

public sealed class SegmentNameValue : ValueObject
{
    public SegmentNameValue()
    {
    }

    public SegmentNameValue(string name)
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

public class BusinessSegmentId :
    TypedValue<Guid>,
    IEntityId<Guid>,
    ICreateGuid<BusinessSegmentId, Guid>
{
    public BusinessSegmentId() : base(Guid.CreateVersion7())
    {
    }

    public BusinessSegmentId(Guid value) : base(value)
    {
    }

    public static BusinessSegmentId Create() => new();

    public static BusinessSegmentId Create(Guid id) => new(id);

    public override string ToString() => Value.ToString();
}