using System.Text.RegularExpressions;

namespace IP.IDI.Api.AppGuides.Update;

internal sealed class UpdateAppGuideCommandHandlerr(IIDIUnitOfWork _unitOfWork) :
    ICommandHandler<UpdateAppGuideCommand, UpdateAppGuideResponse>
{
    private readonly IAppGuidesRepository _appGuideRepository =
        _unitOfWork.GetRepository<IAppGuidesRepository>();

    private readonly IAppUserRepository _userRepository =
        _unitOfWork.GetRepository<IAppUserRepository>();

    public async Task<Result<UpdateAppGuideResponse>> Handle(
        UpdateAppGuideCommand command,
        CancellationToken cancellationToken)
    {
        AppGuideId appGuideId = AppGuideId.Create(command.Id);

        AppGuide? currentAppGuide =
            await _appGuideRepository.Data()
            .AsNoTracking()
            .FirstOrDefaultAsync(guid => guid.Id == appGuideId, cancellationToken);

        if (currentAppGuide is null) return AppGuideErrors.NotFound;

        string name = command.Request.Name!;
        string linkId = command.Request.LinkId!;

        AppGuide? guide = await _appGuideRepository.Data()
           .AsNoTracking()
           .FirstOrDefaultAsync(role => role.Id != appGuideId && 
                (role.Name == name || role.LinkId == linkId),
           cancellationToken);

        if (guide is not null) return AppGuideErrors.AlreadyExists;

        string regexOnlyAphanumericAndUnderscoreAndDash = "^[A-Za-z0-9_-]+$";
        bool isMatch = Regex.IsMatch(linkId, regexOnlyAphanumericAndUnderscoreAndDash);

        if (!isMatch) return AppGuideErrors.InvalidLinkIdPattern;

        currentAppGuide.Update(name, linkId);

        _appGuideRepository.Update(currentAppGuide);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        int totalUsers = await _userRepository.Data()
            .CountAsync(x => x.ActivableInfo.Active &&
                !x.DeletableInfo.Deleted, cancellationToken);

        UpdateAppGuideResponseData responseData = new(
            currentAppGuide.Id.Value,
            currentAppGuide.Name!,
            currentAppGuide.LinkId,
            0,
            totalUsers,
            currentAppGuide.ActivableInfo.Active);

        UpdateAppGuideResponse response = new(responseData,
            $"Registro de Guia do Sistema [{currentAppGuide}] alterado com sucesso!");

        return await Task.FromResult(Result.Success(response));
    }
}