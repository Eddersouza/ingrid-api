namespace IP.IDI.Api.Users.GetUser;

internal sealed class GetUserQueryHandler(
    IIDIUnitOfWork _unitOfWork,
    ICurrentUserInfo _currentUser) :
    IQueryHandler<GetUserQuery, GetUserResponse>
{
    private readonly IAppUserRepository _userRepository =
       _unitOfWork.GetRepository<IAppUserRepository>();

    public async Task<Result<GetUserResponse>> Handle(
        GetUserQuery query,
        CancellationToken cancellationToken)
    {
        bool hasPermission = _currentUser.HasPermission(UserClaim.CanRead.Claim) ||
            _currentUser.Permissions.Contains(UserClaim.All.Claim) ||
            _currentUser.Id == query.Id;

        if (!hasPermission) return UserErrors.ForbiddenAccess;

        AppUser? currentUser = await _userRepository.Data()
            .Include(x => x.UserRoles)
            .ThenInclude(x => x.Role)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == query.Id, cancellationToken);

        GetUserResponse response = new(new UserResponseData(
                currentUser!.Id,
                currentUser.UserName!,
                currentUser.Email!,
                new UserRoleResponseData(
                    currentUser.UserRoles!.First().Role!.Id,
                    currentUser.UserRoles!.First().Role!.Name!),
                currentUser.ActivableInfo.Active,
                currentUser.AuditableInfo.CreatedDate.ToString("G"),
                currentUser.EmailConfirmed));

        return Result.Success(response);
    }
}