namespace IP.IDI.Api.Users.GetUsers;

internal class GetUsersQueryHandler(
    IIDIUnitOfWork _unitOfWork) :
    IQueryHandler<GetUsersQuery, GetUsersResponse>
{
    private readonly IAppUserRepository _userRepository =
       _unitOfWork.GetRepository<IAppUserRepository>();

    public async Task<Result<GetUsersResponse>> Handle(
        GetUsersQuery request,
        CancellationToken cancellationToken)
    {
        Dictionary<string, Expression<Func<AppUser, object>>> sortDictionary = new()
        {
            { "active", x => x.ActivableInfo.Active },
            { "name", x => x.UserName! },
            { "email", x => x.Email! },
            { "role", x => x.UserRoles!.First().Role!.Name! }
        };

        GetUsersQueryRequest queryRequest = request.Request;

        int pageNumber = queryRequest.PageNumber ?? 1;
        int pageSize = queryRequest.PageSize ?? 10;

        IQueryable<AppUser> queryUsers = _userRepository.Data()
            .Include(x => x.UserRoles)
            .ThenInclude(x => x.Role)
            .AsNoTracking();

        IQueryable<UserResponseData> users = ApplyUserFilters(queryRequest, queryUsers)
            .OrderBy(sortDictionary, request.Request.OrderBy, ["active:desc", "name"])
            .Paginate(pageNumber, pageSize)
            .Select(x => new UserResponseData(
                x.Id,
                x.UserName!,
                x.Email!,
                new UserRoleResponseData(x.UserRoles!.First().Role!.Id, x.UserRoles!.First().Role!.Name!),
                x.ActivableInfo.Active,
                x.AuditableInfo.CreatedDate.ToString("G"),
                x.EmailConfirmed));

        int count = await queryUsers.CountAsync(cancellationToken);

        GetUsersResponse response = new(
            users,
            new ResolvedDataPagination(pageNumber, pageSize, count));

        return Result.Success(response);
    }

    private static IQueryable<AppUser> ApplyUserFilters(
        GetUsersQueryRequest queryRequest,
        IQueryable<AppUser> queryUsers) =>
        queryUsers.WhereIf(queryRequest.UserContains.IsNotNullOrWhiteSpace(),
            query => query.UserName!.Contains(queryRequest.UserContains!))
        .WhereIf(queryRequest.EmailContains.IsNotNullOrWhiteSpace(),
            query => query.Email!.Contains(queryRequest.EmailContains!))
        .WhereIf(queryRequest.RoleIs.HasValue, 
            query => query.UserRoles.FirstOrDefault()!.RoleId == queryRequest.RoleIs!.Value);
}