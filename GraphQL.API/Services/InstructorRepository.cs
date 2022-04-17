using GraphQL.API.DataAccess;
using GraphQL.API.DataAccess.Entity;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.API.Services;

public class InstructorRepository
{
    private readonly IDbContextFactory<AppDbContext> _dbContextFactory;

    public InstructorRepository(IDbContextFactory<AppDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<Instructor> GetById(Guid id)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        return await context.Instructors.SingleOrDefaultAsync(x => x.Id.Equals(id));
    }

    public async Task<IEnumerable<Instructor>> GetManyById(IReadOnlyList<Guid> keys)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        return await context.Instructors.Where(x => keys.Contains(x.Id)).ToListAsync();
    }
}