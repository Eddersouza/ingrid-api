namespace IP.IDI.Domain.Users.Rules;

public class PasswordHasSpecialCharRule : IBusinessRule<string>
{
    public bool IsSatisfiedBy(string model) =>
        model.Any(ch => !char.IsLetterOrDigit(ch));
}
