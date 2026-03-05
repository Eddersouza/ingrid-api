namespace IP.IDI.Api.Users.Authorize;

internal sealed class AuthorizeUserCommandHandler(
    ICurrentSessionProvider _currentSessionProvider,
    IIDIUnitOfWork _unitOfWork) :
    ICommandHandler<AuthorizeUserCommand, AuthorizeUserResponse>
{
    private readonly IAppUserRepository _userRepository =
        _unitOfWork.GetRepository<IAppUserRepository>();

    public async Task<Result<AuthorizeUserResponse>> Handle(
        AuthorizeUserCommand command,
        CancellationToken cancellationToken)
    {
        AppUser? user = await GetCurrentUser(
            _currentSessionProvider.CurrentUserInfo.Id,
            cancellationToken);

        if (user is null) return UserErrors.NotFound;

        AppRole? role = user.UserRoles.FirstOrDefault()?.Role;

        if (role is null) return UserErrors.RoleNotFound;

        IEnumerable<string> claims =
            role?.RoleClaims?.Select(x => x.ClaimValue!) ?? [];

        return new AuthorizeUserResponse(
            role!.Id,
            role.Name!,
            [.. claims],
            "Usuario tem permissões de acesso");
    }

    private async Task<AppUser?> GetCurrentUser(
        Guid id,
        CancellationToken cancellationToken) =>
        await _userRepository.Data()
            .AsNoTracking()
            .Include(x => x.UserRoles)
            .ThenInclude(x => x.Role)
            .ThenInclude(x => x.RoleClaims)
            .FirstOrDefaultAsync(x =>
                x.Id == id,
            cancellationToken);
}