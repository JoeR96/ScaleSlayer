using ScaleSlayer.Domain.UserAggregate;

namespace ScaleSlayer.Application.Authentication;

public record AuthenticationResponse(User User, string Token);
