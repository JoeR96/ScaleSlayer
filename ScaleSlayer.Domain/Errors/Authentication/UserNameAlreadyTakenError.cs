namespace ScaleSlayer.Domain.Errors.Authentication;

public class UserNameAlreadyTakenError(string errorMessage = "Username is already taken.")
    : AuthenticationError("Authentication.UserNameAlreadyTaken", errorMessage: errorMessage);