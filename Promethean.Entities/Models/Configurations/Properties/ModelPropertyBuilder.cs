using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Promethean.Entities.Models.Configurations.Properties.Contracts;
using Promethean.Entities.Models.Contracts;

namespace Promethean.Entities.Models.Configurations.Properties
{
	public class ModelPropertyBuilder<TEntity, TModel, TEntityValue, TModelValue> : IModelPropertyBuilder<TEntity, TModel>
		where TEntity : class, IEntity
		where TModel : IModel<TEntity, TModel>, new()
	{
		private readonly Expression<Func<TEntity, TEntityValue>> _entityProperty;
		private Func<TEntity, TModelValue> _entityPropertyValue;

		private readonly Expression<Func<TModel, TModelValue>> _modelProperty;
		private Func<TModel, TEntityValue> _modelPropertyValue;

		private bool _modelOnly = false;
		private bool _entityOnly = false;

		public ModelPropertyBuilder(Expression<Func<TEntity, TEntityValue>> entityProperty, Expression<Func<TModel, TModelValue>> modelProperty, List<IModelPropertyBuilder<TEntity, TModel>> properties)
		{
			_entityProperty = entityProperty;
			_modelProperty = modelProperty;

			properties.Add(this);
		}

		public ModelPropertyBuilder<TEntity, TModel, TEntityValue, TModelValue> IsModelOnly()
		{
			_modelOnly = true;

			return this;
		}

		public ModelPropertyBuilder<TEntity, TModel, TEntityValue, TModelValue> IsEntityOnly()
		{
			_entityOnly = true;

			return this;
		}

		public ModelPropertyBuilder<TEntity, TModel, TEntityValue, TModelValue> GetEntityValue(Func<TEntity, TModelValue> entityPropertyValue)
		{
			_entityPropertyValue = entityPropertyValue;

			return this;
		}

		public ModelPropertyBuilder<TEntity, TModel, TEntityValue, TModelValue> GetModelValue(Func<TModel, TEntityValue> modelPropertyValue)
		{
			_modelPropertyValue = modelPropertyValue;

			return this;
		}

		public void Apply(TEntity entity, TModel model)
		{
			if (!_modelOnly)
			{
				PropertyInfo entityProperty = _getEntityPropertyInfo();

				if (_modelPropertyValue == null)
					_modelPropertyValue = model => (TEntityValue)(_getModelPropertyInfo().GetValue(model) ?? (TEntityValue)default);

				entityProperty.SetValue(entity, (TEntityValue)_modelPropertyValue(model));
			}
		}

		public void Apply(TModel model, TEntity entity)
		{
			if (!_entityOnly)
			{
				PropertyInfo modelProperty = _getModelPropertyInfo();

				if (_entityPropertyValue == null)
					_entityPropertyValue = entity => (TModelValue)(_getEntityPropertyInfo().GetValue(entity) ?? (TModelValue)default);

				modelProperty.SetValue(model, (TModelValue)_entityPropertyValue(entity));
			}
		}

		private PropertyInfo _getEntityPropertyInfo() => (PropertyInfo)((MemberExpression)_entityProperty.Body).Member;
		private PropertyInfo _getModelPropertyInfo() => (PropertyInfo)((MemberExpression)_modelProperty.Body).Member;
	}
}