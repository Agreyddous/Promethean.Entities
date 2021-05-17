using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Promethean.Entities.Models.Configurations
{
	public class ModelBuilderPropertyConfiguration<TEntity, TModel, TEntityValue, TModelValue> : IModelBuilderPropertyConfiguration<TEntity, TModel> where TEntity : class, IEntity where TModel : Model<TEntity, TModel>, new()
	{
		private Expression<Func<TEntity, TEntityValue>> _entityPropertyExpression;
		private Func<TEntity, TModelValue> _entityGetPropertyValue;
		private Action<TEntity, TModel> _entitySetPropertyValue;

		private Expression<Func<TModel, TModelValue>> _modelPropertyExpression;
		private Func<TModel, TEntityValue> _modelGetPropertyValue;
		private Action<TModel, TEntity> _modelSetPropertyValue;

		public ModelBuilderPropertyConfiguration(Expression<Func<TEntity, TEntityValue>> entityProperty, Expression<Func<TModel, TModelValue>> modelProperty)
		{
			_handleEntityProperty(entityProperty);
			_handleModelProperty(modelProperty);
		}

		public ModelBuilderPropertyConfiguration(Expression<Func<TEntity, TEntityValue>> entityProperty) => _handleEntityProperty(entityProperty);

		public ModelBuilderPropertyConfiguration(Expression<Func<TModel, TModelValue>> modelProperty) => _handleModelProperty(modelProperty);

		public void Apply(TEntity entity, TModel model)
		{
			if (_entitySetPropertyValue != null)
				_entitySetPropertyValue(entity, model);
		}

		public void Apply(TModel model, TEntity entity)
		{
			if (_modelSetPropertyValue != null)
				_modelSetPropertyValue(model, entity);
		}

		private void _handleEntityProperty(Expression<Func<TEntity, TEntityValue>> entityProperty)
		{
			_entityPropertyExpression = entityProperty;

			MemberExpression entityMemberExpression = (MemberExpression)_entityPropertyExpression.Body;
			PropertyInfo entityPropertyInfo = (PropertyInfo)entityMemberExpression.Member;

			_entityGetPropertyValue = entity => (TModelValue)entityPropertyInfo.GetValue(entity);
			_entitySetPropertyValue = (entity, model) => entityPropertyInfo.SetValue(entity, _modelGetPropertyValue(model));
		}

		private void _handleModelProperty(Expression<Func<TModel, TModelValue>> modelProperty)
		{
			_modelPropertyExpression = modelProperty;

			MemberExpression modelMemberExpression = (MemberExpression)_modelPropertyExpression.Body;
			PropertyInfo modelPropertyInfo = (PropertyInfo)modelMemberExpression.Member;

			_modelGetPropertyValue = model => (TEntityValue)modelPropertyInfo.GetValue(model);
			_modelSetPropertyValue = (model, entity) => modelPropertyInfo.SetValue(model, _entityGetPropertyValue(entity));
		}
	}
}