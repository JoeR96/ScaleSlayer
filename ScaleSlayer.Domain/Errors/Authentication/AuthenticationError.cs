using Microsoft.AspNetCore.Http;

namespace ScaleSlayer.Domain.Errors.Authentication;

public abstract class AuthenticationError(
    string errorType,
    int statusCode = StatusCodes.Status400BadRequest,
    string? errorMessage = null)
    : Error(errorType, statusCode, errorMessage);