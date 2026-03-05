namespace IP.IDI.Api.AppGuides.GetById;

internal sealed class GetAppGuideQueryHandler(
    IIDIUnitOfWork _unitOfWork) :
    IQueryHandler<GetAppGuideQuery, GetAppGuideResponse>
{
    private readonly IAppGuidesRepository _appGuideRepository =
        _unitOfWork.GetRepository<IAppGuidesRepository>();

    private readonly IAppUserRepository _userRepository =
      _unitOfWork.GetRepository<IAppUserRepository>();

    public async Task<Result<GetAppGuideResponse>> Handle(
        GetAppGuideQuery query,
        CancellationToken cancellationToken)
    {
        AppGuideId appGuideId = AppGuideId.Create(query.Id);

        AppGuide? currentAppGuide =
            await _appGuideRepository.Data()
            .Include(x => x.Users)
            .ThenInclude(x => x.User)
            .AsNoTracking()
            .FirstOrDefaultAsync(guid => guid.Id == appGuideId, cancellationToken);

        if (currentAppGuide is null) return AppGuideErrors.NotFound;

        List<Guid> usersActive = await _userRepository.Data()
           .AsNoTracking()
           .Where(x => x.ActivableInfo.Active &&
               !x.DeletableInfo.Deleted)
           .Select(x => x.Id)
           .ToListAsync(cancellationToken);

        GetAppGuideResponseData responseData = new(
            currentAppGuide.Id.Value,
            currentAppGuide.Name,
            currentAppGuide.LinkId,
            currentAppGuide.Users
                .Where(x => x.ActivableInfo.Active &&
                    usersActive.Contains(x.User!.Id))
                .Select(x => x.Id).Distinct().Count(),
            usersActive.Count,
            currentAppGuide.ActivableInfo.Active);

        return Result.Success(new GetAppGuideResponse(responseData));
    }
}