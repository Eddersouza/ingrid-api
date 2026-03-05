namespace IP.IDI.Domain.Users.Rules;

public class PasswordHasLowercaseRule : IBusinessRule<string>
{
    public bool IsSatisfiedBy(string model) =>
        model.Any(char.IsLower);
}
