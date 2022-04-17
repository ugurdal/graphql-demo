namespace GraphQL.API.Schema;

public class StudentType : PersonType
{
    [GraphQLName("gpa")]
    public decimal GPA { get; set; }
}

public abstract class PersonType
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}