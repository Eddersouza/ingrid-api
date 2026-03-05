using IP.Core.Domain.States;
using IP.IDI.Domain.Roles;
using Org.BouncyCastle.Asn1.Ocsp;

namespace IP.Core.Api.States.GetStates;

internal class GetStatesQueryHandler(
    ICoreUoW _unitOfWork) :
    IQueryHandler<GetStatesQuery, GetStatesResponse>
{
    private readonly IStateRepository _stateRepository =
        _unitOfWork.GetRepository<IStateRepository>();

    public async Task<Result<GetStatesResponse>> Handle(
        GetStatesQuery query,
        CancellationToken cancellationToken)
    {
        Dictionary<string, Expression<Func<State, object>>> sortDictionary = new()
        {
            { "name", x => x.Name.Value },
        };

        GetStatesQueryRequest queryRequest = query.Request;

        int pageNumber = queryRequest.PageNumber ?? 1;
        int pageSize = queryRequest.PageSize ?? 10;

        IQueryable<State> queryStates =
            _stateRepository.Data().AsNoTracking();

        IQueryable<GetStatesResponseData> states =
            ApplyUserFilters(queryRequest, queryStates)
            .OrderBy(sortDictionary, query.Request.OrderBy, ["name"])
            .Paginate(pageNumber, pageSize)
            .Select(x => new GetStatesResponseData(x.Id.Value, x.IBGECode, x.Code, x.Name.Value));

        int count = await queryStates.CountAsync(cancellationToken);

        GetStatesResponse response = new(
            states,
            new ResolvedDataPagination(pageNumber, pageSize, count));

        return Result.Success(response);
    }
    private static IQueryable<State> ApplyUserFilters(
        GetStatesQueryRequest queryRequest,
        IQueryable<State> queryRoles)
    {
        string normalizedName = queryRequest?.NameContains?.NormalizeCustom() ?? string.Empty;
        string? code = queryRequest?.CodeContains?.NormalizeCustom() ?? string.Empty;
        string? ibgeCode = queryRequest?.IBGECodeContains?.NormalizeCustom() ?? string.Empty;

        return queryRoles
            .WhereIf(normalizedName.IsNotNullOrWhiteSpace(),
                query => query.Name.ValueNormalized.Contains(normalizedName))
            .WhereIf(code.IsNotNullOrWhiteSpace(),
                query => query.Code.Contains(code))
            .WhereIf(ibgeCode.IsNotNullOrWhiteSpace(),
                query => query.IBGECode.Contains(ibgeCode));
    }
}