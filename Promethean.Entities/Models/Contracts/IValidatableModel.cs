using System.ComponentModel.DataAnnotations;
using Promethean.Notifications.Validators;

namespace Promethean.Entities.Models.Contracts
{
	public interface IValidatableModel<TEntity, TModel> : IModel<TEntity, TModel>, IValidatable, IValidatableObject
		where TEntity : class, IEntity
		where TModel : IValidatableModel<TEntity, TModel>, new()
	{ }
}