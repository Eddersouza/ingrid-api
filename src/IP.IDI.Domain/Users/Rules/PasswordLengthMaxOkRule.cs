namespace IP.IDI.Domain.Users.Rules;

public class PasswordLengthMaxOkRule : IBusinessRule<string>
{
    public bool IsSatisfiedBy(string model) =>
        model.Length < AppUser.PASSWORD_MAX_LENGTH;
}
