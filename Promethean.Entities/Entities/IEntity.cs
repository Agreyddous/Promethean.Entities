namespace Promethean.Entities
{
	public interface IEntity { }

	public interface IEntity<TId> : IEntity
	{
		TId Id { get; }
	}
}