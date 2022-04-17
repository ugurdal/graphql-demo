using GraphQL.API.DataAccess;
using GraphQL.API.DataAccess.Entity;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.API.Services;

public class CourseRepository
{
    private readonly IDbContextFactory<AppDbContext> _dbContextFactory;

    public CourseRepository(IDbContextFactory<AppDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<IEnumerable<Course>> GetAll()
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        return await context.Courses.ToListAsync();
    }

    public async Task<Course> GetById(Guid id)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        return await context.Courses.SingleOrDefaultAsync(x => x.Id.Equals(id));
    }

    public async Task<Course> CreateCourse(Course course)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        await context.Courses.AddAsync(course);
        await context.SaveChangesAsync();

        return course;
    }

    public async Task<Course> UpdateCourse(Course course)
    {
        // check table if content is exist before update
        using var context = await _dbContextFactory.CreateDbContextAsync();
        context.Courses.Update(course);
        await context.SaveChangesAsync();

        return course;
    }

    public async Task<bool> DeleteCourse(Guid id)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        var course = new Course { Id = id };
        context.Courses.Remove(course);

        try
        {
            var result = await context.SaveChangesAsync();
            return result > 0;
        }
        catch (Exception)
        {
            // Ignored
            return false;
        }
    }
}
