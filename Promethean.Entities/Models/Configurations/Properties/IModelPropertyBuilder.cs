namespace Promethean.Entities.Models.Configurations.Properties
{
	public interface IModelPropertyBuilder<TEntity, TModel>
		where TEntity : class, IEntity
		where TModel : Model<TEntity, TModel>, new()
	{
		void Apply(TEntity entity, TModel model);
		void Apply(TModel model, TEntity entity);
	}
}