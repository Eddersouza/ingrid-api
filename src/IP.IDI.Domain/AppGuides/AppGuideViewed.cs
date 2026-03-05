namespace IP.IDI.Domain.AppGuides;

public class AppGuideViewedId :
    TypedValue<Guid>,
    IEntityId<Guid>,
    ICreateGuid<AppGuideViewedId, Guid>
{
    public AppGuideViewedId() : base(Guid.CreateVersion7())
    {
    }

    public AppGuideViewedId(Guid value) : base(value)
    {
    }

    public static AppGuideViewedId Create() => new();

    public static AppGuideViewedId Create(Guid id) => new(id);

    public override string ToString() => Value.ToString();
}

public class AppGuideViewed : EntityAuditableActivable<AppGuideViewedId>
{
    public AppGuideViewed()
    {
        Id = AppGuideViewedId.Create();
    }

    public AppGuideViewed(Guid userId): this()
    {
        UserId = userId;
        ActivableInfo.SetAsActive();
    }

    public Guid UserId { get; set; }

    public virtual AppUser? User { get; set; } = null;

    public AppGuideId AppGuideId { get; set; } = null!;
    public virtual AppGuide? AppGuide { get; set; } = null;

    public override string ToEntityName() => "Guia do Aplicativo Visualizado";

    public override string ToString() => CreateStringLabel();

    public static AppGuideViewed Create(Guid userId) => new(userId);

    private string CreateStringLabel()
    {
        string appName = AppGuide is null ? AppGuideId.Value.ToString() : AppGuide.Name;
        string userName = User is null ? UserId.ToString() : User.UserName!;

        return $"{appName} - {userName}";
    }
}