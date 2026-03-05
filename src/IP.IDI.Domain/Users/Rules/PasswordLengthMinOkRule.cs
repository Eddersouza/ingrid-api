namespace IP.IDI.Domain.Users.Rules;

public class PasswordLengthMinOkRule : IBusinessRule<string>
{
    public bool IsSatisfiedBy(string model) =>
        model.Length > AppUser.PASSWORD_MIN_LENGTH;
}

