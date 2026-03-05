using IP.Shared.EndpointModules.Response;
using Refit;

namespace IP.Shared.EndpointModules.Core;

public interface IEmployeeEndpoint
{
    [Get("/v1/employees/{id}/team/select")]
    Task<ApiResponse<GetEmployeeTeamDTOResponse>> GetTeamByEmpoyeeIdManager(Guid id, [Query] GetEmployeeTeamDTOParams parameters);
}

public sealed record GetEmployeeTeamDTOParams(string? OrderBy, int? PageNumber, int? PageSize);

public sealed class GetEmployeeTeamDTOResponse(
    IEnumerable<ValueLabelItemDTO> data,
    ResolvedDataPaginationDTO pagination) :
    ResolvedDataPaginatedDTO<ValueLabelItemDTO>(data, pagination);