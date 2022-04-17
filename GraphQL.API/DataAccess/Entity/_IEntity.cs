namespace GraphQL.API.DataAccess.Entity;

public interface IEntity<TId> where TId : struct
{
    public TId Id { get; set; }
}