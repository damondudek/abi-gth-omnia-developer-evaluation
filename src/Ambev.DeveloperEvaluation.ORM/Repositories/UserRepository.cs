using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Models;
using Ambev.DeveloperEvaluation.Domain.Repositories;
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

    /// <summary>
    /// Retrieves all entities from the database
    /// </summary>
    public Task<List<User>> GetAllAsync(CancellationToken cancellationToken = default)
        => _context.Users.AsNoTracking().ToListAsync(cancellationToken);


    public Task<PaginatedList<User>> GetPaginatedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        var query = _context.Users.AsNoTracking().AsQueryable();
        var pagedList = PaginatedList<User>.CreateAsync(query, pageNumber, pageSize, cancellationToken);

        return pagedList;
    }
    

    /// <summary>
    /// Retrieves users
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The list of users</returns>
    public Task<List<User>> GetByPaginationAsync(CancellationToken cancellationToken = default)
        => _context.Users.AsNoTracking().ToListAsync(cancellationToken);

    /// <summary>
    /// Retrieves a user by their email address
    /// </summary>
    public Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }

    /// <summary>
    /// Retrieves a user by their username
    /// </summary>
    public Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        return _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Username == username, cancellationToken);
    }
}
