using Microsoft.EntityFrameworkCore;
using ScaleSlayer.Application.Contracts.Persistence;
using ScaleSlayer.Domain.UserAggregate;

namespace ScaleSlayer.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ScaleSlayerDbContext _context;

    public UserRepository(ScaleSlayerDbContext context)
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