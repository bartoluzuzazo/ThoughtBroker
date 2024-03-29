﻿namespace ThoughtBroker.Domain.Users;

public interface IUserRepository
{
    public Task AddUserAsync(User user);

    public Task<User?> GetUserAsync(Guid id);

    public Task<User?> GetUserAsync(string email);

    public Task<List<User>> GetAllUsersAsync();

    public Task UpdateUserNonSensitiveDataAsync(User user);

    public Task<bool> UserExistsAsync(string username, string email);

    public Task<Guid> PutPasswordAsync(Guid id, string password);
    
    public Task<Guid> DeleteAccountAsync(Guid id);

}