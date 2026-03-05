namespace IP.IDI.Api.AppGuides.ToggleActive;

internal sealed class ToggleActiveAppGuideCommandHandler(IIDIUnitOfWork _unitOfWork) :
    ICommandHandler<ToggleActiveAppGuideCommand, ToggleActiveAppGuideResponse>
{
    private readonly IAppGuidesRepository _appGuideRepository =
        _unitOfWork.GetRepository<IAppGuidesRepository>();

    private readonly IAppUserRepository _userRepository =
        _unitOfWork.GetRepository<IAppUserRepository>();

    public async Task<Result<ToggleActiveAppGuideResponse>> Handle(
        ToggleActiveAppGuideCommand command,
        CancellationToken cancellationToken)
    {
        AppGuideId appGuideId = AppGuideId.Create(command.Id);

        AppGuide? guide = await _appGuideRepository.Data()
            .SingleOrDefaultAsync(guid => guid.Id == appGuideId,
            cancellationToken);

        if (guide is null) return AppGuideErrors.NotFound;

        bool isActived = command.Request.Active;

        if (isActived && guide.ActivableInfo.Active)
            return AppGuideErrors.AlreadyActiveStatus(isActived);

        if (isActived) guide.ActivableInfo.SetAsActive();
        else guide.ActivableInfo.SetAsDeactive();

        _appGuideRepository.Update(guide);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        int totalUsers = await _userRepository.Data()
           .CountAsync(x => x.ActivableInfo.Active &&
               !x.DeletableInfo.Deleted, cancellationToken);

        string activeDeactive = isActived ? "ativado" : "desativado";

        string message = $"Registro de {guide.ToEntityName()} [{guide}] {activeDeactive} com sucesso!";

        var responseData = new ToggleActiveAppGuideResponseData(
            guide.Id.Value,
            guide.Name!,
            guide.LinkId,
            guide.Users.Select(x => x.Id).Distinct().Count(),
            totalUsers,
            guide.ActivableInfo.Active);

        var response = new ToggleActiveAppGuideResponse(responseData, message);

        return Result.Success(response);
    }
}