namespace IP.IDI.Api.Users.Update;

internal class UpdateUserCommandHandler(
    IIDIUnitOfWork _unitOfWork) :
    ICommandHandler<UpdateUserCommand, UpdateUserResponse>
{
    private readonly IAppRoleRepository _roleRepository =
       _unitOfWork.GetRepository<IAppRoleRepository>();

    private readonly IAppUserRepository _userRepository =
            _unitOfWork.GetRepository<IAppUserRepository>();

    public async Task<Result<UpdateUserResponse>> Handle(
        UpdateUserCommand command,
        CancellationToken cancellationToken)
    {
        AppRole? role = await _roleRepository.Data()
         .AsNoTracking()
         .FirstOrDefaultAsync(role => role.Id == command.Request.RoleId,
         cancellationToken);

        if (role is null) return RoleErrors.NotFound;

        AppUser? user = await _userRepository.Data()
               .Include(user => user.UserRoles)
               .ThenInclude(userRole => userRole.Role)
               .FirstOrDefaultAsync(role => role.Id == command.Id,
               cancellationToken);

        if (user is null) return UserErrors.NotFound;

        Guid newRoleId = command.Request.RoleId!.Value;
        Guid oldRoleId = user.UserRoles.First().RoleId;

        if (newRoleId == oldRoleId) return UserErrors.AlreadyHasThisProfile;

        user.UserRoles.Clear();
        user.UserRoles.Add(AppUserRole.Create(user.Id, newRoleId));

        _userRepository.Update(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        UpdateUserResponse response = UpdateResponse(user, role);

        return await Task.FromResult(Result.Success(response));
    }

    private static UpdateUserResponse UpdateResponse(AppUser user, AppRole role)
    {
        UserResponseData data = new(
                user!.Id,
                user.UserName!,
                user.Email!,
                new UserRoleResponseData(
                    role.Id,
                    role.Name!),
                user.ActivableInfo.Active,
                user.AuditableInfo.CreatedDate.ToString("G"),
                user.EmailConfirmed);

        UpdateUserResponse response =
           new(data,
           $"Registro de Usuário [{user}] alterado com sucesso!");
        return response;
    }  
}