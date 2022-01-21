using BRW.Domain.Entities;
using BRW.Domain.Exceptions;
using BRW.Domain.IRepository;
using Microsoft.EntityFrameworkCore;
using Project.Infrastructure.Context;

namespace Project.Infrastructure.Repository;

public class UserRepository : IUserRepository
{
    private readonly BrwAppContext _context;

    public UserRepository(BrwAppContext context)
    {
        _context = context;
    }


    public async Task<List<User>> GetAllUsers()
    {
        try
        {
            return await _context.Users.ToListAsync();
        }
        catch (Exception e)
        {
            throw new ServerException("Erro ao acessar o banco");
        }
    }

    public async Task<User?> GetUserByGuid(Guid guid)
    {
        try
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Guid == guid);
        }
        catch (Exception e)
        {
            throw new ServerException("Erro ao acessar o banco");
        }
    }

    public async Task<bool> UserEmailExists(string email)
    {
        try
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }
        catch (Exception e)
        {
            throw new ServerException("Erro ao acessar o banco");
        }
    }
    

    public async Task<User> CreateUser(User user)
    {
        try
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
        catch (Exception e)
        {
            throw new ServerException("Erro no servidor ao tentar cadastrar");
        }
    }
}