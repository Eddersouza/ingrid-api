namespace IP.IDI.Api.Roles.GetSelectItems;

internal sealed class GetSelectItemsQueryHandler(
    IIDIUnitOfWork _unitOfWork) :
    IQueryHandler<GetSelectItemsRolesQuery, GetSelectItemsRolesResponse>
{
    private readonly IAppRoleRepository _roleRepository =
       _unitOfWork.GetRepository<IAppRoleRepository>();

    public async Task<Result<GetSelectItemsRolesResponse>> Handle(
        GetSelectItemsRolesQuery request,
        CancellationToken cancellationToken)
    {
        Dictionary<string, Expression<Func<AppRole, object>>> sortDictionary = new()
        {
            { "active", x => x.ActivableInfo.Active },
            { "name", x => x.Name! },
        };

        GetSelectItemsRolesQueryRequest queryRequest = request.Request;

        int pageNumber = queryRequest.PageNumber ?? 1;
        int pageSize = queryRequest.PageSize ?? 10;

        string searchTerm = queryRequest.SearchTerm ?? string.Empty;

        IQueryable<AppRole> queryRoles =
            ApplyUserFilters(searchTerm, _roleRepository.Data().AsNoTracking())
            .OrderBy(sortDictionary, queryRequest.OrderBy, ["name"]);

        IQueryable<ValueLabelItem> roles = queryRoles           
            .Paginate(pageNumber, pageSize)
            .Select(x => new ValueLabelItem(
                x.Id.ToString(),
                x.Name!,
                !x.ActivableInfo.Active));

        int count = await queryRoles.CountAsync(cancellationToken);

        GetSelectItemsRolesResponse response = new(
            roles,
            new ResolvedDataPagination(pageNumber, pageSize, count));

        return Result.Success(response);
    }

    private static IQueryable<AppRole> ApplyUserFilters(
        string searchTerm,
        IQueryable<AppRole> queryRoles) =>
        queryRoles.WhereIf(searchTerm.IsNotNullOrWhiteSpace(),
            query => query.Name!.ToLower().Contains(searchTerm.ToLower()));
}