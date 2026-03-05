namespace IP.IDI.Domain.Users.Rules;

public class PasswordHasUppercaseRule : IBusinessRule<string>
{
    public bool IsSatisfiedBy(string model) =>
        model.Any(char.IsUpper);
}