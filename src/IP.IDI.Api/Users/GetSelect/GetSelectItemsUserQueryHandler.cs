namespace IP.IDI.Api.Users.GetSelect;

internal sealed class GetSelectItemsUserQueryHandler(
    IIDIUnitOfWork _unitOfWork) :
    IQueryHandler<GetSelectItemsUserQuery, GetSelectItemsUserResponse>
{
    private readonly IAppUserRepository _userRepository =
       _unitOfWork.GetRepository<IAppUserRepository>();

    public async Task<Result<GetSelectItemsUserResponse>> Handle(
        GetSelectItemsUserQuery request,
        CancellationToken cancellationToken)
    {
        Dictionary<string, Expression<Func<AppUser, object>>> sortDictionary = new()
        {
            { "active", x => x.ActivableInfo.Active },
            { "name", x => x.UserName! },
        };

        GetSelectItemsUserQueryRequest queryRequest = request.Request;

        int pageNumber = queryRequest.PageNumber ?? 1;
        int pageSize = queryRequest.PageSize ?? 10;

        string searchTerm = queryRequest.SearchTerm ?? string.Empty;

        IQueryable<AppUser> queryRoles =
            ApplyUserFilters(searchTerm, _userRepository.Entities.AsNoTracking())
            .OrderBy(sortDictionary, queryRequest.OrderBy, ["name"]);

        IQueryable<ValueLabelItem> roles = queryRoles
            .Paginate(pageNumber, pageSize)
            .Select(x => new ValueLabelItem(
                x.Id.ToString(),
                $"{x.UserName} - {x.Email}",
                !x.ActivableInfo.Active));

        int count = await queryRoles.CountAsync(cancellationToken);

        GetSelectItemsUserResponse response = new(
            roles,
            new ResolvedDataPagination(pageNumber, pageSize, count));

        return Result.Success(response);
    }

    private static IQueryable<AppUser> ApplyUserFilters(
        string searchTerm,
        IQueryable<AppUser> queryRoles) =>
        queryRoles.WhereIf(searchTerm.IsNotNullOrWhiteSpace(),
            query => query.UserName!.ToLower().Contains(searchTerm.ToLower()) ||
            query.Email!.ToLower().Contains(searchTerm.ToLower()));
}