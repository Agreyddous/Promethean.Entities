using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Promethean.Entities.Models.Configurations.Builders.Contracts;
using Promethean.Entities.Models.Configurations.Properties;
using Promethean.Entities.Models.Configurations.Properties.Contracts;
using Promethean.Entities.Models.Contracts;

namespace Promethean.Entities.Models.Configurations.Builders
{
	public class ModelConfigurationBuilder<TEntity, TModel> : IModelConfigurationBuilder<TEntity, TModel>
		where TEntity : class, IEntity
		where TModel : IModel<TEntity, TModel>, new()
	{
		private readonly List<IModelPropertyBuilder<TEntity, TModel>> _properties;

		public ModelConfigurationBuilder() => _properties = new List<IModelPropertyBuilder<TEntity, TModel>>();

		public IReadOnlyList<IModelPropertyBuilder<TEntity, TModel>> Properties => _properties;

		public IEntityModelPropertyBuilder<TEntity, TModel, TEntityValue> EntityProperty<TEntityValue>(Expression<Func<TEntity, TEntityValue>> entityProperty) => new EntityModelPropertyBuilder<TEntity, TModel, TEntityValue>(entityProperty, _properties);

		public IModelEntityPropertyBuilder<TEntity, TModel, TModelValue> ModelProperty<TModelValue>(Expression<Func<TModel, TModelValue>> modelProperty) => new ModelEntityPropertyBuilder<TEntity, TModel, TModelValue>(modelProperty, _properties);

		public TEntity Parse(TModel model)
		{
			TEntity entity = RuntimeHelpers.GetUninitializedObject(typeof(TEntity)) as TEntity;

			foreach (IModelPropertyBuilder<TEntity, TModel> property in Properties)
				property.Apply(entity, model);

			return entity;
		}

		public TModel Parse(TEntity entity)
		{
			TModel model = new TModel();

			foreach (IModelPropertyBuilder<TEntity, TModel> property in Properties)
				property.Apply(model, entity);

			return model;
		}
	}
}