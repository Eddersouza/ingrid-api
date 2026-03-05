namespace IP.IDI.Api.AppGuides.MarkAsReadByAllUsers;

internal sealed class MarkAsReadByAllUsersCommandHandler(IIDIUnitOfWork _unitOfWork) :
    ICommandHandler<MarkAsReadByAllUsersCommand, MarkAsReadByAllUsersResponse>
{
    private readonly IAppGuidesRepository _appGuideRepository =
        _unitOfWork.GetRepository<IAppGuidesRepository>();

    public async Task<Result<MarkAsReadByAllUsersResponse>> Handle(
        MarkAsReadByAllUsersCommand command,
        CancellationToken cancellationToken)
    {
        AppGuide? currentAppGuide =
            await _appGuideRepository.Data()
            .Include(x => x.Users)
            .ThenInclude(x => x.User)
            .FirstOrDefaultAsync(guid => guid.LinkId == command.Id, cancellationToken);

        if (currentAppGuide is null) return AppGuideErrors.NotFound;

        currentAppGuide.SetAllViewWithFalse();

        _appGuideRepository.Update(currentAppGuide);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        MarkAsReadByAllUsersResponse response = new(
            $"Registro de Vizualização alterado com sucesso!");

        return Result.Success(response);
    }
}