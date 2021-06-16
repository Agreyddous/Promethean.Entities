using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Promethean.Entities.Models.Configurations.Properties.Contracts;
using Promethean.Entities.Models.Contracts;

namespace Promethean.Entities.Models.Configurations.Properties
{
	public class EntityModelPropertyBuilder<TEntity, TModel, TEntityValue> : IEntityModelPropertyBuilder<TEntity, TModel, TEntityValue>
		where TEntity : class, IEntity
		where TModel : IModel<TEntity, TModel>, new()
	{
		private readonly Expression<Func<TEntity, TEntityValue>> _entityProperty;
		private readonly List<IModelPropertyBuilder<TEntity, TModel>> _properties;

		public EntityModelPropertyBuilder(Expression<Func<TEntity, TEntityValue>> entityProperty,
									List<IModelPropertyBuilder<TEntity, TModel>> properties)
		{
			_entityProperty = entityProperty;
			_properties = properties;
		}

		public ModelPropertyBuilder<TEntity, TModel, TEntityValue, TModelValue> HasModelProperty<TModelValue>(Expression<Func<TModel, TModelValue>> modelProperty) => new ModelPropertyBuilder<TEntity, TModel, TEntityValue, TModelValue>(_entityProperty, modelProperty, _properties);

		public ModelPropertyBuilder<TEntity, TModel, TEntityValue, object> EntityOnly() => new ModelPropertyBuilder<TEntity, TModel, TEntityValue, object>(_entityProperty, null, _properties).IsEntityOnly();
	}
}