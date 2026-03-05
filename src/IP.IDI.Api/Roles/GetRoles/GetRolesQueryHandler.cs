using System.Linq.Expressions;

namespace IP.IDI.Api.Roles.GetRoles;

internal sealed class GetRolesQueryHandler(
    IIDIUnitOfWork _unitOfWork) :
    IQueryHandler<GetRolesQuery, GetRolesResponse>
{
    private readonly IAppRoleRepository _roleRepository =
       _unitOfWork.GetRepository<IAppRoleRepository>();

    public async Task<Result<GetRolesResponse>> Handle(
        GetRolesQuery request,
        CancellationToken cancellationToken)
    {
        Dictionary<string, Expression<Func<AppRole, object>>> sortDictionary = new()
        {
            { "active", x => x.ActivableInfo.Active },
            { "name", x => x.Name! },
        };

        GetRolesQueryRequest queryRequest = request.Request;

        int pageNumber = queryRequest.PageNumber ?? 1;
        int pageSize = queryRequest.PageSize ?? 10;

        IQueryable<AppRole> queryRoles =
            _roleRepository.Data().AsNoTracking();

        IQueryable<GetRolesResponseData> users =
            ApplyUserFilters(queryRequest, queryRoles)
            .OrderBy(sortDictionary, request.Request.OrderBy, ["active:desc", "name"])
            .Paginate(pageNumber, pageSize)
            .Select(x => new GetRolesResponseData(
                x.Id,
                x.Name!,
                x.ActivableInfo.Active));

        int count = await queryRoles.CountAsync(cancellationToken);

        GetRolesResponse response = new(
            users,
            new ResolvedDataPagination(pageNumber, pageSize, count));

        return Result.Success(response);
    }

    private static IQueryable<AppRole> ApplyUserFilters(
        GetRolesQueryRequest queryRequest,
        IQueryable<AppRole> queryRoles) =>
        queryRoles.WhereIf(queryRequest.NameContains.IsNotNullOrWhiteSpace(),
            query => query.Name!.Contains(queryRequest.NameContains!));
}