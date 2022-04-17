using GraphQL.API.Models;

namespace GraphQL.API.DataAccess.Entity;

public class Course : IEntity<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Subject Subject { get; set; }
    public Guid InstructorId { get; set; }

    public virtual Instructor Instructors { get; set; }
    public virtual IEnumerable<Student> Students { get; set; }
}