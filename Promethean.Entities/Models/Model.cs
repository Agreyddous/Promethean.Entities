using Promethean.Entities.Models.Configurations.Builders;
using Promethean.Notifications;

namespace Promethean.Entities.Models
{
	public abstract class Model<TEntity, TModel> : Notifiable
		where TEntity : class, IEntity
		where TModel : Model<TEntity, TModel>, new()
	{
		private ModelConfigurationBuilder<TEntity, TModel> _builder;

		public Model()
		{
			_builder = new ModelConfigurationBuilder<TEntity, TModel>();
			OnBuild(_builder);
		}

		protected virtual TEntity Parse() => _builder.Parse(this as TModel);
		protected virtual TModel Parse(TEntity entity) => _builder.Parse(entity);

		public static implicit operator Model<TEntity, TModel>(TEntity entity)
		{
			TModel model = null;

			if (entity != null)
			{
				model = new TModel();
				model = model.Parse(entity);
			}

			return model;
		}

		public static implicit operator TEntity(Model<TEntity, TModel> model)
		{
			TEntity entity = null;

			if (model != null)
				entity = model.Parse();

			return entity;
		}

		protected abstract void OnBuild(ModelConfigurationBuilder<TEntity, TModel> builder);
	}
}