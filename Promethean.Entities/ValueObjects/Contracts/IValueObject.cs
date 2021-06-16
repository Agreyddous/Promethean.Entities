using Promethean.Notifications.Contracts;
using Promethean.Notifications.Validators;

namespace Promethean.Entities.ValueObjects.Contracts
{
	public interface IValueObject : INotifiable, IValidatable { }
}