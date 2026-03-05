namespace IP.IDI.Domain.Users.Rules;

public class PasswordDigitRule : IBusinessRule<string>
{
    public bool IsSatisfiedBy(string model) =>
        model.Any(char.IsDigit);
}
