using LoopLearner.Application.Contracts.Persistence;
using LoopLearner.Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;

namespace LoopLearner.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRespository
{
    private readonly LoopLearnerDbContext _context;

    public UserRepository(LoopLearnerDbContext context)
    {
        _context = context;
    }

    public void AddUser(User user)
    {
        _context.Users.Add(user);
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetUserByUserNameAsync(string userName)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);

    }

    public async Task<bool> SaveChangesAsync()
    {
        return (await _context.SaveChangesAsync() > 0);
    }
}