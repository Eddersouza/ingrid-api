using System.Web;

namespace IP.IDI.Api.Users.CreateUser;

internal class CreateUserCommandHandler(
    IIDIUnitOfWork _unitOfWork, 
    IPasswordHasher _passwordHasher) :
    ICommandHandler<CreateUserCommand, CreateUserResponse>
{
    private readonly IAppUserRepository _userRepository =
        _unitOfWork.GetRepository<IAppUserRepository>();

    private readonly IAppRoleRepository _roleRepository =
       _unitOfWork.GetRepository<IAppRoleRepository>();

    public async Task<Result<CreateUserResponse>> Handle(
        CreateUserCommand command,
        CancellationToken cancellationToken)
    {
        AppRole? role = await _roleRepository.Data()
         .AsNoTracking()
         .FirstOrDefaultAsync(role => role.Id == command.Request.RoleId,
         cancellationToken);

        if (role is null) return RoleErrors.NotFound;

        AppUser user = await CreateUser(command, role, cancellationToken);
        CreateUserResponse response = CreateResponse(user, role);

        return await Task.FromResult(Result.Success(response));
    }

    private static CreateUserResponse CreateResponse(AppUser user, AppRole role)
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

        CreateUserResponse response =
           new(data,
           $"Registro de Usuário [{user}] criado com sucesso!");
        return response;
    }

    private async Task<AppUser> CreateUser(
        CreateUserCommand command,
        AppRole role,
        CancellationToken cancellationToken)
    {       
        AppUser user = AppUser.Create(
            command.Request.User, 
            command.Request.Email, 
            _passwordHasher.CreateSecurePasswordEncripted());
        user.UserRoles.Add(
            AppUserRole.Create(user.Id, command.Request.RoleId!.Value));

        var token = HttpUtility.UrlEncode(_passwordHasher.Hash($"{user.UserName}{user.Email}{user.PasswordHash}"));

        user.EventsInfo.Raise(
            new CreatedUserDomainEvent(
                user.Id, 
                user.UserName!, 
                user.Email!, 
                role.Id, 
                role.Name!,
                token));

        await _userRepository.Create(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return user;
    }
}