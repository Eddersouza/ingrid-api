namespace IP.IDI.Domain.Users;

public class AppUserLogin : IdentityUserLogin<Guid>
{
    public const int LOGIN_PROVIDER_MAX_LENGTH = 128;
    public const int PROVIDER_KEY_MAX_LENGTH = 128;
}