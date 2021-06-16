using System;
using System.Linq.Expressions;
using Promethean.Entities.Models.Contracts;

namespace Promethean.Entities.Models.Configurations.Properties.Contracts
{
	public interface IEntityModelPropertyBuilder<TEntity, TModel, TEntityValue>
		where TEntity : class, IEntity
		where TModel : IModel<TEntity, TModel>, new()
	{
		ModelPropertyBuilder<TEntity, TModel, TEntityValue, TModelValue> HasModelProperty<TModelValue>(Expression<Func<TModel, TModelValue>> modelProperty);

		ModelPropertyBuilder<TEntity, TModel, TEntityValue, object> EntityOnly();
	}
}