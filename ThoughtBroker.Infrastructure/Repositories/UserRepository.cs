﻿using Microsoft.EntityFrameworkCore;
using ThoughtBroker.Domain.Users;
using ThoughtBroker.Infrastructure.Context;

namespace ThoughtBroker.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly EfDbContext _context;

    public UserRepository(EfDbContext context)
    {
        _context = context;
    }
    
    public async Task AddUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task<User?> GetUserAsync(Guid id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        return user;
    }
    
    public async Task<User?> GetUserAsync(string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        return user;
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        var users = await _context.Users.ToListAsync();
        return users;
    }

    public async Task UpdateUserNonSensitiveDataAsync(User user)
    {
        var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
        if (dbUser is null) throw new ArgumentNullException();
        dbUser.Username = user.Username;
        dbUser.Email = user.Email;
        await _context.SaveChangesAsync();
    }

    public async Task<bool> UserExistsAsync(string username, string email)
    {
        var usernameCheck = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        var emailCheck = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        return usernameCheck is not null || emailCheck is not null;
    }

    public async Task<Guid> PutPasswordAsync(Guid id, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user is null) return Guid.Empty;
        user.PasswordHash = password;
        await _context.SaveChangesAsync();
        return user.Id;
    }

    public async Task<Guid> DeleteAccountAsync(Guid id)
    {
        var user = await _context.Users.Include(u => u.Opinions).Include(u => u.Thoughts).FirstOrDefaultAsync(u => u.Id == id);
        if (user is null) return Guid.Empty;
        _context.Remove(user);
        await _context.SaveChangesAsync();
        return id;
    }
}