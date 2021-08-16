using System.Collections.Generic;
using System.Linq;
using Promethean.Entities.Models.Contracts;

namespace Promethean.Entities.Models.Extensions
{
	public static class ModelEnumerableExtensions
	{
		public static IEnumerable<TEntity> ToEntities<TEntity, TModel>(this IEnumerable<TModel> models)
			where TEntity : class, IEntity
			where TModel : class, IModel<TEntity, TModel>, new() => models.Select(model => model.Parse());

		public static IEnumerable<TModel> ToModels<TEntity, TModel>(this IEnumerable<TEntity> entities)
			where TEntity : class, IEntity
			where TModel : class, IModel<TEntity, TModel>, new()
		{
			TModel baseModel = new TModel();

			return entities.Select(entity => baseModel.Parse(entity));
		}
	}
}