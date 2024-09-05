namespace LoopLearner.Web.Server.Models.Authentication;

public class UserDto
{
    public Guid Id { get; private set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}