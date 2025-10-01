using System.Net;

namespace Library.Shared.Responses;

public record ExceptionResponse(HttpStatusCode Code, string Message, List<string>? Details = null);