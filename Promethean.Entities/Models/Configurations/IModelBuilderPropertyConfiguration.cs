namespace Promethean.Entities.Models.Configurations
{
	public interface IModelBuilderPropertyConfiguration<TEntity, TModel> where TEntity : class, IEntity where TModel : Model<TEntity, TModel>, new()
	{
		void Apply(TEntity entity, TModel model);
		void Apply(TModel model, TEntity entity);
	}
}