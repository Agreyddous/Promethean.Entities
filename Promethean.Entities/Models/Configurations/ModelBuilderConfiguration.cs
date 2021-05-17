using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Promethean.Entities.Models.Configurations
{
	public class ModelBuilderConfiguration<TEntity, TModel> where TEntity : class, IEntity where TModel : Model<TEntity, TModel>, new()
	{
		private List<IModelBuilderPropertyConfiguration<TEntity, TModel>> _properties;

		public ModelBuilderConfiguration() => _properties = new List<IModelBuilderPropertyConfiguration<TEntity, TModel>>();

		public IReadOnlyList<IModelBuilderPropertyConfiguration<TEntity, TModel>> Properties => _properties;

		public ModelBuilderPropertyConfiguration<TEntity, TModel, TEntityValue, TModelValue> Property<TEntityValue, TModelValue>(Expression<Func<TEntity, TEntityValue>> entityProperty, Expression<Func<TModel, TModelValue>> modelProperty)
		{
			ModelBuilderPropertyConfiguration<TEntity, TModel, TEntityValue, TModelValue> result = new ModelBuilderPropertyConfiguration<TEntity, TModel, TEntityValue, TModelValue>(entityProperty, modelProperty);

			_properties.Add(result);

			return result;
		}

		public ModelBuilderPropertyConfiguration<TEntity, TModel, TEntityValue, TEntityValue> EntityOnlyProperty<TEntityValue>(Expression<Func<TEntity, TEntityValue>> entityProperty)
		{
			ModelBuilderPropertyConfiguration<TEntity, TModel, TEntityValue, TEntityValue> result = new ModelBuilderPropertyConfiguration<TEntity, TModel, TEntityValue, TEntityValue>(entityProperty);

			_properties.Add(result);

			return result;
		}

		public ModelBuilderPropertyConfiguration<TEntity, TModel, TModelValue, TModelValue> ModelOnlyProperty<TModelValue>(Expression<Func<TModel, TModelValue>> modelProperty)
		{
			ModelBuilderPropertyConfiguration<TEntity, TModel, TModelValue, TModelValue> result = new ModelBuilderPropertyConfiguration<TEntity, TModel, TModelValue, TModelValue>(modelProperty);

			_properties.Add(result);

			return result;
		}

		public TEntity ParseModel(TModel model)
		{
			TEntity entity = Activator.CreateInstance<TEntity>();

			foreach (IModelBuilderPropertyConfiguration<TEntity, TModel> property in Properties)
				property.Apply(entity, model);

			return entity;
		}

		public TModel ParseEntity(TEntity entity)
		{
			TModel model = new TModel();

			foreach (IModelBuilderPropertyConfiguration<TEntity, TModel> property in Properties)
				property.Apply(model, entity);

			return model;
		}
	}
}