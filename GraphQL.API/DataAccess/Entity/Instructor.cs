namespace GraphQL.API.DataAccess.Entity;

public class Instructor : IEntity<Guid>
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public decimal Salary { get; set; }

    public virtual IEnumerable<Course> Courses { get; set; }
}