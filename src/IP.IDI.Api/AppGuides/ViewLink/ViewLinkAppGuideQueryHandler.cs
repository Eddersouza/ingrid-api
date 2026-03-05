namespace IP.IDI.Api.AppGuides.ViewLink;

internal class ViewLinkAppGuideQueryHandler(
    IIDIUnitOfWork _unitOfWork,
    ICurrentUserInfo _currentUserInfo) :
    IQueryHandler<ViewLinkAppGuideQuery, ViewLinkAppGuideResponse>
{
    private readonly IAppGuidesRepository _appGuideRepository =
        _unitOfWork.GetRepository<IAppGuidesRepository>();

    public async Task<Result<ViewLinkAppGuideResponse>> Handle(
        ViewLinkAppGuideQuery query,
        CancellationToken cancellationToken)
    {
        AppGuide? currentAppGuide =
            await _appGuideRepository.Data()
            .Include(guid => guid.Users)
            .FirstOrDefaultAsync(guid =>
                guid.LinkId == query.LinkId &&
                    guid.ActivableInfo.Active,
                cancellationToken);

        if (currentAppGuide is null) return AppGuideErrors.NotFound;

        var viewed = currentAppGuide.WasViewedByUser(_currentUserInfo.Id);

        if (!viewed)
        {
            currentAppGuide.Users.Add(
                AppGuideViewed.Create(_currentUserInfo.Id));

            _appGuideRepository.Update(currentAppGuide);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        var responseData = new ViewLinkAppGuideResponse(viewed);

        return Result.Success(responseData);
    }
}