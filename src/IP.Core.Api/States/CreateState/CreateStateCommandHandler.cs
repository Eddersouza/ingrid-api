namespace IP.Core.Api.States.CreateState;

internal class CreateStateCommandHandler(ICoreUoW _unitOfWork) :
ICommandHandler<CreateStateCommand, CreateStateResponse>
{
    private readonly IStateRepository _stateRepository =
        _unitOfWork.GetRepository<IStateRepository>();

    public async Task<Result<CreateStateResponse>> Handle(
        CreateStateCommand command,
        CancellationToken cancellationToken)
    {
        State? state = await _stateRepository.Data()
            .AsNoTracking()
            .FirstOrDefaultAsync(
                s => s.Name.Value == command.Request.Name,
                cancellationToken);

        if (state is not null) return StateErrors.StateAlreadyExists;

        state = State.Create(
            command.Request.IBGECode, 
            command.Request.Code,
            command.Request.Name);

        await _stateRepository.Create(state);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new CreateStateResponse(
            state.Id.Value,
            state.IBGECode,
            state.Code,
            state.Name.Value!,
            $"Registro de Estado [{state.Name.Value}] criado com sucesso!");

        return await Task.FromResult(Result.Success(response));
    }
}
