using InventoryManagement.Application.Interfaces;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Infrastructure.Persistence;
using InventoryManagement.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Infrastructure.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{

    public UserRepository(ApplicationDbContext context) : base(context)
    {
        
    }
    public async Task<IEnumerable<User>> SearchByText(string text)
    {
        text = text?.Trim().ToLower() ?? string.Empty;
        return await _db.AsNoTracking()
            .Where(p => p.Username.ToLower().Contains(text) || p.Email.ToLower().Contains(text)).ToListAsync();
    }
}