namespace RepositoryUnitOfWorkEFCoreSQL.Application.Common.Models;

public class ErrorResponse
{
    public string Message { get; set; }
    public string? Detail { get; set; }
    public Dictionary<string, string[]>? Errors { get; set; }

    public ErrorResponse(string message)
    {
        Message = message;
    }

    public ErrorResponse(string message, Dictionary<string, string[]> errors)
    {
        Message = message;
        Errors = errors;
    }

    public ErrorResponse(string message, string? detail)
    {
        Message = message;
        Detail = detail;
    }
}
