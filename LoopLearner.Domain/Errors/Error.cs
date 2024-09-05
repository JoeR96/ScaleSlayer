using Microsoft.AspNetCore.Http;

namespace LoopLearner.Domain.Errors
{
    public class Error(
        string errorType = "InternalServerError",
        int? statusCode = StatusCodes.Status500InternalServerError,
        string? errorMessage = null)
    {
        public string ErrorType { get; set; } = errorType;
        public int? StatusCode { get; set; } = statusCode;
        public string? ErrorMessage { get; set; } = errorMessage;
    }
}
