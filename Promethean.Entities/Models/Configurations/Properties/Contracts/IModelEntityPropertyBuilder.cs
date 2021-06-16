using System;
using System.Linq.Expressions;
using Promethean.Entities.Models.Contracts;

namespace Promethean.Entities.Models.Configurations.Properties.Contracts
{
	public interface IModelEntityPropertyBuilder<TEntity, TModel, TModelValue>
		where TEntity : class, IEntity
		where TModel : IModel<TEntity, TModel>, new()
	{
		ModelPropertyBuilder<TEntity, TModel, TEntityValue, TModelValue> HasEntityProperty<TEntityValue>(Expression<Func<TEntity, TEntityValue>> entityProperty);

		ModelPropertyBuilder<TEntity, TModel, object, TModelValue> ModelOnly();
	}
}