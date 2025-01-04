using ScaleSlayer.Domain.UserAggregate;

namespace ScaleSlayer.Application.Contracts.Persistence;

public interface IUserRepository
{
    Task<User?> GetUserByEmailAsync(string email);
    Task<User?> GetUserByUserNameAsync(string userName);
    void AddUser(User user);
    Task<bool> SaveChangesAsync();

}