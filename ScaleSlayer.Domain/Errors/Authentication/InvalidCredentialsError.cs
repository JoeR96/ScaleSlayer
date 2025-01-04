namespace ScaleSlayer.Domain.Errors.Authentication
{
    public class InvalidCredentialsError(string errorMessage)
        : AuthenticationError("Authentication.InvalidCredentials", errorMessage: errorMessage);
}
