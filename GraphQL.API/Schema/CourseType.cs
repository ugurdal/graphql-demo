using GraphQL.API.DataLoaders;
using GraphQL.API.Models;

namespace GraphQL.API.Schema;

public class CourseType
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Subject Subject { get; set; }

    //[GraphQLIgnore]
    public Guid InstructorId { get; set; }

    [GraphQLNonNullType]
    public async Task<InstructorType> Instructor([Service] InstructorDataLoader dataLoader) // to solve n+1 problem
    {
        var item = await dataLoader.LoadAsync(this.InstructorId, CancellationToken.None);
        return new InstructorType
        {
            Id = item.Id,
            FirstName = item.FirstName,
            LastName = item.LastName,
            Salary = item.Salary
        };
    }
    public IEnumerable<StudentType> Students { get; set; }
}