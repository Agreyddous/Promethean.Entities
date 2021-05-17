namespace Promethean.Entities
{
	public interface IEntity
	{
		object Id { get; }
	}

	public interface IEntity<TId> : IEntity
	{
		new TId Id { get; }
	}
}