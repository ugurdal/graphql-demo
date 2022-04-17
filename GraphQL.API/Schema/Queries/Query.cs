using Bogus;
using GraphQL.API.DataAccess;
using GraphQL.API.Models;
using GraphQL.API.Schema.Filters;
using GraphQL.API.Schema.Sorters;
using GraphQL.API.Services;

namespace GraphQL.API.Schema.Queries;

public class Query
{
    private readonly CourseRepository _courseRepository;

    public Query(CourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    [UseOffsetPaging(IncludeTotalCount = true, DefaultPageSize = 10)]
    public async Task<IEnumerable<CourseType>> GetOffsetCources()
    {
        var items = await _courseRepository.GetAll();
        return items.Select(x => new CourseType
        {
            Id = x.Id,
            Name = x.Name,
            Subject = x.Subject,
            InstructorId = x.InstructorId
            // Instructor = new InstructorType
            // {
            //     Id = x.InstructorId,
            //     FirstName = x.Instructors.FirstName,
            //     LastName = x.Instructors.LastName,
            //     Salary = x.Instructors.Salary
            // }
        }).ToList();
    }

    [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
    public async Task<IEnumerable<CourseType>> GetCources()
    {
        var items = await _courseRepository.GetAll();
        return items.Select(x => new CourseType
        {
            Id = x.Id,
            Name = x.Name,
            Subject = x.Subject,
            InstructorId = x.InstructorId
            // Instructor = new InstructorType
            // {
            //     Id = x.InstructorId,
            //     FirstName = x.Instructors.FirstName,
            //     LastName = x.Instructors.LastName,
            //     Salary = x.Instructors.Salary
            // }
        }).ToList();
    }

    // attribute order is important 
    [UseDbContext(typeof(AppDbContext))]
    [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
    [UseFiltering(filterType: typeof(CourseFilterType))]
    [UseSorting(sortingType: typeof(CourseSortType))]
    public IQueryable<CourseType> GetPaginatedCources([ScopedService] AppDbContext dbContext)
    {
        return dbContext.Courses.Select(x => new CourseType
        {
            Id = x.Id,
            Name = x.Name,
            Subject = x.Subject,
            InstructorId = x.InstructorId
            // Instructor = new InstructorType
            // {
            //     Id = x.InstructorId,
            //     FirstName = x.Instructors.FirstName,
            //     LastName = x.Instructors.LastName,
            //     Salary = x.Instructors.Salary
            // }
        }).AsQueryable();
    }

    public async Task<CourseType> GetCourseByIdAsync(Guid id)
    {
        var course = await _courseRepository.GetById(id);
        if (course is null)
            throw new GraphQLException(new Error($"Course not found, id:{id}", ""));

        return new CourseType
        {
            Id = course.Id,
            Name = course.Name,
            Subject = course.Subject,
            InstructorId = course.InstructorId
            // Instructor = new InstructorType
            // {
            //     Id = course.InstructorId,
            //     FirstName = course.Instructors.FirstName,
            //     LastName = course.Instructors.LastName,
            //     Salary = course.Instructors.Salary
            // }
        };
    }

    [GraphQLDeprecated("This query is deprecated.")]
    public string Instructions => "Test Query Type";
}