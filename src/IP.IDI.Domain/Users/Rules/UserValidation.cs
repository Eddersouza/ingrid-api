namespace IP.IDI.Domain.Users.Rules;

public class UserValidation
{
    public static ErrorValidation ValidatePassord(string password)
    {
        CompositeBusinessRule<string> passwordRule =
            new PasswordLengthMaxOkRule().WithMessage(UserErrors.PasswordGreaterThanMaxLength)
            .And(new PasswordLengthMinOkRule().WithMessage(UserErrors.PasswordLessThanMinLength))
            .And(new PasswordHasUppercaseRule().WithMessage(UserErrors.PasswordMustHaveUppercase))
            .And(new PasswordHasLowercaseRule().WithMessage(UserErrors.PasswordMustHaveLowercase))
            .And(new PasswordDigitRule().WithMessage(UserErrors.PasswordMustHaveDigits))
            .And(new PasswordHasSpecialCharRule().WithMessage(UserErrors.PasswordMustHaveSpecialChars));

        BusinessRuleValidationResult validationResult = passwordRule.Validate(password);

        ErrorValidation errors = new([.. validationResult.Errors]);
        return errors;
    }
}
