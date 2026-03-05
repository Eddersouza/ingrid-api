
namespace IP.IDI.Domain.AppGuides;

public class AppGuideId :
    TypedValue<Guid>,
    IEntityId<Guid>,
    ICreateGuid<AppGuideId, Guid>
{
    public AppGuideId() : base(Guid.CreateVersion7())
    {
    }

    public AppGuideId(Guid value) : base(value)
    {
    }

    public static AppGuideId Create() => new();

    public static AppGuideId Create(Guid id) => new(id);

    public override string ToString() => Value.ToString();
}

public class AppGuide : EntityAuditableDeletableActivable<AppGuideId>
{
    public const int LINK_ID_MAX_LENGTH = 256;
    public const int LINK_ID_MIN_LENGTH = 10;
    public const int NAME_MAX_LENGTH = 100;
    public const int NAME_MIN_LENGTH = 10;

    public AppGuide()
    {
        Id = AppGuideId.Create();
    }

    public AppGuide(string name, string linkId) : this()
    {
        LinkId = linkId;
        Name = name;
        ActivableInfo.SetAsActive();
    }

    public string LinkId { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;

    public virtual ICollection<AppGuideViewed> Users { get; set; } = [];

    public static AppGuide Create(string name, string linkId) =>
        new(name, linkId);

    public IEnumerable<DateTime> GetDatesByUser(Guid userId) =>
        Users
            .Where(u => u.UserId == userId)
            .OrderByDescending(u => u.AuditableInfo.CreatedDate)
            .Select(u => u.AuditableInfo.CreatedDate);

    public AppUser? GetUser(Guid userId) =>
        Users.FirstOrDefault(u => u.UserId == userId)?.User;

    public void MarkUserViewAsInactive(Guid userId)
    {
        Users = [.. Users.Select(userView => {
            if(userView.UserId == userId && userView.ActivableInfo.Active)
                userView.ActivableInfo.SetAsDeactive();

            return userView;
        })];
    }

    public void SetAllViewWithFalse()
    {
        Users = [.. Users.Select(userView => {
            if(userView.ActivableInfo.Active)
                userView.ActivableInfo.SetAsDeactive();

            return userView;
        })];
    }

    public override string ToEntityName() => "Guia do Sistema";

    public override string ToString() => Name;

    public void Update(string name, string linkId)
    {
        Name = name;
        LinkId = linkId;
    }

    public bool ViewedUser(Guid userId) =>
        Users.Where(u => u.UserId == userId)
            .OrderByDescending(u => u.AuditableInfo.CreatedDate)
            .FirstOrDefault()?.ActivableInfo.Active ?? false;

    public bool WasViewedByUser(Guid id)
    {
        AppGuideViewed? user = Users
            .OrderByDescending(u => u.AuditableInfo.CreatedDate)
            .FirstOrDefault(u => u.UserId == id);

        return user?.ActivableInfo.Active ?? false;
    }
}