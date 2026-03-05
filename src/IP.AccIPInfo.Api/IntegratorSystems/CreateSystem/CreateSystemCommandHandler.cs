namespace IP.AccIPInfo.Api.IntegratorSystems.CreateSystem;

internal class CreateSystemCommandHandler(IAccIPUoW _unitOfWork) :
    ICommandHandler<CreateSystemCommand, CreateSystemResponse>
{
    private readonly IIntegratorSystemRepository _integratorSystemRepository =
        _unitOfWork.GetRepository<IIntegratorSystemRepository>();

    public async Task<Result<CreateSystemResponse>> Handle(
        CreateSystemCommand command,
        CancellationToken cancellationToken)
    {
        IntegratorSystem? integratorSystem = await _integratorSystemRepository.Data()
            .AsNoTracking()
            .FirstOrDefaultAsync(
                s => s.Name.Value == command.Request.Name,
                cancellationToken);

        if (integratorSystem is not null)
            return IntegratorSystemErrors.IntegratorSystemAlreadyExists;

        integratorSystem = IntegratorSystem.Create(
            command.Request.Name,
            command.Request.SiteUrl,
            command.Request.Description
            );

        await _integratorSystemRepository.Create(integratorSystem);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new CreateSystemResponse(
            integratorSystem.Id.Value,
            integratorSystem.Name.Value!,
            integratorSystem.SiteUrl,
            integratorSystem.Description,
            $"Registro de Sistema Integrador [{integratorSystem.Name.Value}] criado com sucesso!",
            integratorSystem.ActivableInfo.Active);

        return await Task.FromResult(Result.Success(response));
    }
}