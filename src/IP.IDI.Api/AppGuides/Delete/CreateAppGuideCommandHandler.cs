namespace IP.IDI.Api.AppGuides.Delete;

internal sealed class CreateAppGuideCommandHandler(IIDIUnitOfWork _unitOfWork) :
    ICommandHandler<DeleteAppGuideCommand, DeleteAppGuideResponse>
{
    private readonly IAppGuidesRepository _appGuideRepository =
        _unitOfWork.GetRepository<IAppGuidesRepository>();

    public async Task<Result<DeleteAppGuideResponse>> Handle(
        DeleteAppGuideCommand command,
        CancellationToken cancellationToken)
    {
        AppGuideId appGuideId = AppGuideId.Create(command.Id);

        AppGuide? guide = await _appGuideRepository.Data()
            .SingleOrDefaultAsync(guid => guid.Id == appGuideId,
            cancellationToken);

        if (guide is null) return AppGuideErrors.NotFound;

        _appGuideRepository.Delete(guide);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        DeleteAppGuideResponse response = new(
            $"Registro de {guide.ToEntityName()} [{guide}] excluído com sucesso!");

        return Result.Success(response);
    }
}