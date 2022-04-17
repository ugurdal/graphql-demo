using Bogus;
using GraphQL.API.DataAccess.Entity;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.API.DataAccess;

public static class AppDbContextSeed
{
    public static async Task SeedAsync(this AppDbContext dbContext)
    {
        var seeded = false;
        if (!await dbContext.Instructors.AnyAsync())
        {
            seeded = true;
            await dbContext.Instructors.AddRangeAsync(GetInstructors());
        }

        if (!await dbContext.Students.AnyAsync())
        {
            seeded = true;
            await dbContext.Students.AddRangeAsync(GetStudents());
        }

        if (seeded)
            await dbContext.SaveChangesAsync();
    }

    private static IEnumerable<Instructor> GetInstructors()
    {
        var faker = new Faker<Instructor>()
            .RuleFor(x => x.Id, f => Guid.NewGuid())
            .RuleFor(x => x.FirstName, f => f.Name.FirstName())
            .RuleFor(x => x.LastName, f => f.Name.LastName())
            .RuleFor(x => x.Salary, f => Math.Ceiling(f.Random.Decimal(0m, 99999m)));

        return faker.Generate(5);
    }

    private static IEnumerable<Student> GetStudents()
    {
        var faker = new Faker<Student>()
            .RuleFor(x => x.Id, f => Guid.NewGuid())
            .RuleFor(x => x.FirstName, f => f.Name.FirstName())
            .RuleFor(x => x.LastName, f => f.Name.LastName())
            .RuleFor(x => x.GPA, f => Math.Round(f.Random.Decimal(1m, 4m), 2));

        return faker.Generate(5);
    }
}