namespace ScaleSlayer.Web.Server.Models.Authentication;

public class AuthResponse
{
    public UserDto User { get; set; }
    public string Token { get; set; }
}