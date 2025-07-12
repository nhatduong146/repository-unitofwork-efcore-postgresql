using System.Text.Json.Serialization;

namespace RepositoryUnitOfWorkEFCoreSQL.Application.Common.Models;

public class ErrorResponse
{
    [JsonPropertyName("message")]
    public string Message { get; set; }

    [JsonPropertyName("detail")]
    public string? Detail { get; set; }

    [JsonPropertyName("errors")]
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
