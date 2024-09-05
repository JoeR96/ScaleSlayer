namespace LoopLearner.Domain.Errors.Authentication;

public class EmailAlreadyInUseError(string errorMessage = "Email is already in use.")
    : AuthenticationError("Authentication.EmailIsAlreadyInUse", errorMessage: errorMessage);