using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Promethean.Entities.Models.Configurations.Properties.Contracts;
using Promethean.Entities.Models.Contracts;

namespace Promethean.Entities.Models.Configurations.Builders.Contracts
{
	public interface IModelConfigurationBuilder<TEntity, TModel>
		where TEntity : class, IEntity
		where TModel : IModel<TEntity, TModel>, new()
	{
		IReadOnlyList<IModelPropertyBuilder<TEntity, TModel>> Properties { get; }

		IEntityModelPropertyBuilder<TEntity, TModel, TEntityValue> EntityProperty<TEntityValue>(Expression<Func<TEntity, TEntityValue>> entityProperty);

		IModelEntityPropertyBuilder<TEntity, TModel, TModelValue> ModelProperty<TModelValue>(Expression<Func<TModel, TModelValue>> modelProperty);

		TEntity Parse(TModel model);

		TModel Parse(TEntity entity);
	}
}