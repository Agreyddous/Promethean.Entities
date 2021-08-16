using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Promethean.Entities.Models.Contracts;
using Promethean.Notifications.Extensions;

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

			return Notifications.AsValidationResult();
		}
	}
}