namespace IP.AccIPInfo.Api.Transactions;

public interface ITransactionSummaryConditions
{
    DateTime? StartDate { get; set; }
    DateTime? EndDate { get; set; }
    Guid? CustomerId { get; set; }
    Guid? OwnerFilterId { get; set; }
    bool? OwnerIsIP { get; set; }
    Guid? RetailerId { get; set; }
    Guid? IntegratorId { get; set; }
}

public interface ITransactionSummaryService
{
    Task<IQueryable<AccountMovementSummary>> GetSummaryFiltered(
        Guid ownerId,
        IQueryable<AccountMovementSummary> queryAccount,
        ITransactionSummaryConditions queryRequest);
}

public sealed class TransactionSummaryService(
    ICurrentUserInfo _currentUserInfo,
    IEmployeeEndpoint _employeeEndpoint) : ITransactionSummaryService
{
    public async Task<IQueryable<AccountMovementSummary>> GetSummaryFiltered(
        Guid ownerId,
        IQueryable<AccountMovementSummary> queryAccount,
        ITransactionSummaryConditions queryRequest)
    {
        DateTime today = DateTime.Now;
        DateTime startDate = queryRequest.StartDate.FirstMomentOfMonth(today);
        DateTime endDate = queryRequest.EndDate.LastMomentOfMonth(today);
        Guid? customerId = queryRequest.CustomerId;
        Guid? ownerIdFilter = queryRequest.OwnerFilterId;
        bool? ownerIsIP = queryRequest.OwnerIsIP;
        Guid? retailerId = queryRequest.RetailerId;
        Guid? integratorId = queryRequest.IntegratorId;

        bool canViewAll = _currentUserInfo
            .HasPermission(TransactionIPTotalByOwnerClaim.All.Claim);

        bool canViewInternalData = _currentUserInfo
            .HasPermission(TransactionIPTotalByOwnerClaim.CanViewInternalData.Claim);

        bool canViewTeamData = _currentUserInfo
            .HasPermission(TransactionIPTotalByOwnerClaim.CanViewTeamData.Claim);

        bool canViewOwnData = _currentUserInfo
           .HasPermission(TransactionIPTotalByOwnerClaim.CanViewOwnData.Claim);

        bool viewOnlyOwn = !canViewAll && !canViewInternalData && !canViewTeamData;

        bool viewInternalData = !canViewAll && canViewInternalData && (ownerIsIP == true);

        List<Guid> teamMembersIds = [];

        if (canViewTeamData)
        {
            ApiResponse<GetEmployeeTeamDTOResponse> teamMembers = await _employeeEndpoint
                .GetTeamByEmpoyeeIdManager(
                ownerId,
                new GetEmployeeTeamDTOParams(null, 1, 1000));

            if (teamMembers.Content != null)
                teamMembersIds = [.. teamMembers.Content!.Data.Select(x => Guid.Parse(x.Value))];
        }
        queryAccount = queryAccount
            .Where(account => account.MovementDateHour >= startDate
                && account.MovementDateHour <= endDate)
            .WhereIf(customerId.HasValue, account => account.AccountIP.Customer.Id == customerId)
            .WhereIf(retailerId.HasValue, account => account.AccountIP.Retailer!.Id == retailerId)
            .WhereIf(integratorId.HasValue, account => account.AccountIP.Integrator!.Id == integratorId);

        if (viewOnlyOwn || canViewOwnData)
            queryAccount = queryAccount.Where(account => account.AccountIP.Owner != null
                && account.AccountIP.Owner.Id == ownerId);

        if (viewInternalData && canViewOwnData)
            queryAccount = queryAccount.Where(account => account.AccountIP.Owner != null
                && (account.AccountIP.Owner.OwnerIsIP == viewInternalData
                    || account.AccountIP.Owner.Id == ownerId));
        else if (viewInternalData)
            queryAccount = queryAccount.Where(account => account.AccountIP.Owner != null
                && account.AccountIP.Owner.OwnerIsIP == viewInternalData);

        if (canViewTeamData && canViewOwnData)
            queryAccount = queryAccount.Where(account => account.AccountIP.Owner != null
                && (account.AccountIP.Owner.Id == ownerId
                    || teamMembersIds.Contains(account.AccountIP.Owner.Id!.Value)))
            .WhereIf(ownerIdFilter.HasValue, account => account.AccountIP!.Owner!.Id == ownerIdFilter);
        else if (canViewTeamData)
            queryAccount = queryAccount.Where(account => account.AccountIP.Owner != null
                && teamMembersIds.Contains(account.AccountIP.Owner.Id!.Value))
            .WhereIf(ownerIdFilter.HasValue, account => account.AccountIP!.Owner!.Id == ownerIdFilter);

        if (canViewAll)
            queryAccount = queryAccount
            .WhereIf(ownerIdFilter.HasValue, account => account.AccountIP!.Owner!.Id == ownerIdFilter);

        return queryAccount;
    }
}