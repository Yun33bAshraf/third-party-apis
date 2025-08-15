namespace IApply.Frontend.Models
{
    public class BaseResponse
    {
        public bool IsSuccess { get; set; } = false;
        public int ErrorCode { get; set; }
        public object? Data { get; set; }
        public Pagination? Pagination { get; set; }
    }

    public class ListingBaseResponse<T>
    {
        public bool IsSuccess { get; set; } = false;
        public int ErrorCode { get; set; }
        public List<T>? Data { get; set; }
        public Pagination? Pagination { get; set; }
    }

    public class Pagination
    {
        public int TotalCount { get; set; }
        public int RecordsReturned { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    public class BaseResponse<T>
    {
        public bool Status { get; set; } = false;
        public T? Data { get; set; }
        public Pagination? Pagination { get; set; }
        public string? Message { get; set; }
        public string? Error { get; set; }
    }

    public class BaseResponseListing<T>
    {
        public bool Status { get; set; } = false;
        public List<T>? Data { get; set; }
        public Pagination? Pagination { get; set; }
        public string? Message { get; set; }
        public string? Error { get; set; }
    }
}
