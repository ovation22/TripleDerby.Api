namespace TripleDerby.Core.DTOs
{
    public record PaginationInfo
    {
        public int TotalItems { get; init; }
        public int ItemsPerPage { get; init; }
        public int ActualPage { get; init; }
        public int TotalPages { get; init; }
        public string Previous { get; init; } = default!;
        public string Next { get; init; } = default!;
    }
}
