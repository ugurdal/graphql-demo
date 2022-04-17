using GraphQL.API.Models;

namespace GraphQL.API.Schema.Mutations;

public record CourseInputType
{
    public string Name { get; set; }
    public Subject Subject { get; set; }
    public Guid InstructorId { get; set; }
}
