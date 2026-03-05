namespace IP.AccIPInfo.Api.AccountsIP;

public sealed class AccountIPResponseData(AccountIP account)
{
    public Guid Id { get; set; } = account.Id.Value;
    public int Number { get; set; } = account.Number;

    public string? Alias { get; set; } = account.Alias?.Value;

    public AccountIPStatus StatusCode { get; set; } = account.StatusCode;
    public string StatusDescription { get; set; } = account.StatusDescription;

    public AccountIPType TypeCode { get; set; } = account.TypeCode;
    public string TypeDescription { get; set; } = account.TypeDescription;

    public EntityChildData Customer { get; set; } = new EntityChildData(
        account.Customer.Id!.Value,
        account.Customer.Name);

    public EntityChildData? BusinessBranch { get; set; } =
        account.BusinessBranchSegment?.BranchName is not null ?
        new EntityChildData(
            account.BusinessBranchSegment.BranchId!.Value,
            account.BusinessBranchSegment.BranchName) : null;

    public EntityChildData? BusinessSegment { get; set; } =
        account.BusinessBranchSegment?.SegmentName is not null ?
        new EntityChildData(
            account.BusinessBranchSegment.SegmentId!.Value,
            account.BusinessBranchSegment.SegmentName) : null;

    public EntityChildData? Integrator { get; set; } =
        account.Integrator?.Name is not null ?
        new EntityChildData(
            account.Integrator.Id!.Value,
            account.Integrator.Name) : null;

    public AccountIPOwnerData? Owner { get; set; } = FillOwnerData(account.Owner!);

    private static AccountIPOwnerData? FillOwnerData(AccountIPOwner owner)
    {
        if (owner is null) return null;

        if (owner.OwnerIsIP.HasValue && owner.OwnerIsIP.Value)
            return new AccountIPOwnerData(true, Guid.Empty, "Responsável Interno");

        return owner?.Name is not null ?
        new AccountIPOwnerData(
            owner.OwnerIsIP,
            owner.Id,
            owner.Name) : null;
    }

    public EntityChildData? Retailer { get; set; } =
        account.Retailer?.Name is not null ?
        new EntityChildData(
            account.Retailer.Id!.Value,
            account.Retailer.Name) : null;

    public EntityChildData? Subscription { get; set; } =
        account.Subscription?.Name is not null ?
        new EntityChildData(
            account.Subscription.Id!.Value,
            account.Subscription.Name) : null;
}

public sealed record EntityChildData(Guid Id, string Name);
public sealed record AccountIPOwnerData(bool? OwnerIsIP, Guid? Id, string? Name);