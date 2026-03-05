namespace IP.AccIPInfo.Api.IntegratorSystems.DeleteSystem;

internal sealed class DeleteSystemCommandHandler(IAccIPUoW _unitOfWork) :
    ICommandHandler<DeleteSystemCommand, DeleteSystemResponse>
{
    private readonly IIntegratorSystemRepository _integratorSystemRepository =
        _unitOfWork.GetRepository<IIntegratorSystemRepository>();

    public async Task<Result<DeleteSystemResponse>> Handle(
       DeleteSystemCommand command,
       CancellationToken cancellationToken)
    {
        IntegratorSystemId systemId = new(command.Id);

        IntegratorSystem? integratorSystem = await _integratorSystemRepository.Data()
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == systemId,
            cancellationToken);

        if (integratorSystem is null) return IntegratorSystemErrors.IntegratorSystemNotFound;

        integratorSystem.DeletableInfo.SetReason(command.Request.Reason);

        _integratorSystemRepository.Delete(integratorSystem);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new DeleteSystemResponse(
            $"Registro de Sistema [{integratorSystem.Name.Value}] excluído com sucesso!");

        return Result.Success(response);
    }
}