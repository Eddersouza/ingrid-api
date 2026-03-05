namespace IP.IDI.Api.Users.Delete;

internal sealed class DeleteUserCommandHandler(IIDIUnitOfWork _unitOfWork) :
    ICommandHandler<DeleteUserCommand, DeleteUserResponse>
{
    private readonly IAppUserRepository _userRepository =
        _unitOfWork.GetRepository<IAppUserRepository>();

    public async Task<Result<DeleteUserResponse>> Handle(
        DeleteUserCommand command,
        CancellationToken cancellationToken)
    {
        AppUser? user = await _userRepository.Data()
            .AsNoTracking()
            .FirstOrDefaultAsync(role => role.Id == command.Id,
            cancellationToken);

        if (user is null) return UserErrors.NotFound;

        user.DeletableInfo.SetReason(command.Request.Reason);

        _userRepository.Delete(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new DeleteUserResponse(
            $"Registro de Usuário [{user}] excluído com sucesso!");

        return Result.Success(response);
    }
}
