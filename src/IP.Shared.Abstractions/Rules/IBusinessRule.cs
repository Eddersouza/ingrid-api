namespace IP.Shared.Abstractions.Rules;

public interface IBusinessRuleMessage<in TData> : IBusinessRule<TData>
{
    BusinessRuleValidationResult Validate(TData model);
}

public interface IBusinessRule<in TData>
{
    bool IsSatisfiedBy(TData model);
}

public class BusinessRuleValidationResult
{
    public BusinessRuleValidationResult() =>
        Errors = [];

    public List<Error> Errors { get; }
    public bool IsValid => !Errors.Any();

    public void AddError(Error errorMessage) =>
        Errors.Add(errorMessage);

    public void AddErrors(IEnumerable<Error> messages) =>
        Errors.AddRange(messages);
}

public class MessageRuleDecorator<TData>(
    IBusinessRule<TData> _inner,
    Error _errorMessage) : IBusinessRuleMessage<TData>
{
    public bool IsSatisfiedBy(TData model)
        => _inner.IsSatisfiedBy(model);

    public BusinessRuleValidationResult Validate(TData model)
    {
        var result = new BusinessRuleValidationResult();

        if (!IsSatisfiedBy(model))
        {
            result.AddError(_errorMessage);
        }

        return result;
    }
}

public static class RuleExtensions
{
    public static IBusinessRuleMessage<TData> WithMessage<TData>(
        this IBusinessRule<TData> rule, Error message) =>
            new MessageRuleDecorator<TData>(rule, message);
}

public abstract class CompositeBusinessRule<TData> : IBusinessRuleMessage<TData>
{
    public abstract bool IsSatisfiedBy(TData model);

    public abstract BusinessRuleValidationResult Validate(TData model);
}

public static class CompositeExtensions
{
    public static CompositeBusinessRule<TModel> And<TModel>(
        this IBusinessRule<TModel> left,
        IBusinessRule<TModel> right) => 
            new AndConditionalRule<TModel>(left, right);
}

public class AndConditionalRule<TData>(
    IBusinessRule<TData> _leftRule, 
    IBusinessRule<TData> _rightRule) : 
    CompositeBusinessRule<TData>
{
    public override bool IsSatisfiedBy(TData model)
        => _leftRule.IsSatisfiedBy(model) && _rightRule.IsSatisfiedBy(model);

    public override BusinessRuleValidationResult Validate(TData model)
    {
        BusinessRuleValidationResult result = new();

        if (_leftRule is IBusinessRuleMessage<TData> leftMsg)
            result.AddErrors(leftMsg.Validate(model).Errors);

        if (_rightRule is IBusinessRuleMessage<TData> rightMsg)
            result.AddErrors(rightMsg.Validate(model).Errors);

        return result;
    }
}