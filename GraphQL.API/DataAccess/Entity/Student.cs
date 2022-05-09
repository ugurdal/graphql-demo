namespace GraphQL.API.DataAccess.Entity;

public class Student : IEntity<Guid>
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public decimal GPA { get; set; }

    public virtual IEnumerable<Course> Courses { get; set; }
}