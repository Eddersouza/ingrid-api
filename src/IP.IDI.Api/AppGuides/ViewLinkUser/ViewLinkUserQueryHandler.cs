namespace IP.IDI.Api.AppGuides.ViewLinkUser;

internal class ViewLinkUserQueryHandler(
    IIDIUnitOfWork _unitOfWork) :
    IQueryHandler<ViewLinkUserQuery, ViewLinkUserResponse>
{
    private readonly IAppGuidesRepository _appGuideRepository =
       _unitOfWork.GetRepository<IAppGuidesRepository>();

    private readonly IAppUserRepository _userRepository =
        _unitOfWork.GetRepository<IAppUserRepository>();

    public async Task<Result<ViewLinkUserResponse>> Handle(
        ViewLinkUserQuery request,
        CancellationToken cancellationToken)
    {
        Dictionary<string, Expression<Func<AppGuide, object>>> sortDictionary = new()
        {
            { "active", x => x.ActivableInfo.Active },
            { "name", x => x.Name },
            { "linkId", x => x.LinkId }
        };

        ViewLinkUserQueryRequest queryRequest = request.Request;

        int pageNumber = queryRequest.PageNumber ?? 1;
        int pageSize = queryRequest.PageSize ?? 10;

        int totalUsers = await _userRepository.Data()
            .CountAsync(x => x.ActivableInfo.Active &&
                !x.DeletableInfo.Deleted, cancellationToken);

        IQueryable<AppGuide> query =
            _appGuideRepository.Data().Include(x => x.Users).AsNoTracking();

        List<ViewLinkUserResponseData> usersViewed =
            await query
            .Where(x => x.LinkId == request.LinkId)
            .SelectMany(x => x.Users)
            .OrderBy(x => x.User!.Email)
            .ThenByDescending(x => x.User!.AuditableInfo.CreatedDate)
            .GroupBy(x => new { x.User!.Id, x.User.Email, x.User.UserName })
            .Select(x => new ViewLinkUserResponseData(
                x.Key.Id,
                x.Key.UserName!,
                x.Key.Email!,
                x.Select(x => x.AuditableInfo.CreatedDate.ToString("G")).Take(4),
                x.Any(x => x.ActivableInfo.Active)
            )).Paginate(pageNumber, pageSize)
            .ToListAsync(cancellationToken);

        int count = await query.CountAsync(cancellationToken);

        ViewLinkUserResponse response = new(
            usersViewed,
            new ResolvedDataPagination(pageNumber, pageSize, count));

        return Result.Success(response);
    }
}