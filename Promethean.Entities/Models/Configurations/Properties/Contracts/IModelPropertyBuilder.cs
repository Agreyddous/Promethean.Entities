using Promethean.Entities.Models.Contracts;

namespace Promethean.Entities.Models.Configurations.Properties.Contracts
{
	public interface IModelPropertyBuilder<TEntity, TModel>
		where TEntity : class, IEntity
		where TModel : IModel<TEntity, TModel>, new()
	{
		void Apply(TEntity entity, TModel model);
		void Apply(TModel model, TEntity entity);
	}
}