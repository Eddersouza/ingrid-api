namespace IP.Core.Api.States.DeleteState;

internal sealed class DeleteStateCommandHandler(ICoreUoW _unitOfWork) :
    ICommandHandler<DeleteStateCommand, DeleteStateResponse>
{
    private readonly IStateRepository _stateRepository =
        _unitOfWork.GetRepository<IStateRepository>();

    public async Task<Result<DeleteStateResponse>> Handle(
        DeleteStateCommand command,
        CancellationToken cancellationToken)
    {
        StateId stateId = new(command.Id);

        State? state = await _stateRepository.Data()
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == stateId,
            cancellationToken);

        if (state is null) return StateErrors.StateNotFound;

        state.DeletableInfo.SetReason(command.Request.Reason);

        _stateRepository.Delete(state);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new DeleteStateResponse(
            $"Registro de Estado [{state.Name.Value}] excluído com sucesso!");

        return Result.Success(response);
    }
}
