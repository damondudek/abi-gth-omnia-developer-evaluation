using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Models;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of IUserRepository for User-specific operations
/// </summary>
public class UserRepository : BaseRepository<User, DefaultContext>, IUserRepository
{
    public UserRepository(DefaultContext context) : base(context)
    {
    }

    public Task<PaginatedList<User>> GetPaginatedAsync(int pageNumber, int pageSize, string orderBy, Dictionary<string, string> filters, CancellationToken cancellationToken = default)
    {
        var query = _context.Users.AsNoTracking().AsQueryable();
        var pagedList = query.ToPagedListAsync(pageNumber, pageSize, orderBy, filters, cancellationToken);

        return pagedList;
    }

    /// <summary>
    /// Retrieves a user by their email address
    /// </summary>
    public Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        => _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email, cancellationToken);

    /// <summary>
    /// Retrieves a user by their username
    /// </summary>
    public Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
        => _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Username == username, cancellationToken);
}
