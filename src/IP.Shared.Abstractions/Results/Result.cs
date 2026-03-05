namespace IP.Shared.Abstractions.Results;

public class Result
{
    public Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None || 
            !isSuccess && error == Error.None)
            throw new ArgumentException("Invalid Error", nameof(error));

        IsSuccess = isSuccess;
        Error = error;
    }

    public Error Error { get; } = Error.None;
    public bool IsFailure => !IsSuccess;
    public bool IsSuccess { get; }

    public static Result Failure(Error error) => new(false, error);

    public static Result<TValue> Failure<TValue>(Error error) =>
        new(default, false, error);

    public static Result Success() => new(true, Error.None);

    public static Result<TValue> Success<TValue>(TValue value) =>
        new(value, true, Error.None);
}

public sealed class Result<TValue>(
    TValue? value, 
    bool isSucess, Error error) : 
    Result(isSucess, error)
{
    private readonly TValue? _value = value;

    public TValue? Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException(
            "The Value of a failure result can't be accessed");

    public static implicit operator Result<TValue>(TValue? value) =>
        Success(value!);

    public static implicit operator Result<TValue>(Error error) =>
        Failure<TValue>(error);
}

public class ResolvedData<TValue>(TValue? data, string message)
{
    public TValue? Data { get; set; } = data;
    public string Message { get; set; } = message;
}

public class ResolvedDataPaginated<TValue>(
    IEnumerable<TValue> data,
    ResolvedDataPagination pagination)
{
    public IEnumerable<TValue> Data { get; set; } = data;
    public ResolvedDataPagination Pagination { get; set; } = pagination;
}

public sealed class ResolvedDataPagination(
    int pageNumber,
    int pageSize,
    int totalItems)
{
    public int PageNumber { get; } = pageNumber;

    public int EndPage { get; } =
        (int)Math.Ceiling(totalItems / (decimal)pageSize);

    public int PageSize { get; } = pageSize;
    public int TotalItems { get; } = totalItems;

    public int TotalPages { get; } = (int)Math.Ceiling(totalItems / (decimal)pageSize);
}
