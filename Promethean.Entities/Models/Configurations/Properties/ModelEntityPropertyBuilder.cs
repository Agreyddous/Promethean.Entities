using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Promethean.Entities.Models.Configurations.Properties.Contracts;
using Promethean.Entities.Models.Contracts;

namespace Promethean.Entities.Models.Configurations.Properties
{
	public class ModelEntityPropertyBuilder<TEntity, TModel, TModelValue> : IModelEntityPropertyBuilder<TEntity, TModel, TModelValue>
		where TEntity : class, IEntity
		where TModel : IModel<TEntity, TModel>, new()
	{
		private readonly Expression<Func<TModel, TModelValue>> _modelProperty;
		private readonly List<IModelPropertyBuilder<TEntity, TModel>> _properties;

		public ModelEntityPropertyBuilder(Expression<Func<TModel, TModelValue>> modelProperty,
									List<IModelPropertyBuilder<TEntity, TModel>> properties)
		{
			_modelProperty = modelProperty;
			_properties = properties;
		}

		public ModelPropertyBuilder<TEntity, TModel, TEntityValue, TModelValue> HasEntityProperty<TEntityValue>(Expression<Func<TEntity, TEntityValue>> entityProperty) => new ModelPropertyBuilder<TEntity, TModel, TEntityValue, TModelValue>(entityProperty, _modelProperty, _properties);

		public ModelPropertyBuilder<TEntity, TModel, object, TModelValue> ModelOnly() => new ModelPropertyBuilder<TEntity, TModel, object, TModelValue>(null, _modelProperty, _properties).IsModelOnly();
	}
}