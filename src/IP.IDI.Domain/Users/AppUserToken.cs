namespace IP.IDI.Domain.Users;

public class AppUserToken : IdentityUserToken<Guid>
{
    public const int NAME_MAX_LENGTH = 128;
    public const int LOGIN_PROVIDER_MAX_LENGTH = 128;
}