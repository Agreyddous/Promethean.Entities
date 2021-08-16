using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Promethean.Entities.Models.Contracts;

namespace Promethean.Entities.Models
{
	public abstract class ValidatableModel<TEntity, TModel> : Model<TEntity, TModel>, IValidatableModel<TEntity, TModel>
		where TEntity : class, IEntity
		where TModel : class, IValidatableModel<TEntity, TModel>, new()
	{
		public override TModel Parse(TEntity entity)
		{
			TModel model = base.Parse(entity);
			model.Validate();

			return model;
		}

		public abstract void Validate();

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			Validate();

			return Notifications.SelectMany(notification => notification.Value.Select(message => new ValidationResult(message.Message, new[] { notification.Key })));
		}
	}
}