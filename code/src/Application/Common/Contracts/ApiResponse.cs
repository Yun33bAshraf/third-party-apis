using OpenProfileAPI.Application.Common.Models;

namespace OpenProfileAPI.Application.Common.Contracts;

public static class ApiResponse
{
    public static ResponseBase Success(object? data = null, string? message = null, Pagination? pagination = null)
    {
        return new ResponseBase
        {
            Status = true,
            Data = data,
            Message = message ?? "Request completed successfully.",
            Pagination = pagination
        };
    }

    public static ResponseBase Error(string error, object? errorDetails = null)
    {
        return new ResponseBase
        {
            Status = false,
            Error = string.IsNullOrWhiteSpace(errorDetails?.ToString()) ? error : new { Message = error, Details = errorDetails }
        };
    }
}
