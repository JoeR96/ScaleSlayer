using Microsoft.AspNetCore.Http;

namespace ScaleSlayer.Domain.Errors.General;

public class NotFoundError : Error
{
    public NotFoundError(
        string? errorMessage = null) : base(nameof(NotFoundError), StatusCodes.Status404NotFound, errorMessage)
    {
        ErrorMessage = errorMessage;
    }
}