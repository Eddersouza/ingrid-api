namespace IP.Shared.Api.Endpoints;

public static class ResultExtensions
{
    public static TOut Match<TOut>(
        this Result result,
        Func<TOut> onSuccess,
        Func<Result, TOut> onFailure) =>
            result.IsSuccess ?
                onSuccess() :
                onFailure(result);

    public static TOut Match<TOut>(
       this Result result,
       Func<TOut> onSuccess,
       Func<Result, HttpContext, TOut> onFailure,
       HttpContext context) =>
           result.IsSuccess ?
               onSuccess() :
               onFailure(result, context);

    public static TOut Match<TIn, TOut>(
        this Result<TIn> result,
        Func<TIn, TOut> onSuccess,
        Func<Result<TIn>, TOut> onFailure) =>
            result.IsSuccess ?
                onSuccess(result.Value!) :
                onFailure(result);

    public static TOut Match<TIn, TOut>(
       this Result<TIn> result,
       Func<TIn, TOut> onSuccess,
       Func<Result<TIn>, HttpContext, TOut> onFailure,
       HttpContext context) =>
           result.IsSuccess ?
               onSuccess(result.Value!) :
               onFailure(result, context);

    public static TOut Match<TIn, TOut>(
        this Result<TIn> result,
        Func<string, TIn, TOut> onSuccess,
        Func<Result<TIn>, HttpContext, TOut> onFailure,
        HttpContext context,
        string uri) =>
            result.IsSuccess ?
            onSuccess(uri, result.Value!) :
            onFailure(result, context);
}