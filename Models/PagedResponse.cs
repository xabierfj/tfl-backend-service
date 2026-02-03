namespace TflBackendService.Models;

public class PagedResponse<T>
{
    public required IReadOnlyCollection<T> Items { get; init; }
    public required PaginationMetadata Pagination { get; init; }
}

public class PaginationMetadata
{
    public required int Page { get; init; }
    public required int PageSize { get; init; }
    public required int TotalCount { get; init; }
    public required int TotalPages { get; init; }
}
