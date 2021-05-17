using Promethean.Entities.Models.Configurations;

namespace Promethean.Entities.Models
{
	public abstract class Model<TEntity, TModel> where TEntity : class, IEntity where TModel : Model<TEntity, TModel>, new()
	{
		private ModelBuilderConfiguration<TEntity, TModel> _builder;

		public Model()
		{
			_builder = new ModelBuilderConfiguration<TEntity, TModel>();
			OnBuild(_builder);
		}

		protected TEntity ParseModel() => _builder.ParseModel(this as TModel);
		protected TModel ParseEntity(TEntity entity) => _builder.ParseEntity(entity);

		protected abstract void OnBuild(ModelBuilderConfiguration<TEntity, TModel> builder);
	}
}