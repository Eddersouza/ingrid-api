namespace IP.Core.Api.States.GetState;

internal sealed class GetStateQueryHandler(
    ICoreUoW _unitOfWork) :
    IQueryHandler<GetStateQuery, GetStateResponse>
{
    private readonly IStateRepository _stateRepository =
        _unitOfWork.GetRepository<IStateRepository>();

    public async Task<Result<GetStateResponse>> Handle(
        GetStateQuery query,
        CancellationToken cancellationToken)
    {
        StateId id = new(query.Id);
        State? currentState =
            await _stateRepository.Data()
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        
        if (currentState is null) return StateErrors.StateNotFound;

        GetStateResponse response = new(
            currentState.Id.Value!,
            currentState.IBGECode!,
            currentState.Code!,
            currentState.Name.Value!);

        return Result.Success(response);
    }
}