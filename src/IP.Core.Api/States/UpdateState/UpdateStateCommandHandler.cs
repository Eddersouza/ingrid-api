namespace IP.Core.Api.States.UpdateState;

internal sealed class UpdateStateCommandHandler(ICoreUoW _unitOfWork) :
    ICommandHandler<UpdateStateCommand, UpdateStateResponse>
{
    private readonly IStateRepository _stateRepository =
        _unitOfWork.GetRepository<IStateRepository>();

    public async Task<Result<UpdateStateResponse>> Handle(
        UpdateStateCommand command,
        CancellationToken cancellationToken)
    {
        StateId stateId = new(command.Id);

        State? state = await _stateRepository.Data()
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == stateId,
            cancellationToken);

        if (state is null) return StateErrors.StateNotFound;

        state.Update(
             command.Request.IBGECode,
             command.Request.Code,
             command.Request.Name);

        _stateRepository.Update(state);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new UpdateStateResponse(
            state.Id.Value!,
            state.IBGECode,
            state.Code,
            state.Name.Value!,
            $"Registro de Estado [{state.Name.Value}] alterado com sucesso!");

        return Result.Success(response);
    }
}

