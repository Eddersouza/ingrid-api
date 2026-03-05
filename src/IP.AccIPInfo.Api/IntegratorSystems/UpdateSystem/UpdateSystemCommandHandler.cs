namespace IP.AccIPInfo.Api.IntegratorSystems.UpdateSystem;

internal sealed class UpdateSystemCommandHandler(IAccIPUoW _unitOfWork) :
    ICommandHandler<UpdateSystemCommand, UpdateSystemResponse>
{
    private readonly IIntegratorSystemRepository _integratorSystemRepository =
        _unitOfWork.GetRepository<IIntegratorSystemRepository>();

    public async Task<Result<UpdateSystemResponse>> Handle(
        UpdateSystemCommand command,
        CancellationToken cancellationToken)
    {
        IntegratorSystemId integratorSystemId = new(command.Id);

        IntegratorSystem? integratorSystem = await _integratorSystemRepository.Data()
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == integratorSystemId,
            cancellationToken);

        if (integratorSystem is null) return IntegratorSystemErrors.IntegratorSystemNotFound;

        integratorSystem.Update(
             command.Request.Name,
             command.Request.SiteUrl,
             command.Request.Description
             );

        _integratorSystemRepository.Update(integratorSystem);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new UpdateSystemResponse(
            integratorSystem.Id.Value!,
            integratorSystem.Name.Value!,
            integratorSystem.SiteUrl,
            integratorSystem.Description,
            $"Registro de Sistema [{integratorSystem.Name.Value}] alterado com sucesso!");

        return Result.Success(response);
    }
}