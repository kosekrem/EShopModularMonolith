namespace Shared.DDD;

public interface IEntity<out T> : IEntity
{
    public T Id { get; }
}


public interface IEntity
{
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public string? LastModifiedBy { get; set; }
}