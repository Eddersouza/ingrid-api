namespace IP.IDI.Api.AppGuides.Get;

internal sealed class GetAppGuidesQueryHandler(
    IIDIUnitOfWork _unitOfWork) :
    IQueryHandler<GetAppGuidesQuery, GetAppGuidesResponse>
{
    private readonly IAppGuidesRepository _appGuideRepository =
       _unitOfWork.GetRepository<IAppGuidesRepository>();

    private readonly IAppUserRepository _userRepository =
        _unitOfWork.GetRepository<IAppUserRepository>();

    public async Task<Result<GetAppGuidesResponse>> Handle(
        GetAppGuidesQuery request,
        CancellationToken cancellationToken)
    {
        Dictionary<string, Expression<Func<AppGuide, object>>> sortDictionary = new()
        {
            { "active", x => x.ActivableInfo.Active },
            { "name", x => x.Name },
            { "linkId", x => x.LinkId }
        };

        GetAppGuidesQueryRequest queryRequest = request.Request;

        int pageNumber = queryRequest.PageNumber ?? 1;
        int pageSize = queryRequest.PageSize ?? 10;

        List<Guid> usersActive = await _userRepository.Data()
            .AsNoTracking()
            .Where(x => x.ActivableInfo.Active &&
                !x.DeletableInfo.Deleted)
            .Select(x => x.Id)
            .ToListAsync(cancellationToken);

        IQueryable<AppGuide> query =
            _appGuideRepository.Data().Include(x => x.Users).AsNoTracking();

        IQueryable<GetAppGuidesResponseData> usersViewed =
            ApplyUserFilters(queryRequest, query)
            .OrderBy(sortDictionary, request.Request.OrderBy, ["active:desc", "name"])
            .Paginate(pageNumber, pageSize)
            .Select(x => new GetAppGuidesResponseData(
                x.Id.Value,
                x.Name,
                x.LinkId,
                x.Users.Where(x => x.ActivableInfo.Active && 
                    usersActive.Contains(x.User!.Id))
                .Select(x => x.Id).Distinct().Count(),
                usersActive.Count,
                x.ActivableInfo.Active));

        int count = await query.CountAsync(cancellationToken);

        GetAppGuidesResponse response = new(
            usersViewed,
            new ResolvedDataPagination(pageNumber, pageSize, count));

        return Result.Success(response);
    }

    private static IQueryable<AppGuide> ApplyUserFilters(
        GetAppGuidesQueryRequest queryRequest,
        IQueryable<AppGuide> query)
    {
        string nameContains = queryRequest.NameContains?.ToLower() ?? string.Empty;
        string linkIdContains = queryRequest.LinkIdContains?.ToLower() ?? string.Empty;

        return query.WhereIf(nameContains.IsNotNullOrWhiteSpace(),
            appGuide => appGuide.Name.ToLower().Contains(nameContains))
        .WhereIf(linkIdContains.IsNotNullOrWhiteSpace(),
            appGuide => appGuide.LinkId.ToLower().Contains(linkIdContains));
    }
}