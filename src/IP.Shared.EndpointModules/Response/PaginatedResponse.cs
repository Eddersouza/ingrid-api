using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IP.Shared.EndpointModules.Response;

public class ResolvedDataPaginatedDTO<TValue>(
    IEnumerable<TValue> data,
    ResolvedDataPaginationDTO pagination)
{
    public IEnumerable<TValue> Data { get; set; } = data;
    public ResolvedDataPaginationDTO Pagination { get; set; } = pagination;
}

public sealed class ResolvedDataPaginationDTO(
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
