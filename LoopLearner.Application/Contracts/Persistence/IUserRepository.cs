using LoopLearner.Domain.UserAggregate;

namespace LoopLearner.Application.Contracts.Persistence;

public interface IUserRepository
{
    Task<User?> GetUserByEmailAsync(string email);
    Task<User?> GetUserByUserNameAsync(string userName);
    void AddUser(User user);
    Task<bool> SaveChangesAsync();

}