namespace IP.IDI.Api.AppGuides.MarkAsReadByUser;

internal sealed class MarkAsReadByUserCommandHandler(IIDIUnitOfWork _unitOfWork) :
    ICommandHandler<MarkAsReadByUserCommand, MarkAsReadByUserResponse>
{
    private readonly IAppGuidesRepository _appGuideRepository =
        _unitOfWork.GetRepository<IAppGuidesRepository>();

    private readonly IAppUserRepository _userRepository =
        _unitOfWork.GetRepository<IAppUserRepository>();

    public async Task<Result<MarkAsReadByUserResponse>> Handle(
        MarkAsReadByUserCommand command,
        CancellationToken cancellationToken)
    {
        AppGuide? currentAppGuide =
            await _appGuideRepository.Data()
            .Include(x => x.Users)
            .ThenInclude(x => x.User)
            .FirstOrDefaultAsync(guid => guid.LinkId == command.Id, cancellationToken);

        if (currentAppGuide is null) return AppGuideErrors.NotFound;

        bool viewedUser = currentAppGuide.ViewedUser(command.Request.UserId);

        if (!viewedUser) return AppGuideErrors.UserNotViewed;

        currentAppGuide.MarkUserViewAsInactive(command.Request.UserId);

        _appGuideRepository.Update(currentAppGuide);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        int totalUsers = await _userRepository.Data()
            .CountAsync(x => x.ActivableInfo.Active &&
                !x.DeletableInfo.Deleted, cancellationToken);

        AppUser? currentUser = currentAppGuide.GetUser(command.Request.UserId);

        IEnumerable<DateTime> dates = currentAppGuide.GetDatesByUser(command.Request.UserId);

        MarkAsReadByUserResponse response = new(
            $"Registro de Vizualização de [{currentAppGuide}] pelo o Usuário alterado com sucesso!");

        return Result.Success(response);
    }
}