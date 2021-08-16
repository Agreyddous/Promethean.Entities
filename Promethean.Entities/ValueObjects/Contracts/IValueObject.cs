using Promethean.Notifications.Contracts;
using Promethean.Notifications.Validators.Contracts;

namespace Promethean.Entities.ValueObjects.Contracts
{
	public interface IValueObject : INotifiable, IValidatable { }
}