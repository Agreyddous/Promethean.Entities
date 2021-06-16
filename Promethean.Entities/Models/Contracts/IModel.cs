using Promethean.Notifications.Contracts;

namespace Promethean.Entities.Models.Contracts
{
	public interface IModel<TEntity, TModel> : INotifiable
		where TEntity : class, IEntity
		where TModel : IModel<TEntity, TModel>, new()
	{
		TEntity Parse();
		TModel Parse(TEntity entity);
	}
}