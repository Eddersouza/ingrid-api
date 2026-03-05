namespace IP.AccIPInfo.Api.IntegratorSystems.ActiveSystem;

internal sealed class ActiveSystemCommandHandler(IAccIPUoW _unitOfWork) :
    ICommandHandler<ActiveSystemCommand, ActiveSystemResponse>
{
    private readonly IIntegratorSystemRepository _integratorSystemRepository =
        _unitOfWork.GetRepository<IIntegratorSystemRepository>();

    public async Task<Result<ActiveSystemResponse>> Handle(
        ActiveSystemCommand command,
        CancellationToken cancellationToken)
    {
        IntegratorSystemId integratorSystemId = new(command.Id);

        IntegratorSystem? integratorSystem = await _integratorSystemRepository.Data()
            .AsNoTracking()
            .FirstOrDefaultAsync(
            x => x.Id == integratorSystemId,
            cancellationToken);

        if (integratorSystem is null) return IntegratorSystemErrors.IntegratorSystemNotFound;

        bool isActived = command.Request.Active;
        string reason = command.Request.Reason;

        if (isActived && integratorSystem.ActivableInfo.Active)
            return IntegratorSystemErrors.AlreadyActiveStatus(isActived);

        if (isActived) integratorSystem.ActivableInfo.SetAsActive();
        else integratorSystem.ActivableInfo.SetAsDeactive(reason);

        _integratorSystemRepository.Update(integratorSystem);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var actionText = isActived ? "ativado" : "desativado";

        var response = new ActiveSystemResponse(
            integratorSystem.Id.Value!,
            integratorSystem.Name.Value!,
            integratorSystem.ActivableInfo.Active,
            $"Registro de Sistema {actionText} com sucesso!");

        return Result.Success(response);
    }
}