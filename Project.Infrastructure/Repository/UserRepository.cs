using Microsoft.EntityFrameworkCore;
using Project.Domain.Entities;
using Project.Domain.Exceptions;
using Project.Domain.IRepository;
using Project.Infrastructure.Context;

namespace Project.Infrastructure.Repository;

public class UserRepository : IUserRepository
{
    private readonly DataContext _context;

    public UserRepository(DataContext context)
    {
        _context = context;
    }
    public async Task<List<User>> GetAllUsers()
    {
        return await _context.Users.AsNoTracking().ToListAsync();
    }

    public async Task<User> GetUserByGuid(Guid guid)
    {
        var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Guid == guid);

        if (user == default) throw new NotFoundException("Usuário não encontrado");
        return user;
    }

    public async Task<bool> UserEmailExists(string email)
    {
        return await _context.Users.AsNoTracking().AnyAsync(u => u.Email == email);
    }

    public async Task<User> CreateUser(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task DeleteUser(Guid guid)
    {
        var user = await GetUserByGuid(guid);
        if (user == null) throw new NotFoundException("Usuário não encontrado");
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }
}